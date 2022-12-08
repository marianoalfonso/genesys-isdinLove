using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isdinLove.forms
{
    public partial class diagnosis : Form
    {
        public diagnosis()
        {
            InitializeComponent();
        }

        private void diagnosis_Load(object sender, EventArgs e)
        {
            //valido conexion con la DB
            bool conexionDbValidada = diganosticarConexionDB();
            if (conexionDbValidada)
            {
                lblDBconnection.Text = "conexion DB: ok";
            } else
            {
                lblDBconnection.Text = "conexion DB: error";
            }

            //valido conexion XLSX (esto lo hago con un archivo que guardo en alguna
            //parte del arbol solo para chequear conexion
            bool conexionXlsxValidada = diagnosticarConexionXLSX();



        }

        //diagnostico la conexion con la DB
        public bool diganosticarConexionDB()
        {
            bool conexion = false;
            try
            {
                clases.cConexion objConection = new clases.cConexion();
                conexion = objConection.setConnection();
                return conexion;
            }
            catch (Exception)
            {
                return conexion;
                throw;
            }
        }

        //diagnostico la conexion con la DB
        public bool diagnosticarConexionXLSX()
        {
            bool conexion = false;
            try
            {


                return conexion;
            }
            catch (Exception)
            {
                return conexion;
                throw;
            }
        }

        //boton temporal, luego lo saco y se ejecuta automaticamente si el diagnostico esta OK
        private void button1_Click(object sender, EventArgs e)
        {
            //instancio la clase watcherLive
            watcherLive watcherLive = new watcherLive();
            watcherLive.Show();
        }
    }
}
