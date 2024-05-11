using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<CategoriaArticulo> listar()
        {
            List<CategoriaArticulo> listaCategorias = new List<CategoriaArticulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("select id, descripcion from categorias");
                datos.leer();

                while(datos.Reader.Read())
                {
                    CategoriaArticulo aux = new CategoriaArticulo();
                    aux.id = datos.Reader.GetInt32(0);
                    aux.descripcion = datos.Reader.GetString(1);

                    listaCategorias.Add(aux);
                }

                return listaCategorias;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregarCategoria(CategoriaArticulo categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (categoria.descripcion != "")
                {
                    datos.setQuery("insert into categorias values ('" + categoria.descripcion + "')");
                    datos.escribir();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarCategoria(CategoriaArticulo categoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("update categorias set descripcion = '" + categoria.descripcion + "' where id = @id");
                datos.setearParametro("@id", categoria.id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void eliminarCategoria(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setQuery("delete from categorias where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}
