using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isdinLoveCliente.clases
{
    public static class clsConstantes
    {


        //harcodeo las constantes, luego debo obtenerlas desde algun archivo externo
        //para poder ser editadas sin tener que recompliar el programa

        public static string xlsxPath { get; set; }
        //public static string xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";
        public static string xlsxInboxPath { get; set; }
        public static string sqlConnectionString { get; set; }
        //public static string sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2022MiruLeta";
        public static string fileName { get; set; } //archivo a migrarse

        public static string prefijo { get; set; } //prefijo del archivo a migrarse

    }
}
