using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Models.Categorias
{
    public class CategoriasEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CategoriaPaiId { get; set; }
    }
}
