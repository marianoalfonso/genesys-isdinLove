using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // agregamos la referencia al sql
using System.Windows.Forms;

namespace isdinLove.clases
{
    class cConexion
    {
        SqlConnection conn = new SqlConnection();

        //conecta con la DB con los parametros seteados}
        //y devuelve true o false 
        public bool setConnection()
        {
            try
            {
                conn.ConnectionString = clsConstantes.sqlConnectionString;
                conn.Open();
                conn.Close();
                return true;
            }
            catch (SqlException e)
            {
                //guardar el error en el log de errores
                //MessageBox.Show("no se logro conectar a la DB: (" + e.ToString() + ")");
                return false;
            }
        }
    }
}
