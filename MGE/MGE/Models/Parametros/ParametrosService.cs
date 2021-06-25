using MGE.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Parametros
{
    public class ParametrosService
    {
        private readonly DatabaseContext _databaseContext;

        public ParametrosService(DatabaseContext databaseContext) { }

        public ICollection<ParametrosEntity> ObterTodos()
        {
            return _databaseContext.Parametros.ToList();
        }

        public ParametrosEntity ObterPorId(int id)
        {
            try
            {
                return _databaseContext.Parametros.Find(id);
            }catch{
                throw new Exception("Paramêtro de ID #" + id + " não encontrado");
            }
        }

        public ParametrosEntity Adicionar(IDadosBasicosParametrosModel dadosBasicos)
        {
            var novaEntidade = ValidarDadosBasicos(dadosBasicos);
            _databaseContext.Parametros.Add(novaEntidade);
            _databaseContext.SaveChanges();

            return novaEntidade;
        }

        public ParametrosEntity Editar(int id, IDadosBasicosParametrosModel dadosBasicos)
        {
            var entidadeAEditar = ObterPorId(id);
            entidadeAEditar = ValidarDadosBasicos(dadosBasicos, entidadeAEditar);
            _databaseContext.SaveChanges();

            return entidadeAEditar;
        }

        public bool Remover(int id)
        {
            var entidade = ObterPorId(id);
            _databaseContext.Parametros.Remove(entidade);
            _databaseContext.SaveChanges();

            return true;
        }

        private ParametrosEntity ValidarDadosBasicos(IDadosBasicosParametrosModel dadosBasicos, ParametrosEntity entidadeExistente=null)
        {
            var entidade = entidadeExistente ?? new ParametrosEntity();

            //Validações dos campos
            if(dadosBasicos.ValorKwh == null)
            {
                throw new Exception("O valor Kw/h é obrigatório");
            }
            try
            {
                var valor = Decimal.Parse(dadosBasicos.ValorKwh);
                entidade.ValorKwh = valor;
            }catch{
                throw new Exception("O valor Kw/h possui um formato inválido");
            }

            if (dadosBasicos.FaixaConsumoAlto == null)
            {
                throw new Exception("O valor da faixa de consumo alta é obrigatório");
            }
            try
            {
                var valor = Decimal.Parse(dadosBasicos.FaixaConsumoAlto);
                entidade.FaixaConsumoAlto = valor;
            }
            catch
            {
                throw new Exception("A faixa de consumo alta possui um formato inválido");
            }

            if (dadosBasicos.FaixaConsumoMedio == null)
            {
                throw new Exception("O valor da faixa de consumo média é obrigatório");
            }
            try
            {
                var valor = Decimal.Parse(dadosBasicos.FaixaConsumoMedio);
                entidade.FaixaConsumoMedio = valor;
            }
            catch
            {
                throw new Exception("A faixa de consumo média possui um formato inválido");
            }

            return entidade;
        }
    }

    public interface IDadosBasicosParametrosModel
    {
        public string ValorKwh { get; set; }
        public string FaixaConsumoAlto { get; set; }
        public string FaixaConsumoMedio { get; set; }
    }
}
