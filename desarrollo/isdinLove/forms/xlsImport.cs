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

        private void xlsImport_Load(object sender, EventArgs e)
        {
            string xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";
            //DETECTAR TIPO DE ARCHIVO
            //string xlsxFileName = "CON - Archivo ISDIN Love.xlsx";
            string xlsxFileName = "PIN CANJES LI PIN.xlsx";
            string sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2022MiruLeta";
            string sql;

            xlsConnector conexion = new xlsConnector(xlsxPath, xlsxFileName, sqlConnectionString);
            DataTable dt = new DataTable();
            conexion.xlsxFileName = xlsxFileName;

            string archivo = xlsxFileName.Substring(0,3);
            if (archivo == "CON")
            {
                sql = "select [Checkout order id],[Shipping type],[Creation date],[Product type],[Product name],[Product EAN]," +
                                "[Email],[Status],[Pharmacy id sap],[Delivery nº],[Address],[City],[Region name],[Zip code],[Name],[Surname]," +
                                "[Phone],[Id Resource],[Packaging] " +
                             "from [valueSheet$]";
                dt = conexion.obtenerDatos(archivo, sql);
                //MUESTRO TEMPORALMENTE EL DATAGRID, LUEGO NO ME SIRVE
                dataGridView1.DataSource = dt;
            }
            else if (archivo == "PIN")
            {
                sql = "select [Date],[Order ID],[SKU],[Units],[Product],[F# name],[M# name],[L# name],[Street],[City],[Postcode]," +
                        "[Region],[Phone],[Manager ID],[Record Count],[Packing] " +
                       "from [Hoja1$]";
                dt = conexion.obtenerDatos(archivo, sql);
                //MUESTRO TEMPORALMENTE EL DATAGRID, LUEGO NO ME SIRVE
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("tipo de archivo no reconocido");
            }


            //ACA DEBERIA IR EL CODIGO EN COMUN, NO ME RECONOCE LA VARIABLE SQL DEFINIDA A NIVEL DE FUNCION


        }

    }
}
