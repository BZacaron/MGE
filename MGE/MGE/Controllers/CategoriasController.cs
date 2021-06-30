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
                var catPai = "N/A";
                if (categoriasEntity.CategoriaPai != null)
                    catPai = categoriasEntity.CategoriaPai.ToString();

                viewModel.Categorias.Add(new Categorias()
                {
                    Id = categoriasEntity.Id.ToString(),
                    Descricao = categoriasEntity.Descricao,
                    CategoriaPai = catPai
                }); ;
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
        public IActionResult Editar(int id)
        {
            try
            {
                var entidadeAEditar = _categoriasService.ObterPorId(id);

                var catPai = "N/A";
                if (entidadeAEditar.CategoriaPai != null)
                    catPai = entidadeAEditar.CategoriaPai.ToString();

                var viewModel = new EditarViewModel()
                {
                    FormMensagensErro = (string[])TempData["formMensagensErro"],
                    Id = entidadeAEditar.Id.ToString(),
                    Descricao = entidadeAEditar.Descricao,
                    CategoriaPai = catPai
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
        public RedirectToActionResult Editar(int id, EditarRequestModel requestModel)
        {
            var listaDeErros = requestModel.ValidarEFiltrar();
            if (listaDeErros.Count > 0)
            {
                TempData["formMessagensErro"] = listaDeErros;

                return RedirectToAction("Editar");
            }

            try
            {
                _categoriasService.Editar(id, requestModel);
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
        public IActionResult Remover(int id)
        {
            try
            {
                var entidadeARemover = _categoriasService.ObterPorId(id);

                var viewModel = new RemoverViewModel()
                {
                    Id = entidadeARemover.Id.ToString(),
                    Descricao = entidadeARemover.Descricao,
                    CategoriaPai = entidadeARemover.CategoriaPai.ToString(),
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
        public RedirectToActionResult Remover(int id, object requestModel)
        {
            try
            {
                _categoriasService.Remover(id);
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
