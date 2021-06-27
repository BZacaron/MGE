using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.ViewModels.Categorias
{
    public class IndexViewModel
    {
        public ICollection<Categorias> Categorias { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemErro { get; set; }

        public IndexViewModel()
        {
            Categorias = new List<Categorias>();
        }
    }

    public class Categorias
    {
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string CategoriaPaiId { get; set; }
    }
}
