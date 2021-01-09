using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Gerenciar");
        }

        public IActionResult Gerenciar()
        {
            return View();
        }

        public IActionResult Adicionar()
        {
            return View("AdicionarEditar");
        }

        public IActionResult Editar()
        {
            return View("AdicionarEditar");
        }
    }
}