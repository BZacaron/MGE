using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MGE.Models.Itens.ItensService;

namespace MGE.RequestModels.Itens
{
    public class AdicionarRequestModel : IDadosBasicosItensModel
    {
        public string Categoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }

        //Validação a nível de controller
        public ICollection<string> ValidarEFiltrar()
        {
            var listaDeErros = new List<string>();

            return listaDeErros;
        }
    }
}
