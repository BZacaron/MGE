﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.ViewModels.Categorias
{
    public class RemoverViewModel
    {
        public string[] FormMensagensErro { get; set; }
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string CategoriaPaiId { get; set; }
    }
}
