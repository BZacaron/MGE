using MGE.Models.Parametros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.RequestModels.Parametros
{
    public class AdicionarRequestModel : IDadosBasicosParametrosModel
    {
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }

        //Validação a nível de controller
        public ICollection<string> ValidarEFiltrar()
        {
            var listaDeErros = new List<string>();

            return listaDeErros;
        }
    }
}
