using ClientesWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientesWebAPI.Models
{
    public class ClientesModel
    {
        public ErroModel Erro { get; set; }
        public List<Cliente> ListaClientes { get; set; }
        public Cliente Cliente { get; set; }
        public int IdCliente { get; set; }
        public int IdTelefone { get; set; }

        public ClientesModel()
        {
            this.ListaClientes = new List<Cliente>();
            this.Erro = new ErroModel();
            this.Cliente = new Cliente();
        }
    }
}
