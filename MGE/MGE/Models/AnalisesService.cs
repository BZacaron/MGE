using MGE.Data;
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
        public class AnalisesSerivce 
        {
            private readonly DatabaseContext _databaseContext;
            private readonly ParametrosService _parametrosService;
            //private readonly CategoriaService _categoriaService;
            //private readonly ItemService _itemService;

            public AnalisesSerivce(DatabaseContext databaseContext, ParametrosService parametrosService/*, CategoriaService categoriaService, ItemService itemService*/)
            {
                _databaseContext = databaseContext;
                _parametrosService = parametrosService;
                //_parametrosService = categoriaService;
                //_parametrosService = itemService;
            }

            public ICollection<ConsumoCategoria> CategoriasQueMaisConsomem()
            {
                var parametrosAtivos = _parametrosService.ObterParametroAtivo();
                //var todasCategorias = _categoriaService.ObterTodos();

                var listaDeConsumos = new Collection<ConsumoCategoria>();

                foreach(var categoriaEntity in todasCategorias)
                {
                    var itensDaCategoria = _itemService.ObterTodosPorCategoria(categoriaEntity.Id);

                    decimal consumoMensalItens = 0;

                    foreach(var itemEntity in itensDaCategoria)
                    {
                        consumoMensalItens += itemEntity.CalcularGastoEnergeticoMensalKwh();
                    }

                    listaDeConsumos.Add(new ConsumoCategoria()
                    {
                        Categoria = categoriaEntity.Descricao,
                        ConsumoMensalKwh = consumoMensalItens,
                        ValorMensalKwh = consumoMensalItens * parametrosAtivos.ValorKwh
                    });
                }

                return listaDeConsumos.OrderByDescending(c => c.ConsumoMensalKwh).Take(3).ToList();
            }
        }

        public class ConsumoCategoria
        {
            public string Categoria { get; set; }
            public decimal ConsumoMensalKwh { get; set; }
            public decimal ValorMensalKwh { get; set; }
        }
    }
}
