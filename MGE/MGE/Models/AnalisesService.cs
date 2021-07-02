using MGE.Data;
using MGE.Models.Categorias;
using MGE.Models.Itens;
using MGE.Models.Parametros;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models
{
    public class AnalisesService
    {

        private readonly DatabaseContext _databaseContext;
        private readonly ParametrosService _parametrosService;
        private readonly CategoriasService _categoriasService;
        private readonly ItensService _itensService;

        public AnalisesService(DatabaseContext databaseContext, ParametrosService parametrosService, CategoriasService categoriasService, ItensService itensService)
        {
            _databaseContext = databaseContext;
            _parametrosService = parametrosService;
            _categoriasService = categoriasService;
            _itensService = itensService;
        }

        public ICollection<ConsumoCategoria> CategoriasQueMaisConsomem()
        {
            var parametrosAtivos = _parametrosService.ObterParametroAtivo();
            var todasCategorias = _categoriasService.ObterTodos();

            var listaDeConsumos = new Collection<ConsumoCategoria>();

            foreach (var categoriasEntity in todasCategorias)
            {
                var idCategoria = categoriasEntity.Id;

                var itensDaCategoria = _itensService.ObterTodosPorCategoria(idCategoria);

                decimal consumoMensalItens = 0;

                foreach (var itensEntity in itensDaCategoria)
                {
                    consumoMensalItens += itensEntity.CalcularGastoEnergeticoMensalKwh();
                }

                listaDeConsumos.Add(new ConsumoCategoria()
                {
                    Categoria = categoriasEntity.Descricao,
                    ConsumoMensalKwh = consumoMensalItens,
                    ValorMensalKwh = consumoMensalItens * parametrosAtivos.ValorKwh
                });
            }

            return listaDeConsumos.OrderByDescending(c => c.ConsumoMensalKwh).Take(3).ToList();
        }

        public ICollection<ConsumoItem> ItensQueMaisConsomem()
        {
            var parametroAtivo = _parametrosService.ObterParametroAtivo();
            var todosItens = _itensService.ObterTodos();

            var listaDeConsumos = new Collection<ConsumoItem>();

            foreach (var itemEntity in todosItens)
            {
                var idItem = itemEntity.Id;

                //decimal consumoMensalItens = 0;

                listaDeConsumos.Add(new ConsumoItem()
                {
                    Item = itemEntity.Nome,
                    ConsumoMensalKwh = itemEntity.CalcularGastoEnergeticoMensalKwh(),
                    ValorMensalKwh = itemEntity.CalcularGastoEnergeticoMensalKwh() * parametroAtivo.ValorKwh
                });
            }
            return listaDeConsumos.OrderByDescending(i => i.ConsumoMensalKwh).Take(5).ToList();
        }

        public decimal TotalConsumoWatts()
        {
            var todosItens = _itensService.ObterTodos();

            decimal totalConsumoMensalWatts = 0;

            foreach (var itemEntity in todosItens)
            {
                totalConsumoMensalWatts += itemEntity.ConsumoWatts;
            }

            return totalConsumoMensalWatts * 30;
        }

        public decimal TotalCustoMensal()
        {
            var parametroAtivo = _parametrosService.ObterParametroAtivo();

            return TotalConsumoWatts() * parametroAtivo.ValorKwh;
        }

        public string FaixaConsumo()
        {
            var parametroAtivo = _parametrosService.ObterParametroAtivo();
            var consumo = TotalConsumoWatts() / 30;

            var alto = parametroAtivo.FaixaConsumoAlto;
            var medio = parametroAtivo.FaixaConsumoMedio;

            if (consumo >= alto)
            {
                return "Alto";
            }
            else if (consumo >= medio && consumo < alto)
            {
                return "Médio";
            }
            else
            {
                return "Baixo";
            }
        }
    }

    public class ConsumoCategoria
    {
        public string Categoria { get; set; }
        public decimal ConsumoMensalKwh { get; set; }
        public decimal ValorMensalKwh { get; set; }
    }

    public class ConsumoItem
    {
        public string Item { get; set; }
        public decimal ConsumoMensalKwh { get; set; }
        public decimal ValorMensalKwh { get; set; }
    }

    /*public class ConsumoMensalWatts
    {
        public decimal Consumo { get; set; }
    }*/
}