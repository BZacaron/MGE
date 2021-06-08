using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Parametros
{
    public class ParametrosEntity
    {
        public int Id { get; set; }
        public decimal ValorKwh { get; set; }
        public decimal FaixaConsumoAlto { get; set; }
        public decimal FaixaConsumoMedio { get; set; }
    }
}
