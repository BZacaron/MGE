using MGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Item
{
    public class ItemService
    {
        private readonly DatabaseContext _databaseContext;

        public ItemService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ICollection<ItemEntity> ObterTodos()
        {
            return _databaseContext.Itens.ToList();
        }

        public ItemEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.Itens.Find(id);
            }
            catch
            {
                throw new Exception("Item de ID #" + id + " não encontrado");
            }
        }

        public ItemEntity Adicionar(IDadosBasicosItensModel dadosBasicos)
        {
            var novaEntidade = ValidarDadosBasicos(dadosBasicos);
            _databaseContext.Itens.Add(novaEntidade);
            _databaseContext.SaveChanges();

            return novaEntidade;
        }

        public ItemEntity Editar(int id, IDadosBasicosItensModel dadosBasicos)
        {
            var entidadeAEditar = ObterPorId(id);
            entidadeAEditar = ValidarDadosBasicos(dadosBasicos, entidadeAEditar);
            _databaseContext.SaveChanges();

            return entidadeAEditar;
        }

        public bool Remover(int id)
        {
            var entidade = ObterPorId(id);
            _databaseContext.Itens.Remove(entidade);
            _databaseContext.SaveChanges();

            return true;
        }

        private ItemEntity ValidarDadosBasicos(IDadosBasicosItensModel dadosBasicos, ItemEntity entidadeExistente = null)
        {
            var entidade = entidadeExistente ?? new ItemEntity();

            //Validações dos campos
            if (dadosBasicos.Categoria == null)
            {
                throw new Exception("Definir uma categoria é obrigatório");
            }
            try
            {
                var valor = Guid.Parse(dadosBasicos.Categoria);
                entidade.Categoria = valor;
            }
            catch
            {
                throw new Exception("O valor Categoria possui um formato inválido");
            }

            if (dadosBasicos.Nome == null)
            {
                throw new Exception("O nome do item é obrigatório");
            }
            try
            {
                var valor = dadosBasicos.Nome;
                entidade.Nome = valor;
            }
            catch
            {
                throw new Exception("O nome possui um formato inválido");
            }

            if (dadosBasicos.Descricao == null)
            {
                throw new Exception("O campo descrição é obrigatório");
            }
            try
            {
                var valor = dadosBasicos.Descricao;
                entidade.Descricao = valor;
            }
            catch
            {
                throw new Exception("A descrição possui um formato inválido");
            }

            if (dadosBasicos.DataFabricacao == null)
            {
                throw new Exception("O campo data de fabricação é obrigatório");
            }
            try
            {
                var valor = DateTime.Parse(dadosBasicos.DataFabricacao);
                entidade.DataFabricacao = valor;
            }
            catch
            {
                throw new Exception("A data de fabricação possui um formato inválido");
            }

            if (dadosBasicos.ConsumoWatts == null)
            {
                throw new Exception("O campo de consumo watts é obrigatório");
            }
            try
            {
                var valor = Decimal.Parse(dadosBasicos.Descricao);
                entidade.ConsumoWatts = valor;
            }
            catch
            {
                throw new Exception("O consumo em watts possui um formato inválido");
            }

            if (dadosBasicos.HorasUsoDiario == null)
            {
                throw new Exception("O campo horas de uso diário é obrigatório");
            }
            try
            {
                var valor = int.Parse(dadosBasicos.Descricao);
                entidade.HorasUsoDiario = valor;
            }
            catch
            {
                throw new Exception("Campo horas de consumo diário possui um formato inválido");
            }

            return entidade;
        }

        public interface IDadosBasicosItensModel
        {
            public string Categoria { get; set; }
            public string Nome { get; set; }
            public string Descricao { get; set; }
            public string DataFabricacao { get; set; }
            public string ConsumoWatts { get; set; }
            public string HorasUsoDiario { get; set; }
        }
    }
}
