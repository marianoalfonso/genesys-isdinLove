﻿using System;
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


        //obtener datos y devolver datatable
        public DataTable obtenerDatos(string sql) 
        {
            string sqlInsercion;
            try
            {
                
                xlsxConn.Open();
                OleDbCommand cmd = new OleDbCommand(sql, xlsxConn);    //valueSheet$ (nombre de la hoja en el xls)
                OleDbDataAdapter da = new OleDbDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                xlsxConn.Close();

                sqlConn.Open();
                foreach (DataRow row in dt.Rows)
                {
                    sqlInsercion = "insert into mtb_CON values ('" + row["Checkout order id"];
                    sqlInsercion = sqlInsercion + "', '" + row["Shipping type"];
                    sqlInsercion = sqlInsercion + "', '" + row["Creation date"];
                    sqlInsercion = sqlInsercion + "', '" + row["Product type"];
                    sqlInsercion = sqlInsercion + "', '" + row["Product name"];
                    sqlInsercion = sqlInsercion + "', '" + row["Product EAN"];
                    sqlInsercion = sqlInsercion + "', '" + row["Email"];
                    sqlInsercion = sqlInsercion + "', '" + row["Status"];
                    sqlInsercion = sqlInsercion + "', '" + row["Pharmacy id sap"];
                    sqlInsercion = sqlInsercion + "', '" + row["Delivery nº"];
                    sqlInsercion = sqlInsercion + "', '" + row["Address"];
                    sqlInsercion = sqlInsercion + "', '" + row["City"];
                    sqlInsercion = sqlInsercion + "', '" + row["Region name"];
                    sqlInsercion = sqlInsercion + "', '" + row["Zip code"];
                    sqlInsercion = sqlInsercion + "', '" + row["Name"];
                    sqlInsercion = sqlInsercion + "', '" + row["Surname"];
                    sqlInsercion = sqlInsercion + "', '" + row["Phone"];
                    sqlInsercion = sqlInsercion + "', '" + row["Id Resource"];
                    sqlInsercion = sqlInsercion + "', '" + row["Packaging"];
                    sqlInsercion = sqlInsercion + "')";

                    guardarDatos(sqlInsercion);
                }
                sqlConn.Close();

                return dt;
            }
            catch (Exception err)
            {
                return null;
            }

        }

        //obtener datos y devolver datatable
        public void guardarDatos(string sqlLinea)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(sqlLinea, sqlConn);
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
            }



}

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
