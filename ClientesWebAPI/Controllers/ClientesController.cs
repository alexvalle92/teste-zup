using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ClientesWebAPI.Entity;
using ClientesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientesWebAPI.Controllers
{
    public class ClientesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Gerenciar");
        }

        public async Task<IActionResult> GerenciarAsync()
        {
            ClientesModel clientesMod = new ClientesModel();

            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Get, $"Clientes");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                clientesMod.ListaClientes = JsonConvert.DeserializeObject<List<Cliente>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                clientesMod.Erro = new ErroModel() { Mensagem = "Ooops, algo deu errado na pesquisa dos clientes. Verifique com o administrador do sistema para mais detalhes." };
            }

            return View(clientesMod);
        }

        public IActionResult Adicionar()
        {
            ClientesModel clientesMod = new ClientesModel();

            return View(clientesMod);
        }

        public async Task<IActionResult> EditarAsync(int idCliente)
        {
            ClientesModel clientesMod = new ClientesModel();

            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Get, $"Clientes/{idCliente}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                clientesMod.Cliente = JsonConvert.DeserializeObject<Cliente>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                clientesMod.Erro = JsonConvert.DeserializeObject<ErroModel>(await response.Content.ReadAsStringAsync());
            }

            return View(clientesMod);
        }

        public async Task<JsonResult> DoExcluirClienteAsync([FromBody] ClientesModel cliente)
        {
            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Delete, $"Clientes/{cliente.IdCliente}");

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                cliente.Erro = JsonConvert.DeserializeObject<ErroModel>(await response.Content.ReadAsStringAsync());
                //cliente.Erro = new ErroModel() { Mensagem = $"Não foi possível encontra o cliente {cliente.IdCliente}." };
            } else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                cliente.Erro = new ErroModel() { Mensagem = "Ooops, algo deu de errado na exclusão do cliente. Entre em contato com o administrador do sistema para mais detalhes." };
            }
            return new JsonResult(cliente);
        }

        public async Task<JsonResult> DoAdicionarClienteAsync([FromBody] Cliente cliente)
        {
            ClientesModel clienteModel = new ClientesModel();
            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Post, "Clientes", cliente);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clienteModel.Erro = new ErroModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clienteModel.Erro = JsonConvert.DeserializeObject<ErroModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clienteModel);
        }

        public async Task<JsonResult> DoEditarClienteAsync(int id, [FromBody] Cliente cliente)
        {
            ClientesModel clienteModel = new ClientesModel();
            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Put, $"Clientes/{id}", cliente);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clienteModel.Erro = new ErroModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clienteModel.Erro = JsonConvert.DeserializeObject<ErroModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clienteModel);
        }

        public async Task<JsonResult> DoExcluirTelefoneAsync([FromBody] ClientesModel clientes)
        {

            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Delete, $"Telefones/{clientes.IdTelefone}");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clientes.Erro = new ErroModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clientes.Erro = JsonConvert.DeserializeObject<ErroModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clientes);
        }

        public async Task<JsonResult> DoAdicionarTelefoneAsync(int id, [FromBody] Telefone telefone)
        {
            ClientesModel clientes = new ClientesModel();
            HttpResponseMessage response = RequestApi.SendRequest(HttpMethod.Put, $"Telefones/novoTelefone/{id}", telefone);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                JObject returnValues = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
                if (returnValues.GetValue("errors") != null)
                {
                    clientes.Erro = new ErroModel() { Mensagem = returnValues.GetValue("errors").First.First.First.Value<string>() };
                }
            }
            else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                clientes.Erro = JsonConvert.DeserializeObject<ErroModel>(await response.Content.ReadAsStringAsync());
            }

            return new JsonResult(clientes);
        }
    }
}