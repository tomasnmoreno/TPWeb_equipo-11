using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPWeb_equipo_11
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        
        public List<Articulo> listaArticulos;
        public List<Imagen> listaImagenes;
        public List<dominio.Carrito> listaCarrito;
        public int idRecup;
        public int cantidadSelec;
        Articulo articulo;
        protected void Page_Load(object sender, EventArgs e)
        
        {
            
            listaArticulos = (List<Articulo>)Session["listaArticulos"];
            idRecup = int.Parse(Request.QueryString["id"]);
            articulo = listaArticulos.Find(Articulo => Articulo.id == idRecup);
            Session.Add("articulo", articulo); // creo que no se usa. <tomas moreno>

            ImagenNegocio imgNegocio = new ImagenNegocio();
            listaImagenes = imgNegocio.listar(articulo);
            

            //// Asigno datos a los textboxs ////
            
            txtCodigo.Text = articulo.codigo;
            txtNombre.Text = articulo.nombre;
            txtDescripcion.Text = articulo.descripcion;
            txtMarca.Text = articulo.marca.ToString();
            txtCategoria.Text = articulo.categoria.ToString();
            txtPrecio.Text = "$" + articulo.precio.ToString();
            
            
        }

        protected void validCantidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            cantidadSelec = validCantidad.SelectedIndex + 1;
            Session.Add("cantidadSelec", cantidadSelec);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            dominio.Carrito artCarrito = new dominio.Carrito();
            artCarrito.agregado = articulo;
            cantidadSelec = int.Parse(Session["cantidadSelec"].ToString());
            //artCarrito.cantidad = int.Parse(Session["cantidadSelec"].ToString()); no funciona, se rompe con esto
            artCarrito.cantidad = cantidadSelec;
            listaCarrito = (List<dominio.Carrito>)Session["listaCarrito"];
            listaCarrito.Add(artCarrito);
            Session.Add("listaCarrito", listaCarrito);
        }
    }
}