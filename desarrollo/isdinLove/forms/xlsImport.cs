using System;
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
                if (transferirArchivo())
                {
                    generarPedido();
                }
            }
        }








        //validamos el archivo importado
        //analizar para modularizar este proceso
        public void importarXlsx()
        {

            //DETECTAR TIPO DE ARCHIVO
                //string xlsxFileName = "CON - Archivo ISDIN Love.xlsx";
            string xlsxFileName = clsConstantes.fileName;
            clsConstantes.prefijo = xlsxFileName.Substring(0, 3);

            //bool validarArchivo = validarArchivo(clsConstantes.fileName);



            //string xlsxPath = clsConstantes.xlsxPath;
            //string sqlConnectionString = clsConstantes.sqlConnectionString;
            string sql;

            xlsConnector conexion = new xlsConnector(clsConstantes.xlsxPath, xlsxFileName, clsConstantes.sqlConnectionString);
            DataTable dt = new DataTable();
            conexion.xlsxFileName = xlsxFileName;

            //string prefijoArchivo = xlsxFileName.Substring(0, 3);

            int fileID = obtenerID(clsConstantes.prefijo);


            if (clsConstantes.prefijo == "CON")
            {
                sql = "select [Checkout order id],[Shipping type],[Creation date],[Product type],[Product name],[Product EAN]," +
                                "[Email],[Status],[Pharmacy id sap],[Delivery nº],[Address],[City],[Region name],[Zip code],[Name],[Surname]," +
                                "[Phone],[Id Resource],[Packaging] " +
                             "from [valueSheet$]";

                conexion.limpiarTabla(clsConstantes.prefijo);
                dt = conexion.obtenerDatos(clsConstantes.prefijo, sql, fileID);

                //MUESTRO TEMPORALMENTE EL DATAGRID, LUEGO NO ME SIRVE
                dataGridView1.DataSource = dt;
            }
            else if (clsConstantes.prefijo == "PIN")
            {
                sql = "select [Date],[Order ID],[SKU],[Units],[Product],[F# name],[M# name],[L# name],[Street],[City],[Postcode]," +
                        "[Region],[Phone],[Manager ID],[Record Count],[Packing] " +
                       "from [Hoja1$]";
                dt = conexion.obtenerDatos(clsConstantes.prefijo, sql, fileID);
                //MUESTRO TEMPORALMENTE EL DATAGRID, LUEGO NO ME SIRVE
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("tipo de archivo no reconocido");
            }
        }


        //valido el archivo a procesar (ItemBoundsPortion nombre)
        //public bool validarArchivo(fileName)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        public int obtenerID(string prefijoArchivo)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection(clsConstantes.sqlConnectionString);
                string sql = "";
                if (prefijoArchivo == "CON")
                {
                    sql = "select max(fileID) + 1 [fileID] from mtb_CON_STORE";
                }
                else if (prefijoArchivo == "PIN")
                {
                    sql = "select max(fileID) + 1 as [fileID] from mtb_PIN_STORE";
                }

                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                int fileID = (int)cmd.ExecuteScalar();

                sqlConn.Close();
                //return fileID;
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
        public bool transferirArchivo()
        {
            try
            {
                string sql = "";
                string sqlConnectionString = clsConstantes.sqlConnectionString;
                if (clsConstantes.prefijo == "CON")
                {
                    sql = "insert into mtb_CON_STORE select * from mtb_CON";
                }
                else if (clsConstantes.prefijo == "PIN")
                {
                    sql = "insert into mtb_PIN_STORE select * from mtb_PIN";
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


        //armo el array con los articulos y cantidades
        public void generarPedido()
        {
            string sqlConnectionString = clsConstantes.sqlConnectionString;
            string sql = "select [Product EAN][producto],count(*)[cantidad] from mtb_con group by [Product EAN]";
            SqlConnection sqlConn = new SqlConnection(sqlConnectionString);
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            SqlDataReader reader;
            string productoObtenido;
            int cantidadObtenida;

            //creo el datatable
            DataTable productos = new DataTable("productos");
            DataColumn producto = new DataColumn("producto");
            DataColumn cantidad = new DataColumn("cantidad");
            productos.Columns.Add(producto);
            productos.Columns.Add(cantidad);

            sqlConn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productoObtenido = reader[0].ToString();
                cantidadObtenida = int.Parse(reader[1].ToString());
                DataRow fila = productos.NewRow();
                fila["producto"] = productoObtenido;
                fila["cantidad"] = cantidadObtenida;
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
            productos.Rows.Add(filaPackaging);


            //asigno temporalmente lo obtenido al grid
            dgvProductos.DataSource = productos;


            sqlConn.Close();

        }



    }
}
