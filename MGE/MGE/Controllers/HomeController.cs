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
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
