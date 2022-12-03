using System;
using System.IO; //escribir, leer archivos, ver informacion de directorios, etc
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using isdinLove.clases;

namespace isdinLove.forms
{
    public partial class watcherLive : Form
    {

        public watcherLive()
        {
            InitializeComponent();
        }

        private void watcherLive_Load(object sender, EventArgs e)
        {
            fileSystemWatcher1.Path = clsConstantes.xlsxInboxPath;  //referencio la variable estatica
            fileSystemWatcher1.EnableRaisingEvents = true;
            getFiles(); //se ejecuta al iniciar
        }

        private void getFiles()
        {
            string[] lst = Directory.GetFiles(clsConstantes.xlsxInboxPath); //getfiles es un metodo estatico, asique no es necesario invocar al objeto
            textBox1.Text = "";
            foreach (var sFile in lst)
            {
                textBox1.Text += sFile + Environment.NewLine;
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            getFiles();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            getFiles();
            clsConstantes.fileName = e.Name;
            xlsImport xlsImport = new xlsImport();
            xlsImport.Show();
        }
        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            getFiles();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            getFiles();
        }
    }
}
