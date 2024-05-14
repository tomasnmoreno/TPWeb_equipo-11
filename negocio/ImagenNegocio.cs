using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.ComponentModel.Design;


namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listar(Articulo seleccionado)
        {
            List<Imagen> listaImagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("SELECT Id, IdArticulo, ImagenUrl from IMAGENES where @IdArt = IdArticulo;");
                datos.setearParametro("@IdArt", seleccionado.id);
                datos.leer();

                while (datos.Reader.Read())
                {
                    Imagen aux = new Imagen();
                    aux.id = (int)datos.Reader["Id"];
                    aux.idArticulo = (int)datos.Reader["IdArticulo"];
                    aux.url = (string)datos.Reader["ImagenUrl"];

                    listaImagenes.Add(aux);
                }

                return listaImagenes;
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
    }
}
