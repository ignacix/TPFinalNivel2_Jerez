using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlConnection con;
        private SqlCommand cmd;
        private SqlDataReader reader;
        public AccesoDatos()
        {
            con = new SqlConnection("server=.\\SQLEXPRESS; database = CATALOGO_DB; integrated security = true");
            cmd = new SqlCommand();            
        }

        public SqlDataReader Reader()
        {
            return reader;
        }

        public void setearConsulta(string consulta)
        {
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText= consulta;
            cmd.Connection = con;
        }
        public void ejecutarLectura()
        {
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ejecutarConsulta()
        {
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void cerrarConexion()
        {
            try
            {
                if (reader !=null)
                {
                    reader.Close();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }
}
