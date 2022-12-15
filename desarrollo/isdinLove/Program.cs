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

            //desktop andis
            //asigno las variables globales accediendo a las variables estaticas de la clase estatica clsConstantes.cs
            clsConstantes.xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";  //path para el conector odbc de xls
            clsConstantes.xlsxInboxPath = @"A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";         //path del inbox de archivos
            clsConstantes.xlsxStore = @"A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\store\";  //path para el almacen de archivos xlsx
            clsConstantes.sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2023MiruLeta";


            //laptop hp
            //asigno las variables globales accediendo a las variables estaticas de la clase estatica clsConstantes.cs
            //clsConstantes.xlsxPath = @"Data source=C:\projects\genesys\suadeo\ISDIN\genesys-isdinLove\desarrollo\isdinLove\inbox\";  //path para el conector odbc de xls
            //clsConstantes.xlsxStore = @"Data source=C:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\store\";  //path para el almacen de archivos xlsx
            //clsConstantes.xlsxInboxPath = @"C:\projects\genesys\suadeo\ISDIN\genesys-isdinLove\desarrollo\isdinLove\inbox\";         //path del inbox de archivos
            //clsConstantes.sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2023MiruLeta";

            Application.Run(new forms.diagnosis());
            //Application.Run(new forms.watcherLive());
            //Application.Run(new forms.xlsImport());
        }
    }
}
