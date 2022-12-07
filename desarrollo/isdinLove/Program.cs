using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using isdinLove.clases;

namespace isdinLove
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //asigno las variables globales accediendo a las variables estaticas de la clase estatica clsConstantes.cs
            clsConstantes.xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";  //path para el conector odbc de xls
            clsConstantes.xlsxInboxPath = @"A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";         //path del inbox de archivos
            clsConstantes.sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2023MiruLeta";


            Application.Run(new forms.watcherLive());
            //Application.Run(new forms.xlsImport());
        }
    }
}
