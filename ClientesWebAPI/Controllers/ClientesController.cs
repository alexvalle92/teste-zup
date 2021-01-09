using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientesWebAPI.Entity;
using ClientesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientesWebAPI.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Gerenciar");
        }

        public IActionResult Gerenciar()
        {
            Services.ClientesController clientes = new Services.ClientesController();
            ClientesModel clientesMod = new ClientesModel();

            ObjectResult retornoPesquisa = (ObjectResult)clientes.Get();
            if(retornoPesquisa.Value != null)
            {
                clientesMod.ListaClientes = (List<Cliente>)retornoPesquisa.Value;
            }

            return View(clientesMod);
        }

        public IActionResult Adicionar()
        {
            ClientesModel clientesMod = new ClientesModel();

            return View(clientesMod);
        }

        public IActionResult Editar(int idCliente)
        {
            ClientesModel clientesMod = new ClientesModel();
            Services.ClientesController clientes = new Services.ClientesController();
            ObjectResult retornoPesquisa = (ObjectResult)clientes.Get(idCliente);

            if (retornoPesquisa.Value != null)
            {
                clientesMod.Cliente = (Cliente)retornoPesquisa.Value;
            }

            return View(clientesMod);
        }
    }
}