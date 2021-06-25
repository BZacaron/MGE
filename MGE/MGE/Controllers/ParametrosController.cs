using MGE.Models.Parametros;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGE.ViewModels.Parametros;

namespace MGE.Controllers
{
    public class ParametrosController : Controller
    {
        private readonly ParametrosService _parametrosService;

        /*public IActionResult Parametros()
        {
            return View();
        }*/

        public ParametrosController(ParametrosService parametrosService)
        {

        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                MensagemSucesso = (string)TempData["formMensagemSucesso"],
                MensagemErro = (string)TempData["formMensagemErro"]
            };
        
            var listaDeParametros = _parametrosService.ObterTodos();

            foreach(ParametrosEntity parametrosEntity in listaDeParametros)
            {
                viewModel.Parametros.Add(new Parametros()
                {
                    Id = parametrosEntity.Id.ToString(),
                    ValorKwh = parametrosEntity.ValorKwh.ToString("C"),
                    FaixaConsumoAlto = parametrosEntity.FaixaConsumoAlto.ToString("N"),
                    FaixaConsumoMedio = parametrosEntity.FaixaConsumoMedio.ToString("N"),
                }); 
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            var viewModel = new AdicionarViewModel()
            {
                FormMensagensErro = (string[])TempData["formMensagensErro"]
            };

            return View(viewModel);
        }
    }
}
