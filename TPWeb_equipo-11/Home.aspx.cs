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
        public ImagenNegocio imaNegocio = new ImagenNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio artNegocio = new ArticuloNegocio();
            listaArticulos = artNegocio.listar();

            if (!IsPostBack)
            {
                repetidor.DataSource = listaArticulos;
                repetidor.DataBind();
            }

            Session.Add("listaArticulos", listaArticulos);
        }

        protected void btnAgregarAlCarrito_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int articuloId = Convert.ToInt32(btn.CommandArgument);

            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulos = negocio.listar();


            List<Articulo> seleccionados;
            if (Session["Seleccionados"] == null)
            {
                seleccionados = new List<Articulo>();
            }
            else
            {
                seleccionados = (List<Articulo>)Session["Seleccionados"];
            }

            foreach (Articulo item in listaArticulos)
            {
                if (articuloId == item.id)
                {
                    seleccionados.Add(item);
                }
            }




            Session["Seleccionados"] = seleccionados;
            Response.Redirect(Request.RawUrl);
        }
    }
}
