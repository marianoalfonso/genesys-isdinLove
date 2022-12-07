using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace isdinLoveCliente.forms
{
    public partial class archivosPendientes : Form
    {
        public archivosPendientes()
        {
            InitializeComponent();
        }

        private void archivosPendientes_Load(object sender, EventArgs e)
        {
            obtenerArchivosPendientes();
        }


        public bool obtenerArchivosPendientes()
        {

        }


    }
}
