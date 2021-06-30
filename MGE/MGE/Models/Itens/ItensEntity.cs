using MGE.Models.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Itens
{
    public class ItensEntity
    {
        public Guid Id { get; set; }
        public CategoriasEntity Categoria { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public decimal ConsumoWatts { get; set; }
        public int HorasUsoDiario { get; set; }

        public ItensEntity()
        {
            Id = new Guid();
        }

        public decimal CalcularGastoEnergeticoMensalKwh()
        {
            return ((ConsumoWatts * HorasUsoDiario) * 30) / 1000;
        }
    }
}
