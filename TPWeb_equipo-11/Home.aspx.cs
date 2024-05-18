using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPWeb_equipo_11
{
    public partial class Home : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos;
        public List<Imagen> listaimagenes;
        public List<dominio.Carrito> listaCarrito;
        List<Articulo> filtroArticulos;
        string filtro;
        public ImagenNegocio imaNegocio = new ImagenNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {           

            if (!IsPostBack)
            {
                ArticuloNegocio artNegocio = new ArticuloNegocio();
                listaArticulos = artNegocio.listar();
                Session.Add("listaArticulos", listaArticulos);

                repetidor.DataSource = listaArticulos;
                repetidor.DataBind();

                if (Session["listaCarrito"] == null)
                {
                    listaCarrito = new List<dominio.Carrito>();
                    Session.Add("listaCarrito", listaCarrito);
                }
            }
            else
            {
                listaArticulos = (List<Articulo>)Session["listaArticulos"];
            }

        }

        protected void btnAgregarAlCarrito_Click(object sender, EventArgs e)
        {
            int articuloId = int.Parse(((Button)sender).CommandArgument);
            listaArticulos = (List<Articulo>)Session["listaArticulos"];
            Articulo seleccionado = listaArticulos.Find(Articulo => Articulo.id == articuloId);

            //Carrito itemCarrito2 = new Carrito();
            dominio.Carrito itemCarrito = new dominio.Carrito();
            itemCarrito.agregado = seleccionado;
            itemCarrito.cantidad = 1;

            listaCarrito = (List<dominio.Carrito>)Session["listaCarrito"];
            listaCarrito.Add(itemCarrito);

            Session.Add("listaCarrito", listaCarrito);

            //Button btn = (Button)sender;
            //int articuloId = Convert.ToInt32(btn.CommandArgument);



            //List<Articulo> seleccionados;
            //if (Session["Seleccionados"] == null)
            //{
            //    ArticuloNegocio negocio = new ArticuloNegocio();
            //    listaArticulos = negocio.listar();
            //    seleccionados = new List<Articulo>();
            //}
            //else
            //{
            //    seleccionados = (List<Articulo>)Session["Seleccionados"];
            //}

            //foreach (Articulo item in listaArticulos)
            //{
            //    if (articuloId == item.id)
            //    {
            //        seleccionados.Add(item);
            //    }
            //}

            //Session["Seleccionados"] = seleccionados;
            //Response.Redirect(Request.RawUrl);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            filtro = txtBusqueda.Text;

            if(filtro != "")
            {
                filtroArticulos = listaArticulos.FindAll(a => a.marca.descripcion.ToUpper().Contains(filtro.ToUpper()) || a.categoria.descripcion.ToUpper().Contains(filtro.ToUpper()) || a.nombre.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                filtroArticulos = negocio.listar();                
            }

            repetidor.DataSource = filtroArticulos;
            repetidor.DataBind();
            Session.Add("listaArticulos", filtroArticulos);            
        }
    }
}
