using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPWeb_equipo_11
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        List<Articulo> listaArticulos = new List<Articulo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["listaCarrito"] != null)
                {
                    List<dominio.Carrito> seleccionados = (List<dominio.Carrito>)Session["listaCarrito"];
                    int cantidadArticulos = 0;
                    
                    foreach (dominio.Carrito item in seleccionados)
                    {
                        cantidadArticulos+=item.cantidad; 

                    }

                    contadorCarrito.Text = cantidadArticulos.ToString();

                }
            }
        }
    }
}