using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        //private void xlsImport_Load(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0.;" + 
        //            @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\CON - Archivo ISDIN Love.xlsx;Extended properties= 'Excel 8.0;HDR=False'");
        //        conn.Open();
        //        OleDbCommand cmd = new OleDbCommand("select [Checkout order id],[Shipping type] from[valueSheet$]", conn);    //valueSheet$ (nombre de la hoja en el xls)
        //        OleDbDataAdapter da = new OleDbDataAdapter();
        //        da.SelectCommand = cmd;
        //        DataTable dt = new DataTable();
        //        dt.Clear();
        //        da.Fill(dt);
        //        dataGridView1.DataSource = dt;
        //        conn.Close();
        //    }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show(er.Message);
        //    }

        //}

        private void xlsImport_Load(object sender, EventArgs e)
        {
            string xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";
            string xlsxFileName = "CON - Archivo ISDIN Love.xlsx";
            string sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2022MiruLeta";

            xlsConnector conexion = new xlsConnector(xlsxPath, xlsxFileName, sqlConnectionString);
            conexion.xlsxFileName = "CON - Archivo ISDIN Love.xlsx";
            DataTable dt = new DataTable();
            string sql = "select [Checkout order id],[Shipping type],[Creation date],[Product type],[Product name],[Product EAN]," +
                            "[Email],[Status],[Pharmacy id sap],[Delivery nº],[Address],[City],[Region name],[Zip code],[Name],[Surname]," +
                            "[Phone],[Id Resource],[Packaging] " +
                         "from [valueSheet$]";
            dt = conexion.obtenerDatos(sql);
            dataGridView1.DataSource = dt;
        }

    }
}
