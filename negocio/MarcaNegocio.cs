using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> listaMarcas = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("select id, descripcion from marcas");
                datos.leer();

                while(datos.Reader.Read())
                {
                    Marca aux = new Marca();
                    aux.id = datos.Reader.GetInt32(0);
                    aux.descripcion = datos.Reader.GetString(1);

                    listaMarcas.Add(aux);
                }

                return listaMarcas;
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
        public void agregarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (marca.descripcion != "")
                {
                    datos.setQuery("insert into marcas values ('" + marca.descripcion + "')");
                    datos.ejecutarAccion();
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

        public void modificarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("update marcas set descripcion = '" + marca.descripcion + "' where id = @idMarca");
                datos.setearParametro("@idMarca", marca.id);
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

        public void eliminarMarca(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setQuery("delete from marcas where id = @id");
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
