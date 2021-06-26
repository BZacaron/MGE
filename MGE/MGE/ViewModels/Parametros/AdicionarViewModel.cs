using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.ViewModels.Parametros
{
    public class AdicionarViewModel
    {
        public string[] FormMensagensErro { get; set; }
        public string Id { get; set; }
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }
    }
}
