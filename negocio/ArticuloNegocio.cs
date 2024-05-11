using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.CodeDom;
using dominio;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.Data.SqlTypes;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();  

            try
            {
                //ME MUESTRA VARIOS ART //datos.setQuery("SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, I.ImagenUrl ImagenUrl, A.Id FROM ARTICULOS A INNER JOIN MARCAS M ON A.IdMarca = M.Id INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id INNER JOIN IMAGENES I ON I.IdArticulo = A.Id");
                // NUEVA QUERY EN PROCESO, NECESITO UNA SOLA FILA POR CADA CODIGO//
                datos.setQuery("SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id   FROM ARTICULOS A  LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id");
                datos.leer();

                while (datos.Reader.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Reader["Id"];
                    aux.codigo = (string)datos.Reader["Codigo"];
                    aux.nombre = (string)datos.Reader["Nombre"];
                    aux.descripcion = (string)datos.Reader["Descripcion"];
                    aux.marca = new Marca();
                    aux.marca.id = (int)datos.Reader["IdMarca"];
                    aux.marca.descripcion = (string)datos.Reader["Marca"];
                    aux.categoria = new CategoriaArticulo();
                    if (!(datos.Reader["IdCategoria"] is DBNull))
                    {
                        aux.categoria.id = (int)datos.Reader["IdCategoria"];
                    }
                    if (!(datos.Reader["Categoria"] is DBNull))
                    {
                    aux.categoria.descripcion = (string)datos.Reader["Categoria"];
                    }
                    else{ aux.categoria.descripcion = "N/A"; }
                    aux.precio = (decimal)datos.Reader["Precio"];
                    if(!(datos.Reader["ImagenUrl"] is DBNull))
                    {
                        aux.imagenUrl = (string)datos.Reader["ImagenUrl"];
                    }

                    listaArticulos.Add(aux);
                }

                return listaArticulos;
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
        private int ultimoIdArticulo()
        {
            int idUltimo;

            List<Articulo> listaArticulos = new List<Articulo> ();
            listaArticulos = listar();
            idUltimo = listaArticulos.Last().id;
            
            return idUltimo;
        }

        public void agregarArticulo(Articulo newArt, List<Imagen> listaImagenes)
        {
            AccesoDatos datos = new AccesoDatos();
            

            try
            {

                datos.setQuery("insert into articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values ('" + newArt.codigo + "', '" + newArt.nombre + "', '" + newArt.descripcion + "', @idMarca, @idCategoria, @precio)");
                datos.setearParametro("@idMarca", newArt.marca.id);
                datos.setearParametro("@idCategoria", newArt.categoria.id);
                datos.setearParametro("@precio", newArt.precio);
                datos.escribir();
                datos.cerrarConexion();

                int idUltimo = ultimoIdArticulo(); //Ultimo hasta el momento, o sea el que acabo de guardar 

                //Recorro la lista y guardo cada imagen del articulo en IMAGENES(Table) en un procedimiento por lote
                int cantImg = listaImagenes.Count();
                for (int i=0; i<cantImg; i++)
                {
                datos.setQuery("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) values('" + (idUltimo) + "', '" + listaImagenes[i].url + "')");
                datos.escribir();
                    datos.cerrarConexion();
                    //i++; //auemento contador para que en las sucesivas vueltas guarde el url de la imagen correspondiente a la lista
                }

                /*datos.setQuery("DECLARE @IdUltimo INT" + //TOMAS: EN FASE BETA
                    "insert into articulos (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values ('" + newArt.codigo + "', '" + newArt.nombre + "', '" + newArt.descripcion + "', @idMarca, @idCategoria, '" + newArt.precio + "') SET @IdUltimo = @@IDENTITY " +
                    "INSERT INTO IMAGENES values(@IdUltimo, '" + newArt.imagenUrl + "')"); */
                //NECESITO QUE GUARDE EL REGISTRO CON 'IdArticulo' CON EL {ultimoId + 1}

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

        public void modificarArticulo(Articulo articulo, List<Imagen>listaImagenes)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //ACTUALIZO EL ARTICULO
                datos.setQuery("UPDATE ARTICULOS SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, Precio = @precio where id = @id");

                datos.setearParametro("@codigo", articulo.codigo);
                datos.setearParametro("@nombre", articulo.nombre);
                datos.setearParametro("@descripcion", articulo.descripcion);
                datos.setearParametro("@IdMarca", articulo.marca.id);
                datos.setearParametro("@IdCategoria", articulo.categoria.id);
                datos.setearParametro("@precio", articulo.precio);
                datos.setearParametro("@id", articulo.id);

                datos.escribir();
                datos.cerrarConexion();
                

                //BORRO TODO PARA DESPUES CARGAR LO QUE TENGO EN LA LISTA ACTUAL
                datos.setQuery("DELETE FROM IMAGENES where IdArticulo = '" + articulo.id +"' " );
                datos.escribir();
                datos.cerrarConexion();

                //CARGO LISTA ACTUAL
                /*datos.setQuery(" IMAGENES SET IdArticulo = @IdArticulo, ImagenUrl = @ImagenUrl where IdArticulo = @idWhere");
                datos.setearParametro("IdArticulo", articulo.id);
                datos.setearParametro("@ImagenUrl", articulo.imagenUrl);
                datos.setearParametro("@idWhere", articulo.id ); 
                datos.escribir();*/

                //Recorro la lista y guardo cada imagen del articulo en IMAGENES(Table) en un procedimiento por lote
                int cantImg = listaImagenes.Count();
                for (int i = 0; i < cantImg; i++)
                {
                    datos.setQuery("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) values('" + (articulo.id) + "', '" + listaImagenes[i].url + "')");
                    datos.escribir();
                    datos.cerrarConexion();
                    //i++; //auemento contador para que en las sucesivas vueltas guarde el url de la imagen correspondiente a la lista
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
                AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("delete from ARTICULOS where id=@id ");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                datos.setQuery("DELETE FROM IMAGENES where IdArticulo = '" + id + "'");
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

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> listaFiltrada = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                string query = "";
                switch (campo)
                {
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor que":
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE A.Precio > " + filtro + " group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                            case "Menor que":
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE A.Precio < " + filtro + " group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                            default:
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE A.Precio = " + filtro + " group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                        }
                        break;

                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con":
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE M.Descripcion like '" + filtro + "%' group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                            case "Termina con":
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE M.Descripcion like '%" + filtro + "' group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                            default:
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE M.Descripcion like '%" + filtro + "%' group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                        }
                        break;

                    case "Categoría":
                        switch (criterio)
                        {
                            case "Comienza con":
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE C.Descripcion like '" + filtro + "%' group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                            case "Termina con":
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE C.Descripcion like '%" + filtro + "' group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";
                                break;
                            default:
                                query = "SELECT A.Codigo Codigo, A.Nombre Nombre, A.Descripcion Descripcion, M.Id IdMarca , M.Descripcion Marca, C.Id IdCategoria, C.Descripcion Categoria, A.Precio Precio, MAX(I.ImagenUrl) as ImagenUrl, A.Id  FROM ARTICULOS A LEFT JOIN IMAGENES I ON I.IdArticulo = A.Id INNER JOIN MARCAS M ON A.IdMarca = M.Id  INNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id  WHERE C.Descripcion like '%" + filtro + "%' group by A.Codigo, A.Nombre, A.Descripcion, M.Id, M.Descripcion, C.Id, C.Descripcion, A.Precio, A.Id order by A.Id";                               
                                break;
                        }
                        break;
                }

                datos.setQuery(query);
                datos.leer();
                while (datos.Reader.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Reader["Id"];
                    aux.codigo = (string)datos.Reader["Codigo"];
                    aux.nombre = (string)datos.Reader["Nombre"];
                    aux.descripcion = (string)datos.Reader["Descripcion"];
                    aux.marca = new Marca();
                    aux.marca.id = (int)datos.Reader["IdMarca"];
                    aux.marca.descripcion = (string)datos.Reader["Marca"];
                    aux.categoria = new CategoriaArticulo();
                    if (!(datos.Reader["IdCategoria"] is DBNull))
                    {
                        aux.categoria.id = (int)datos.Reader["IdCategoria"];
                    }
                    if (!(datos.Reader["Categoria"] is DBNull))
                    {
                        aux.categoria.descripcion = (string)datos.Reader["Categoria"];
                    }
                    else { aux.categoria.descripcion = "N/A"; }
                    aux.precio = (decimal)datos.Reader.GetSqlMoney(7);
                    if (!(datos.Reader["ImagenUrl"] is DBNull))
                    {
                        aux.imagenUrl = (string)datos.Reader["ImagenUrl"];
                    }

                    listaFiltrada.Add(aux);
                }
  
                    return listaFiltrada;
                                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
