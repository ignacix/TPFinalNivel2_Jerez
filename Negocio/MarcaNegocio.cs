using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlDataReader reader;

            try
            {
                accesoDatos.setearConsulta("select Id, Descripcion from Marcas");
                accesoDatos.ejecutarLectura();
                reader = accesoDatos.Reader();

                while (reader.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)reader["Id"];
                    aux.Descripcion = (string)reader["Descripcion"];
                    lista.Add(aux);
                }
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                accesoDatos.cerrarConexion();
            }
        
        }
    }
}
