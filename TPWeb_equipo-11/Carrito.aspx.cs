using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPWeb_equipo_11
{
    public partial class Carrito : System.Web.UI.Page
    {
        public List<Articulo> listaArticulos;
        public List<dominio.Carrito> listaCarrito;
        private int cantidadSelec;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatosActualPag();
            }
            //else
            //{
            //    cargarDatosActualPag();
            //}

            //else
            //{
            //    listaCarrito = (List<dominio.Carrito>)Session["listaCarrito"];
            //    repCarrito.DataSource = listaCarrito;
            //    repCarrito.DataBind();
            //}




            //if (Session["Seleccionados"] != null)
            //{
            //    List<Articulo> seleccionados = (List<Articulo>)Session["Seleccionados"];

            //    if (!IsPostBack)
            //    {
            //        repCarrito.DataSource = seleccionados;
            //        repCarrito.DataBind();

            //        decimal totalCarrito = CalcularTotalCarrito(seleccionados);
            //        lblTotalCarrito.Text = "Total del Carrito: $" + totalCarrito.ToString("0.00");
            //    }
            //}
        }
        private decimal CalcularTotalCarrito(List<dominio.Carrito> articulos)
        {
            decimal total = 0;

            foreach (var articulo in articulos)
            {
                total += articulo.agregado.precio * articulo.cantidad;
            }

            return total;
            //decimal total = 0;

            //foreach (var articulo in articulos)
            //{
            //    total += articulo.precio;
            //}

            //return total;
        }

        private void cargarDatosActualPag()
        {
            listaCarrito = (List<dominio.Carrito>)Session["listaCarrito"];
            repCarrito.DataSource = listaCarrito;
            repCarrito.DataBind();
            decimal totalCarrito = CalcularTotalCarrito(listaCarrito);
            lblTotalCarrito.Text = "Total del Carrito: $" + totalCarrito.ToString("0.00");
        }

        protected void btnEliminarDelCarrito_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int articuloId = Convert.ToInt32(btn.CommandArgument);

            listaCarrito = (List<dominio.Carrito>)Session["listaCarrito"];
            //List<Articulo> seleccionados = (List<Articulo>)Session["listaCarrito"];

            if (listaCarrito.Count() != 0)
            {
                dominio.Carrito articuloElim = listaCarrito.Find(x => x.agregado.id == articuloId);
                listaCarrito.Remove(articuloElim);
                Session.Add("listaCarrito", listaCarrito);
                cargarDatosActualPag();
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((TextBox)sender).Text) >= 0)
            {
                cantidadSelec = Convert.ToInt32(((TextBox)sender).Text);
                //dominio.Carrito artCarrito = new dominio.Carrito();
                listaCarrito = (List<dominio.Carrito>)Session["listaCarrito"];

                var Item = listaCarrito.Find(x => x.agregado.id == Convert.ToInt32(((TextBox)sender).ToolTip));
                Item.cantidad = cantidadSelec;

                Session.Add("listaCarrito", listaCarrito);

                cargarDatosActualPag();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}
