using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.ViewModels.Itens
{
    public class EditarViewModel
    {
        public string[] FormMensagensErro { get; set; }
        public string Id { get; set; }
        public ICollection<SelectListItem> Categorias { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataFabricacao { get; set; }
        public string ConsumoWatts { get; set; }
        public string HorasUsoDiario { get; set; }

        public EditarViewModel()
        {
            Categorias = new List<SelectListItem>
            {

            };
        }
    }
}
