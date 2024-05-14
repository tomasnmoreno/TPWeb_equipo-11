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
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio artNegocio = new ArticuloNegocio();
            listaArticulos = artNegocio.listar();
           
            Session.Add("listaArticulos", listaArticulos);
        }
    }
}
