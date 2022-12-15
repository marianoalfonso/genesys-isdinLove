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
            string fileName = e.Name;
            if (validarPrefijo(fileName))
            {
                if (validarExistenciaArchivo(fileName))
                {
                    MessageBox.Show("el archivo ya existe");
                }
                else //no existe, se procesa
                {
                    clsConstantes.fileName = fileName;
                    xlsImport xlsImport = new xlsImport();
                    xlsImport.Show();
                }
            }
            else //el archivo no es CON o PIN
            {
                MessageBox.Show("no se reconoce el nombre del archivo");
            }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            getFiles();
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            getFiles();
        }

        //verifico que el prefijo del archivo sea CON o PIN
        private bool validarPrefijo(string fileName)
        {
            bool estado = false;

            try
            {
                if (fileName.Substring(0, 3) == "CON" || fileName.Substring(0, 3) == "PIN")
                {
                    estado = true;
                }
                return estado;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //verifico que el archivo no exista previamente en la carpeta STORE
        private bool validarExistenciaArchivo(string fileName)
        {
            bool estado = false;
            try
            {
                string path = clsConstantes.xlsxStore + fileName;
                estado = File.Exists(path);
                
                if (estado) //si existe, borro el archivo en la carpeta INBOX
                {
                    File.Delete(clsConstantes.xlsxInboxPath + fileName);
                }
                return estado;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
