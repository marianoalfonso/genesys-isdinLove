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
        static string host = "riv-sql03";
        static string db = "sandbox";
        static string usr = "malfonso";
        static string pwd = "2022MiruLeta";
        static string port = "1433";

        public string connectionString = "Data Source = " + host + "," + port + "; user id = " + usr + "; password = " + pwd + "; Initial Catalog = " + db + "; Persist Security Info = true";

        public SqlConnection setConnection()
        {
            try
            {
                conn.ConnectionString = connectionString;
                conn.Open();
                MessageBox.Show("conexion correcta");
            }
            catch (SqlException e)
            {
                MessageBox.Show("no se logro conectar a la DB: (" + e.ToString() + ")");
            }
            return conn;
        }
    }
}
