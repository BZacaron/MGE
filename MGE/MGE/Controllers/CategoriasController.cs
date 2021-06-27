using MGE.Models.Categorias;
using MGE.RequestModels.Categorias;
using MGE.ViewModels.Categorias;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly CategoriasService _categoriasService;

        public CategoriasController(CategoriasService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                MensagemSucesso = (string)TempData["formMensagemSucesso"],
                MensagemErro = (string)TempData["formMensagemErro"]
            };

            var listaDeCategorias = _categoriasService.ObterTodos();

            foreach (CategoriasEntity categoriasEntity in listaDeCategorias)
            {
                viewModel.Categorias.Add(new Categorias()
                {
                    Id = categoriasEntity.Id.ToString(),
                    Descricao = categoriasEntity.Descricao,
                    CategoriaPaiId = categoriasEntity.CategoriaPaiId.ToString("N"),
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
                _categoriasService.Adicionar(requestModel);
                TempData["formMensagemSucesso"] = "Categoria adicionada com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Adicionar");
            }
        }


        [HttpGet]
        public IActionResult Editar(int param)
        {
            try
            {
                var entidadeAEditar = _categoriasService.ObterPorId(param);

                var viewModel = new EditarViewModel()
                {
                    FormMensagensErro = (string[])TempData["formMensagensErro"],
                    Id = entidadeAEditar.Id.ToString(),
                    Descricao = entidadeAEditar.Descricao,
                    CategoriaPaiId = entidadeAEditar.CategoriaPaiId.ToString("N"),
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
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

            try
            {
                _categoriasService.Editar(param, requestModel);
                TempData["formMensagemSucesso"] = "Categoria editada com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Editar");
            }
        }


        [HttpGet]
        public IActionResult Remover(int param)
        {
            try
            {
                var entidadeARemover = _categoriasService.ObterPorId(param);

                var viewModel = new RemoverViewModel()
                {
                    Id = entidadeARemover.Id.ToString(),
                    Descricao = entidadeARemover.Descricao,
                    CategoriaPaiId = entidadeARemover.CategoriaPaiId.ToString("N"),
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["formMensagemErro"] = e.Message;

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Remover(int param, object requestModel)
        {
            try
            {
                _categoriasService.Remover(param);
                TempData["formMensagemSucesso"] = "Categoria excluida com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Remover");
            }
        }
    }
}
