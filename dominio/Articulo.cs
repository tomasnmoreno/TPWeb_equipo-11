using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Articulo
    {

        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        //public int marca { get; set; }
        public Marca marca { get; set; }

        //public int categoria { get; set; }
        
        public CategoriaArticulo categoria { get; set; }
        public string imagenUrl { get; set; }

        //List <string> listaImagenes = new List<string>();

        //public SqlMoney precio { get; set; }

        public decimal precio { get; set; }
    }


}
