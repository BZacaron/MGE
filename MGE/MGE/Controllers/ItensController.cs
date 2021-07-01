using MGE.Models.Itens;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGE.ViewModels.Itens;
using MGE.RequestModels.Itens;

namespace MGE.Controllers
{
    public class ItensController : Controller
    {
        private readonly ItensService _itensService;

        public ItensController(ItensService itensService)
        {
            _itensService = itensService;
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
                    Categoria = itensEntity.Categoria.Id.ToString(),
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
                var entidadeAEditar = _itensService.ObterPorId(id);

                var viewModel = new EditarViewModel()
                {
                    FormMensagensErro = (string[])TempData["formMensagensErro"],
                    Id = entidadeAEditar.Id.ToString(),
                    Categoria = entidadeAEditar.Categoria.ToString(),
                    Nome = entidadeAEditar.Nome,
                    Descricao = entidadeAEditar.Descricao,
                    DataFabricacao = entidadeAEditar.DataFabricacao.ToString("d"),
                    ConsumoWatts = entidadeAEditar.ConsumoWatts.ToString("N"),
                    HorasUsoDiario = entidadeAEditar.HorasUsoDiario.ToString("N"),
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
                    Categoria = entidadeARemover.Categoria.ToString(),
                    Nome = entidadeARemover.Nome,
                    Descricao = entidadeARemover.Descricao,
                    DataFabricacao = entidadeARemover.DataFabricacao.ToString("d"),
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
