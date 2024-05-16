using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPWeb_equipo_11
{
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Seleccionados"] != null)
            {
                List<Articulo> seleccionados = (List<Articulo>)Session["Seleccionados"];

                if (!IsPostBack)
                {
                    repCarrito.DataSource = seleccionados;
                    repCarrito.DataBind();

                    decimal totalCarrito = CalcularTotalCarrito(seleccionados);
                    lblTotalCarrito.Text = "Total del Carrito: $" + totalCarrito.ToString("0.00");
                }

            }
        }
            private decimal CalcularTotalCarrito(List<Articulo> articulos)
            {
                decimal total = 0;

                foreach (var articulo in articulos)
                {
                    total += articulo.precio;
                }

                return total;
            }
        
    }
}
