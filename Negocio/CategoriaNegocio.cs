using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos accesoDatos = new AccesoDatos();
            SqlDataReader reader;

            try
            {
                accesoDatos.setearConsulta("select Id, Descripcion from CATEGORIAS");
                accesoDatos.ejecutarLectura();
                reader = accesoDatos.Reader();

                while (reader.Read())
                {
                    Categoria aux = new Categoria();
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
