using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.ViewModels.Home
{
    public class IndexViewModel
    {
        public ICollection<CategoriaQueConsome> CategoriasQueMaisConsomem { get; set; }
        public ICollection<ItemQueConsome> ItensQueMaisConsomem { get; set; }

        public IndexViewModel()
        {
            CategoriasQueMaisConsomem = new List<CategoriaQueConsome>();
            ItensQueMaisConsomem = new List<ItemQueConsome>();
        }        
    }
    public class CategoriaQueConsome
    {
        public string Posicao { get; set; }
        public string NomeCategoria { get; set; }
        public string ConsumoMensalKwh { get; set; }
        public string ValorMensalKwh { get; set; }
    }
    public class ItemQueConsome
    {
        public string Posicao { get; set; }
        public string NomeItem { get; set; }
        public string Categoria { get; set; }
        public string ConsumoMensalKwh { get; set; }
        public string ValorMensalKwh { get; set; }
    }
}
