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
        protected void Page_Load(object sender, EventArgs e)
        
        {
            
            listaArticulos = (List<Articulo>)Session["listaArticulos"];
            int idRecup = int.Parse(Request.QueryString["id"]);
            Articulo articulo = listaArticulos.Find(Articulo => Articulo.id == idRecup);
            Session.Add("articulo", articulo);

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
    }
}