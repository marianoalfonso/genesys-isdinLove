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
            xlsConnector conexion = new xlsConnector();
            conexion.conexionAbrir();
            DataTable dt = new DataTable();
            string sql = "select [Checkout order id],[Shipping type] from[valueSheet$]";
            dt = conexion.obtenerDatos(sql);
            dataGridView1.DataSource = dt;
            conexion.conexionCerrar();
        }

    }
}
