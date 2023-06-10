using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        private AccesoDatos AccesoDatos;
        public ArticuloNegocio()
        {
            this.AccesoDatos = new AccesoDatos();
        }
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlDataReader reader;
            try
            {
                AccesoDatos.setearConsulta("select A.Id as 'Id', Codigo, Nombre, A.Descripcion as 'A.Descripcion', IdMarca, M.Descripcion as 'M.Descripcion', IdCategoria,C.Descripcion as 'C.Descripcion', ImagenUrl, Precio from ARTICULOS as A, MARCAS as M, CATEGORIAS as C where IdMarca = M.Id and IdCategoria =C.Id");
                AccesoDatos.ejecutarLectura();
                reader = AccesoDatos.Reader();
                while (reader.Read())
                {                    
                    Articulo aux = new Articulo();
                    aux.Id = (int)reader["Id"];
                    aux.Codigo = (string)reader["Codigo"];
                    aux.Nombre = (string)reader["Nombre"];
                    aux.Descripcion = (string)reader["A.Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)reader["IdMarca"];
                    aux.Marca.Descripcion = (string)reader["M.Descripcion"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)reader["IdCategoria"];
                    aux.Categoria.Descripcion = (string)reader["C.Descripcion"];
                    aux.UrlImagen = (string)reader["ImagenUrl"];
                    string nDecimal = ((decimal)reader["Precio"]).ToString("0.00");
                    aux.Precio = decimal.Parse(nDecimal);

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
                AccesoDatos.cerrarConexion();
            } 


            
        }
        public void Agregar(Articulo articulo)
        {
            try
            {
                AccesoDatos.setearConsulta($"insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)values('{articulo.Codigo}','{articulo.Nombre}','{articulo.Descripcion}', {articulo.Marca.Id}, {articulo.Categoria.Id}, '{articulo.UrlImagen}', {articulo.Precio.ToString().Replace(",", ".")})");
                AccesoDatos.ejecutarConsulta();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }
            
        }

        public void Modificar(Articulo articulo)
        {
            try
            {
                AccesoDatos.setearConsulta($"update ARTICULOS set Codigo='{articulo.Codigo}', Nombre='{articulo.Nombre}', Descripcion='{articulo.Descripcion}',IdMarca={articulo.Marca.Id}, IdCategoria={articulo.Categoria.Id}, ImagenUrl='{articulo.UrlImagen}', Precio = {articulo.Precio.ToString().Replace(",",".")}  where Id={articulo.Id}");
                AccesoDatos.ejecutarConsulta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }

        }

        public void Eliminar(int Id)
        {
            try
            {
                AccesoDatos.setearConsulta($"delete from ARTICULOS where Id = {Id}");
                AccesoDatos.ejecutarConsulta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                AccesoDatos.cerrarConexion();
            }
        }


        
    }
}
