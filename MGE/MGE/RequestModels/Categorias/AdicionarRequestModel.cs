using MGE.Models.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.RequestModels.Categorias
{
    public class AdicionarRequestModel : IDadosBasicosCategoriasModel
    {
        public string Descricao { get; set; }
        public string CategoriaPaiId { get; set; }

        //Validação a nível de controller
        public ICollection<string> ValidarEFiltrar()
        {
            var listaDeErros = new List<string>();

            return listaDeErros;
        }
    }
}
