using MGE.Models.Parametros;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGE.ViewModels.Parametros;
using MGE.RequestModels.Parametros;

namespace MGE.Controllers
{
    public class ParametrosController : Controller
    {
        private readonly ParametrosService _parametrosService;

        public ParametrosController(ParametrosService parametrosService)
        {
            _parametrosService = parametrosService;
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

        [HttpPost]
        public RedirectToActionResult Adicionar(AdicionarRequestModel requestModel)
        {
            var listaDeErros = requestModel.ValidarEFiltrar();
            if (listaDeErros.Count > 0)
            {
                TempData["formMensagensErro"] = listaDeErros;

                return RedirectToAction("Adicionar");
            }

            try
            {
                _parametrosService.Adicionar(requestModel);
                TempData["formMensagemSucesso"] = "Parâmetro adicionado com sucesso";

                return RedirectToAction("Index");
            }catch(Exception exception){
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Adicionar");
            }
        }

        [HttpGet]
        public IActionResult Editar(int param)
        {
            try
            {
                var entidadeAEditar = _parametrosService.ObterPorId(param);

                var viewModel = new EditarViewModel()
                {
                    FormMensagensErro = (string[])TempData["formMensagensErro"],
                    Id = entidadeAEditar.Id.ToString(),
                    ValorKwh = entidadeAEditar.ValorKwh.ToString("C"),
                    FaixaConsumoAlto = entidadeAEditar.FaixaConsumoAlto.ToString("N"),
                    FaixaConsumoMedio = entidadeAEditar.FaixaConsumoMedio.ToString("N"),
                };

                return View(viewModel);
            }catch(Exception e){
                TempData["formMensagemErro"] = e.Message;

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Editar(int param, EditarRequestModel requestModel)
        {
            var listaDeErros = requestModel.ValidarEFiltrar();
            if (listaDeErros.Count > 0)
            {
                TempData["formMessagensErro"] = listaDeErros;

                return RedirectToAction("Editar");
            }

            try {
                _parametrosService.Editar(param, requestModel);
                TempData["formMensagemSucesso"] = "Parâmetro editado com sucesso";

                return RedirectToAction("Index");
            } catch(Exception exception) {
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Editar");
            }
        }

        [HttpGet]
        public IActionResult Remover(int param)
        {
            try
            {
                var entidadeARemover = _parametrosService.ObterPorId(param);

                var viewModel = new RemoverViewModel()
                {
                    Id = entidadeARemover.Id.ToString(),
                    ValorKwh = entidadeARemover.ValorKwh.ToString("C"),
                    FaixaConsumoAlto = entidadeARemover.FaixaConsumoAlto.ToString("N"),
                    FaixaConsumoMedio = entidadeARemover.FaixaConsumoMedio.ToString("N"),
                };

                return View(viewModel);
            }catch(Exception e){
                TempData["formMensagemErro"] = e.Message;

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Remover(int param, object requestModel)
        {
            try
            {
                _parametrosService.Remover(param);
                TempData["formMensagemSucesso"] = "Parâmetro excluido com sucesso";

                return RedirectToAction("Index");
            }catch(Exception exception){
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Remover");
            }
        }
    }
}
