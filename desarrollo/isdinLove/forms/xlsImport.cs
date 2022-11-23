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

        //instancio la clase con las variables globales
        clsConstantes constantesGlobales = new clsConstantes();
        

        public xlsImport()
        {
            InitializeComponent();
        }

        private void xlsImport_Load(object sender, EventArgs e)
        {

            //importarXlsx();
            validarArchivo();

        }









        //validamos el archivo importado
        //analizar para modularizar este proceso
        public void importarXlsx()
        {

            //DETECTAR TIPO DE ARCHIVO
                //string xlsxFileName = "CON - Archivo ISDIN Love.xlsx";
                string xlsxFileName = "PIN CANJES LI PIN.xlsx";


            string xlsxPath = constantesGlobales.xlsxPath;
            string sqlConnectionString = constantesGlobales.sqlConnectionString;
            string sql;

            xlsConnector conexion = new xlsConnector(xlsxPath, xlsxFileName, sqlConnectionString);
            DataTable dt = new DataTable();
            conexion.xlsxFileName = xlsxFileName;

            string archivo = xlsxFileName.Substring(0, 3);
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
        }

        public void validarArchivo()
        {
            try
            {
                conValidator validar = new conValidator();
                bool validacionPackaging = validar.validarPackaging("CON"); //validamos packaging
                bool validacionRemito = validar.validarRemito("CON"); //validamos remito
                bool validacionProducto = validar.validarProducto("CON"); //validamos integridad de productos
                bool validacionEmail = validar.validarEmail("CON");

                MessageBox.Show("packaging: " + validacionPackaging);
                MessageBox.Show("remitos: " + validacionRemito);
                MessageBox.Show("productos: " + validacionProducto);
                MessageBox.Show("email: " + validacionEmail);


            }
            catch (Exception ex)
            {
                MessageBox.Show("error validando el archivo: " + ex.Message);
            }







        }


    }
}
