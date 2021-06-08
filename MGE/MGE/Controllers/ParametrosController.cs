using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGE.Controllers
{
    public class ParametrosController : Controller
    {
        public IActionResult Parametros()
        {
            return View();
        }
    }
}
