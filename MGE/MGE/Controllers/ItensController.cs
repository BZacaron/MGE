using MGE.Models.Itens;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGE.ViewModels.Itens;
using MGE.RequestModels.Itens;
using MGE.Models.Categorias;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MGE.Controllers
{
    public class ItensController : Controller
    {
        private readonly ItensService _itensService;
        private readonly CategoriasService _categoriasService;

        public ItensController(ItensService itensService, CategoriasService categoriasService)
        {
            _itensService = itensService;
            _categoriasService = categoriasService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel()
            {
                MensagemSucesso = (string)TempData["formMensagemSucesso"],
                MensagemErro = (string)TempData["formMensagemErro"]
            };

            var listaDeItens = _itensService.ObterTodos();

            foreach (ItensEntity itensEntity in listaDeItens)
            {
                var desc = "N/A";
                if (itensEntity.Descricao != null)
                    desc = itensEntity.Descricao;

                viewModel.Itens.Add(new Itens()
                {
                    Id = itensEntity.Id.ToString(),
                    Categoria = itensEntity.Categoria.Descricao,
                    Nome = itensEntity.Nome,
                    Descricao = desc,
                    DataFabricacao = itensEntity.DataFabricacao.ToString("d"),
                    ConsumoWatts = itensEntity.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = itensEntity.HorasUsoDiario.ToString("N")
                });
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Adicionar()
        {
            var listaCategorias = _categoriasService.ObterTodos();

            var viewModel = new AdicionarViewModel()
            {
                FormMensagensErro = (string[])TempData["formMensagensErro"]
            };

            foreach(var categoria in listaCategorias)
            {
                viewModel.Categorias.Add(new SelectListItem()
                {
                    Value = categoria.Id.ToString(),
                    Text = categoria.Descricao
                });
            }

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
                _itensService.Adicionar(requestModel);
                TempData["formMensagemSucesso"] = "Item adicionado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Adicionar");
            }
        }


        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            try
            {
                var listaCategorias = _categoriasService.ObterTodos();
                var entidadeAEditar = _itensService.ObterPorId(id);

                var viewModel = new EditarViewModel()
                {
                    FormMensagensErro = (string[])TempData["formMensagensErro"],
                    Id = entidadeAEditar.Id.ToString(),
                    Nome = entidadeAEditar.Nome,
                    Descricao = entidadeAEditar.Descricao,
                    DataFabricacao = entidadeAEditar.DataFabricacao.ToString("d"),
                    ConsumoWatts = entidadeAEditar.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = entidadeAEditar.HorasUsoDiario.ToString("N"),
                };
                foreach (var categoria in listaCategorias)
                {
                    viewModel.Categorias.Add(new SelectListItem()
                    {
                        Value = categoria.Id.ToString(),
                        Text = categoria.Descricao
                    });
                }

                return View(viewModel);
            }
            catch (Exception e)
            {
                TempData["formMensagemErro"] = e.Message;

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public RedirectToActionResult Editar(Guid id, EditarRequestModel requestModel)
        {
            var listaDeErros = requestModel.ValidarEFiltrar();
            if (listaDeErros.Count > 0)
            {
                TempData["formMessagensErro"] = listaDeErros;

                return RedirectToAction("Editar");
            }

            try
            {
                _itensService.Editar(id, requestModel);
                TempData["formMensagemSucesso"] = "Item editado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                TempData["formMensagensErro"] = new List<string> { exception.Message };

                return RedirectToAction("Editar");
            }
        }


        [HttpGet]
        public IActionResult Remover(Guid id)
        {
            try
            {
                var entidadeARemover = _itensService.ObterPorId(id);

                var viewModel = new RemoverViewModel()
                {
                    Id = entidadeARemover.Id.ToString(),
                    Nome = entidadeARemover.Nome,
                    Descricao = entidadeARemover.Descricao,
                    ConsumoWatts = entidadeARemover.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = entidadeARemover.HorasUsoDiario.ToString("N"),
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
        public RedirectToActionResult Remover(Guid id, object requestModel)
        {
            try
            {
                _itensService.Remover(id);
                TempData["formMensagemSucesso"] = "Item excluido com sucesso";

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
