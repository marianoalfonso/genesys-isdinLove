using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;  //agregado

namespace isdinLove
{
    class xlsConnector
    {

        string cadenaConexion = "Provider=Microsoft.ACE.OLEDB.12.0.;" + @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\CON - Archivo ISDIN Love.xlsx;Extended properties= 'Excel 8.0;HDR=False'";
        public OleDbConnection conn = new OleDbConnection();

        public xlsConnector()
        {
            conn.ConnectionString = cadenaConexion;
        }

        //abrir conexion
        public void conexionAbrir()
        {
            try
            {
                conn.Open();
                Console.WriteLine("conexion con XLS abierta");
            }
            catch
            {
                Console.WriteLine("no pudo abrirse la conexion con XLS");
            }

        }

        //obtener datos y devolver datatable
        public DataTable obtenerDatos(string sql) 
        {
            try
            {
                //OleDbCommand cmd = new OleDbCommand("select [Checkout order id],[Shipping type] from[valueSheet$]", conn);    //valueSheet$ (nombre de la hoja en el xls)
                OleDbCommand cmd = new OleDbCommand(sql, conn);    //valueSheet$ (nombre de la hoja en el xls)
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }

        }

        //cerrar conexion
        public void conexionCerrar()
        {
            conn.Close();
        }


    }
}
