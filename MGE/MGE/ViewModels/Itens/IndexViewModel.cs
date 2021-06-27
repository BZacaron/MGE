using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.ViewModels.Itens
{
    public class IndexViewModel
    {
        public ICollection<Itens> Itens { get; set; }
        public string MensagemSucesso { get; set; }
        public string MensagemErro { get; set; }

        public IndexViewModel()
        {
            Itens = new List<Itens>();
        }
    }

    public class Itens
    {
        public string Id { get; set; }
        public string Categoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }
    }
}


