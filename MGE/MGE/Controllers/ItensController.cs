using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Controllers
{
    public class ItensController : Controller
    {
        public IActionResult Itens()
        {
            return View();
        }
    }
}
