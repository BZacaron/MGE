using MGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Categorias
{
    public class CategoriasService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriasService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public ICollection<CategoriasEntity> ObterTodos()
        {
            return _databaseContext.Categorias.ToList();
        }

        public CategoriasEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.Categorias.Find(id);
            }
            catch
            {
                throw new Exception("Categoria de ID #" + id + " não encontrado");
            }
        }

        public CategoriasEntity Adicionar(IDadosBasicosCategoriasModel dadosBasicos)
        {
            var novaEntidade = ValidarDadosBasicos(dadosBasicos);
            _databaseContext.Categorias.Add(novaEntidade);
            _databaseContext.SaveChanges();

            return novaEntidade;
        }

        public CategoriasEntity Editar(int id, IDadosBasicosCategoriasModel dadosBasicos)
        {
            var entidadeAEditar = ObterPorId(id);
            entidadeAEditar = ValidarDadosBasicos(dadosBasicos, entidadeAEditar);
            _databaseContext.SaveChanges();

            return entidadeAEditar;
        }

        public bool Remover(int id)
        {
            var entidade = ObterPorId(id);
            _databaseContext.Categorias.Remove(entidade);
            _databaseContext.SaveChanges();

            return true;
        }

        private CategoriasEntity ValidarDadosBasicos(IDadosBasicosCategoriasModel dadosBasicos, CategoriasEntity entidadeExistente = null)
        {
            var entidade = entidadeExistente ?? new CategoriasEntity();

            //Validações dos campos
            if (dadosBasicos.Descricao == null)
            {
                throw new Exception("A descrição é obrigatório");
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

            try
            {
                var valor = int.Parse(dadosBasicos.CategoriaPai);
                var categoria = ObterPorId(1);
                entidade.CategoriaPai = categoria;
            }
            catch
            {
                throw new Exception("A Categoria Pai possui um formato inválido");
            }

            return entidade;
        }
    }

    public interface IDadosBasicosCategoriasModel
    {
        public string Descricao { get; set; }
        public string CategoriaPai { get; set; }
    }
}