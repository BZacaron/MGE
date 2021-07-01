using MGE.Models;
using MGE.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Controllers
{
    public class HomeController : Controller
    {
        private readonly AnalisesService _analisesService;

        public HomeController(AnalisesService analisesService)
        {
            _analisesService = analisesService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var categoriasQueMaisConsomem = _analisesService.CategoriasQueMaisConsomem();
            var itensQueMaisConsomem = _analisesService.ItensQueMaisConsomem();

            var posicao = 0;
            foreach (var consumoCategoria in categoriasQueMaisConsomem)
            {
                viewModel.CategoriasQueMaisConsomem.Add(new CategoriaQueConsome
                {
                    Posicao = (posicao += 1).ToString(),
                    NomeCategoria = consumoCategoria.Categoria,
                    ConsumoMensalKwh = consumoCategoria.ConsumoMensalKwh.ToString("N"),
                    ValorMensalKwh = consumoCategoria.ValorMensalKwh.ToString("C")
                });
            }

            posicao = 0;
            foreach (var consumoItem in itensQueMaisConsomem)
            {
                viewModel.ItensQueMaisConsomem.Add(new ItemQueConsome
                {
                    Posicao = (posicao += 1).ToString(),
                    NomeItem = consumoItem.Item,
                    ConsumoMensalKwh = consumoItem.ConsumoMensalKwh.ToString("N"),
                    ValorMensalKwh = consumoItem.ValorMensalKwh.ToString("C")
                }) ;
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
