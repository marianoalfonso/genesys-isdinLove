using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;  //agregado
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using isdinLove.clases;

namespace isdinLove.forms
{
    public partial class xlsImport : Form
    {        

        public xlsImport()
        {
            InitializeComponent();
        }

        private void xlsImport_Load(object sender, EventArgs e)
        {
            //averiguar si es posible controlar el nombre del archivo
            //para evitar ser procesado nuevamente

            importarXlsx();
            if (validarArchivo())
            {
                if (transferirArchivo("validado"))
                {
                    generarPedido();
                }
            } else
            {
                transferirArchivo("no validado");
            }
        }


        //validamos el archivo importado
        //analizar para modularizar este proceso
        public void importarXlsx()
        {
            //DETECTAR TIPO DE ARCHIVO
            string xlsxFileName = clsConstantes.fileName;
            clsConstantes.prefijo = xlsxFileName.Substring(0, 3);

            xlsConnector conexion = new xlsConnector(clsConstantes.xlsxPath, xlsxFileName, clsConstantes.sqlConnectionString);
            DataTable dt = new DataTable();
            conexion.xlsxFileName = xlsxFileName;

            int fileID = obtenerID();
            conexion.limpiarTabla();
            dt = conexion.obtenerDatosBulk(fileID);
            dataGridView1.DataSource = dt;

            moverArchivo(xlsxFileName);
        }


        //obtengo el proximo ID del archivo
        public int obtenerID()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(clsConstantes.sqlConnectionString);
                string sql = "";
                if (clsConstantes.prefijo == "CON")
                {
                    sql = "select max(fileID) + 1 [fileID] from mtb_CON_STORE";
                }
                else if (clsConstantes.prefijo == "PIN")
                {
                    sql = "select max(fileID) + 1 as [fileID] from mtb_PIN_STORE";
                }

                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                int fileID = (int)cmd.ExecuteScalar();

                sqlConn.Close();
                return fileID;
            }
            catch (Exception ex)
            {
                return 1;
            }



        }



        public bool validarArchivo()
        {
            bool validado = false;
            try
            {
                conValidator validar = new conValidator();
                bool validacionPackaging = validar.validarPackaging(clsConstantes.prefijo); //validamos packaging
                bool validacionRemito = validar.validarRemito(clsConstantes.prefijo); //validamos remito
                bool validacionProducto = validar.validarProducto(clsConstantes.prefijo); //validamos integridad de productos
                bool validacionEmail = validar.validarEmail(clsConstantes.prefijo);

                //muestro los controles check
                //packaging
                if (validacionPackaging)
                {
                    chkPackaging.Checked = true;
                    chkPackaging.ForeColor = Color.Green;
                } else
                {
                    chkPackaging.ForeColor = Color.Red;
                }
                //remito
                if (validacionRemito)
                {
                    chkRemito.Checked = true;
                    chkRemito.ForeColor = Color.Green;
                }
                else
                {
                    chkRemito.ForeColor = Color.Red;
                }
                //producto
                if (validacionProducto)
                {
                    chkProducto.Checked = true;
                    chkProducto.ForeColor = Color.Green;
                }
                else
                {
                    chkProducto.ForeColor = Color.Red;
                }
                //email
                if (validacionEmail)
                {
                    chkEmail.Checked = true;
                    chkEmail.ForeColor = Color.Green;
                }
                else
                {
                    chkEmail.ForeColor = Color.Red;
                }

                if (validacionPackaging && validacionRemito && validacionProducto && validacionEmail)
                {
                    validado = true;
                } else
                {
                    validado = false;
                }

                return validado;

            }
            catch (Exception ex)
            {
                MessageBox.Show("error validando el archivo: " + ex.Message);
                return validado;
            }
        }


        //transfiero el archivo a las tablas de almacenamiento
        public bool transferirArchivo(string estado)
        {
            try
            {
                string sql = "";
                string sqlConnectionString = clsConstantes.sqlConnectionString;
                if (clsConstantes.prefijo == "CON")
                {
                    if (estado == "validado")
                    {
                        sql = "insert into mtb_CON_STORE select *,2[estado] from mtb_CON";
                    } else
                    {
                        sql = "insert into mtb_CON_STORE select *,1[estado] from mtb_CON";
                    }
                    
                }
                else if (clsConstantes.prefijo == "PIN")
                {
                    if (estado == "validado")
                    {
                        sql = "insert into mtb_PIN_STORE select *,2[estado] from mtb_PIN";
                    } else
                    {
                        sql = "insert into mtb_PIN_STORE select *,1[estado] from mtb_PIN";
                    }
                        
                }
                SqlConnection sqlConn = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("transferirArchivo: " + ex.Message);
                return false;
                throw;
            }
        }

        //muevo el archivo de la carpeta INBOX a STORE
        public void moverArchivo(string fileName)
        {
            try
            {
                string moverDesde = clsConstantes.xlsxInboxPath + fileName;
                string moverHasta = clsConstantes.xlsxStore + fileName;
                File.Move(moverDesde, moverHasta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("no pudo moverse el archivo a la carpeta STORE: (" + ex.Message);
            }
        }

        //armo el array con los articulos y cantidades
        public void generarPedido()
        {
            string sqlConnectionString = clsConstantes.sqlConnectionString;
            string sql = "";
            string prefijoArchivo = "CON";
            string msj = "";

            try
            {
                sql = "select [Product EAN][producto],count(*)[cantidad] from mtb_con group by [Product EAN]";
                SqlConnection sqlConn = new SqlConnection(sqlConnectionString);
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                SqlDataReader reader;
                string productoObtenido;
                int cantidadObtenida;

                //creo el datatable
                DataTable productos = new DataTable("productos");
                DataColumn producto = new DataColumn("producto");
                DataColumn cantidad = new DataColumn("cantidad");
                DataColumn prefijo = new DataColumn("prefijo");
                DataColumn mensaje = new DataColumn("mensaje");
                productos.Columns.Add(producto);
                productos.Columns.Add(cantidad);
                productos.Columns.Add(prefijo);
                productos.Columns.Add(mensaje);

                sqlConn.Open();
                reader = cmd.ExecuteReader();

                //cargo los datos en el datatable
                while (reader.Read())
                {
                    productoObtenido = reader[0].ToString();
                    cantidadObtenida = int.Parse(reader[1].ToString());
                    DataRow fila = productos.NewRow();
                    fila["producto"] = productoObtenido;
                    fila["cantidad"] = cantidadObtenida;
                    fila["prefijo"] = "CON";
                    fila["mensaje"] = msj;
                    productos.Rows.Add(fila);
                }
                reader.Close();

                //obtengo la cantiad de packaging (chkPackaging = 'ISDIN')
                sql = "select count(*)[cantidad] from mtb_con where packaging = 'ISDIN'";
                cmd.Connection = sqlConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                int cantidadPackaging = Convert.ToInt32(cmd.ExecuteScalar());
                //agrego el valor al datatable
                DataRow filaPackaging = productos.NewRow();
                filaPackaging["producto"] = "packaging";
                filaPackaging["cantidad"] = cantidadPackaging;
                filaPackaging["prefijo"] = prefijoArchivo;
                filaPackaging["mensaje"] = msj;
                productos.Rows.Add(filaPackaging);

                //asigno temporalmente lo obtenido al grid
                dgvProductos.DataSource = productos;
                sqlConn.Close();


                SqlBulkCopy objBulk = new SqlBulkCopy(sqlConn);
                objBulk.DestinationTableName = "tmpProcesoIsdin";
                //mapeo cada campo del datatable (campo datatable --> campo tabla sql)
                objBulk.ColumnMappings.Add("producto", "cod_articulo");
                objBulk.ColumnMappings.Add("cantidad", "cantidad");
                objBulk.ColumnMappings.Add("prefijo", "proceso");
                objBulk.ColumnMappings.Add("mensaje", "mensaje");
                sqlConn.Open();
                objBulk.WriteToServer(productos);
                sqlConn.Close();


                //invoco al stored procedure y lo guardo en un datatable
                //p_api_actualiza_stock  (se invoca por ODBC)
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = clsConstantes.sqlConnectionString;
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("p_api_actualiza_stock", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@p_proceso", SqlDbType.NVarChar).Value = "CON";
                da.SelectCommand.Parameters.AddWithValue("@estado", SqlDbType.NVarChar).Value = "ok";
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {


                throw;
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
