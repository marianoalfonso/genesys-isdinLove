using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using isdinLove.clases;
using System.Data.SqlClient;

namespace isdinLove.clases
{
    class conValidator
    {
        public string sqlConnectionString { get; set; }
        clsConstantes constantesGlobales = new clsConstantes(); //instanciamos la clase constantesGlobales
        private SqlConnection sqlConn = new SqlConnection();

        //constructor
        public conValidator()
        {
            this.sqlConnectionString = constantesGlobales.sqlConnectionString; //tomamos el sqlConnectionString de la clase de variables globales
        }


        //validamos que el packaging no este nulo
        //recibimos el parametro prefijo (CON,PIN)
        public bool validarPackaging(string prefijo)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = sqlConnectionString;
            bool estado = false;
            string sql;

            if (prefijo == "CON")
            {
                sql = "select count(*) as cantidad from mtb_CON where packaging not in ('ISDIN','TRF')";
            }
            else
            {
                sql = "select count(*) from mtb_PIN where packing not in ('ISDIN','TRF')";
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                int registrosNulos = (int)cmd.ExecuteScalar();
                if (registrosNulos == 0)
                {
                    estado = true;
                }
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                estado = false;
                return estado;
            }
            return estado;
        }


        //validamos que el numero de remito no venga vacio o no sea numerico
        //recibimos el parametro prefijo (CON,PIN)
        public bool validarRemito(string prefijo)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = sqlConnectionString;
            bool estado = false;
            string sql;

            if (prefijo == "CON")
            {
                sql = "select count([Checkout order id]) as cantidad from mtb_CON where isnumeric([Checkout order id]) = 0";
            }
            else
            {
                sql = "select count([Order id]) as cantidad from mtb_PIN where isnumeric([Order id]) = 0";
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                int registrosNulos = (int)cmd.ExecuteScalar();
                if (registrosNulos == 0)
                {
                    estado = true;
                }
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                estado = false;
                return estado;
            }
            return estado;
        }


        //validamos que el numero de remito no venga vacio o no sea numerico
        //recibimos el parametro prefijo (CON,PIN)
        public bool validarProducto(string prefijo)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = sqlConnectionString;
            bool estado = false;
            string sql;

            if (prefijo == "CON")
            {
                sql = "select count(*) from mtb_CON where [Product EAN] is null or [Product EAN] = ''";
            }
            else
            {
                sql = "select count(*) from mtb_PIN where [SKU] is NULL or [SKU] = ''";
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                int registrosNulos = (int)cmd.ExecuteScalar();
                if (registrosNulos == 0)
                {
                    estado = true;
                }
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                estado = false;
                return estado;
            }
            return estado;
        }


        //validamos que el email no venga mas de una vez ya que corresponde a un receptor
        //recibimos el parametro prefijo (CON,PIN)
        public bool validarEmail(string prefijo)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = sqlConnectionString;
            bool estado = false;
            string sql;

            if (prefijo == "CON")
            {
                sql = "select top 1 count(*) as cantidad from mtb_CON group by email order by 1 desc";
            }
            else
            {
                sql = "select top 1 count(*) as cantidad from mtb_PIN group by [Manager ID] order by 1 desc";
            }

            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConn);
                sqlConn.Open();
                int registrosNulos = (int)cmd.ExecuteScalar();
                if (registrosNulos == 1)
                {
                    estado = true;
                }
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                sqlConn.Close();
                estado = false;
                return estado;
            }
            return estado;
        }
        //validamos xxxxxxxxxxxxxxxx
        //recibimos el parametro prefijo (CON,PIN)


        //validamos xxxxxxxxxxxxxxxx
        //recibimos el parametro prefijo (CON,PIN)
    }
}
