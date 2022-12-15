using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;        //conexion con xls
using System.Data.SqlClient;    //conexion con sql

namespace isdinLove.clases
{
    class xlsConnector
    {
        public string xlsxPath { get; set; }
        public string xlsxFileName { get; set; }
        public string sqlConnectionString { get; set; }

               
        private OleDbConnection xlsxConn = new OleDbConnection();
        private SqlConnection sqlConn = new SqlConnection();

        //constructor (siempre debe tener el mismo nombre que la clase)
        //si no hay constructor, existe uno por defecto que no recibe nada
        public xlsConnector(string xlsxPath, string xlsxFileName, string sqlConnectionString)
        {
            this.xlsxPath = xlsxPath;                       // usando this. accedo a las propiedades de la clase, en este caso accedo a public string xlsxPath { get; set; }
            this.xlsxFileName = xlsxFileName;               // usando this. accedo a las propiedades de la clase, en este caso accedo a public string xlsxFileName { get; set; }
            this.sqlConnectionString = sqlConnectionString; // usando this. accedo a las propiedades de la clase, en este caso accedo a public string sqlConnectionString { get; set; }
            string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0.;" + xlsxPath + xlsxFileName + ";Extended properties= 'Excel 8.0;HDR=False'";
            xlsxConn.ConnectionString = cadenaConexion;         //xlsx
            sqlConn.ConnectionString = sqlConnectionString;     //sql
        }

        //limpiamos la tabla destino
        public void limpiarTabla()
        {
            string sql = "truncate table mtb_" + clsConstantes.prefijo;
            SqlCommand cmd = new SqlCommand(sql, sqlConn);
            try
            {
                sqlConn.Open();
                cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception)
            {
               
                throw;
            }
        }


        //extraigo los datos del XLSX y los inserto en el SQL
        public DataTable obtenerDatosBulk(int fileID)
        //public DataTable obtenerDatosBulk(string archivo, string sql, int fileID)
        {
            string sql = "";
            try
            {
                if (clsConstantes.prefijo == "CON")
                {
                    sql = "select [Checkout order id],[Shipping type],[Creation date],[Product type],[Product name],[Product EAN]," +
                            "[Email],[Status],[Pharmacy id sap],[Delivery nº],[Address],[City],[Region name],[Zip code],[Name],[Surname]," +
                            "[Phone],[Id Resource],[Packaging] " +
                            "from [valueSheet$]";
                }
                else if (clsConstantes.prefijo == "PIN")
                {
                    sql = "select [Date],[Order ID],[SKU],[Units],[Product],[F# name],[M# name],[L# name],[Street],[City],[Postcode]," +
                            "[Region],[Phone],[Manager ID],[Record Count],[Packing] " +
                            "from [Hoja1$]";
                }

                xlsxConn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, xlsxConn);    //valueSheet$ (nombre de la hoja en el xls)
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                xlsxConn.Close();

                //agrego un campo en el datarow que no existe
                //enum este caso es el mismo valor para todos los row ya que es un ID
                //y lo lleno con datos antes de asignar el mapeo de columnas para el bulk insert
                dt.Columns.Add(new DataColumn("fileID", typeof(System.Int16)));
                foreach(DataRow dr in dt.Rows)
                {
                    dr["fileID"] = fileID;
                }


                SqlBulkCopy objBulk = new SqlBulkCopy(sqlConn);
                if (clsConstantes.prefijo == "CON")
                {
                    objBulk.DestinationTableName = "mtb_CON";
                    //mapeo cada campo del datatable (campo datatable --> campo tabla sql)
                    objBulk.ColumnMappings.Add("fileID", "fileID");
                    objBulk.ColumnMappings.Add("Checkout order id", "Checkout order id");
                    objBulk.ColumnMappings.Add("Shipping type", "Shipping type");
                    objBulk.ColumnMappings.Add("Creation date", "Creation date");
                    objBulk.ColumnMappings.Add("Product type", "Product type");
                    objBulk.ColumnMappings.Add("Product name", "Product name");
                    objBulk.ColumnMappings.Add("Product EAN", "Product EAN");
                    objBulk.ColumnMappings.Add("Email", "Email");
                    objBulk.ColumnMappings.Add("Status", "Status");
                    objBulk.ColumnMappings.Add("Pharmacy id sap", "Pharmacy id sap");
                    objBulk.ColumnMappings.Add("Delivery nº", "Delivery nº");
                    objBulk.ColumnMappings.Add("Address", "Address");
                    objBulk.ColumnMappings.Add("City", "City");
                    objBulk.ColumnMappings.Add("Region name", "Region name");
                    objBulk.ColumnMappings.Add("Zip code", "Zip code");
                    objBulk.ColumnMappings.Add("Name", "Name");
                    objBulk.ColumnMappings.Add("Surname", "Surname");
                    objBulk.ColumnMappings.Add("Phone", "Phone");
                    objBulk.ColumnMappings.Add("Id Resource", "Id Resource");
                    objBulk.ColumnMappings.Add("Packaging", "Packaging");

                    sqlConn.Open();
                    objBulk.WriteToServer(dt);
                    sqlConn.Close();

                    //return dt;
                }
                else if (clsConstantes.prefijo == "PIN")
                {
                    objBulk.DestinationTableName = "mtb_PIN";
                    //mapeo cada campo del datatable (campo datatable --> campo tabla sql)
                    objBulk.ColumnMappings.Add("fileID", "fileID");
                    objBulk.ColumnMappings.Add("Date", "Date");
                    objBulk.ColumnMappings.Add("Order ID", "Order ID");
                    objBulk.ColumnMappings.Add("SKU", "SKU");
                    objBulk.ColumnMappings.Add("Units", "Units");
                    objBulk.ColumnMappings.Add("Product", "Product");
                    objBulk.ColumnMappings.Add("F# name", "F# name");
                    objBulk.ColumnMappings.Add("M# name", "M# name");
                    objBulk.ColumnMappings.Add("L# name", "L# name");
                    objBulk.ColumnMappings.Add("Street", "Street");
                    objBulk.ColumnMappings.Add("City", "City");
                    objBulk.ColumnMappings.Add("Postcode", "Postcode");
                    objBulk.ColumnMappings.Add("Region", "Region");
                    objBulk.ColumnMappings.Add("Phone", "Phone");
                    objBulk.ColumnMappings.Add("Manager ID", "Manager ID");
                    objBulk.ColumnMappings.Add("Record Count", "Record Count");
                    objBulk.ColumnMappings.Add("Packing", "Packing");

                    sqlConn.Open();
                    objBulk.WriteToServer(dt);
                    sqlConn.Close();
                }

                return dt;

            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        //abro la conexion SQL
        public void conexionSqlAbrir()
        {
            try
            {
                string SqlConnectionString = "Data Source=.;initial catalog=baes;integrated security=true";
                SqlConnection sqlConn = new SqlConnection(SqlConnectionString);
                sqlConn.Open();
            }
            catch
            {

            }
        }

        //cierro la conexion SQL
        public void conexionSqlCerrar()
        {
            try
            {
                sqlConn.Close();
            }
            catch
            {

            }
        }

    }
}
