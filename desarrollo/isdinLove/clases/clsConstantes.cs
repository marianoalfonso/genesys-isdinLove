using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace isdinLove.clases
{
    class clsConstantes
    {


        //harcodeo las constantes, luego debo obtenerlas desde algun archivo externo
        //para poder ser editadas sin tener que recompliar el programa

        public string xlsxPath { get; set; }
        //public static string xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";
        public string sqlConnectionString { get; set; }
        //public static string sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2022MiruLeta";



        //constructor
        public clsConstantes()
        {

            //habilitar cuando obtenga los valores desde un archivo externo y se pueda inicializar el constructor
            this.xlsxPath = @"Data source=A:\genesys.proyectos\genesys-isdinLove\desarrollo\isdinLove\inbox\";
            this.sqlConnectionString = "Data Source=riv-sql03;initial catalog=sandbox;User id=malfonso;Password=2022MiruLeta";

        }

    }
}
