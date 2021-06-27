using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Item
{
    public class ItemEntity
    {
        public Guid Id { get; set; }
        public CategoriaEntity Categoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public decimal ConsumoWatts { get; set; }
        public int HorasUsoDiario { get; set; }

        public ItemEntity()
        {
            Id = new Guid();
        }

        public decimal CalcularGastoEnergeticoMensalKwh()
        {
            return ((ConsumoWatts * HorasUsoDiario) * 30) / 1000;
        }
    }
}
