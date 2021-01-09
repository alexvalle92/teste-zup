using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClientesWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        /// <summary>
        /// Obter todos os Clientes
        /// </summary>
        /// <response code="200">A lista retornou todos os clientes com sucesso</response>
        /// <response code="500">O correu um erro ao obter a lista de clientes</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClientesModel>), 200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        public IActionResult Get()
        {
            try
            {
                List<ClientesModel> listCli = new List<ClientesModel>();
                return Ok(listCli);
            }
            catch (Exception er)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Pesquisar Cliente
        /// </summary>
        /// <param name="id">Id do Cliente</param>
        /// <response code="200">Cliente retornou com sucesso</response>
        /// <response code="500">O correu um erro ao obter a lista de clientes</response>
        /// <response code="404">Não foi possível encontrar o cliente</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientesModel), 200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Get(int id)
        {
            try
            {
                ClientesModel cli = new ClientesModel();
                if (cli.teste == 0)
                {
                    return NotFound(new ErroModel() { Codigo = 1, Mensagem = $"Não foi possível encontrar o cliente com o ID { cli.teste }" });
                }
                else
                {
                    return Ok(cli);
                }
                
            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Inserir um Cliente
        /// </summary>
        /// <param name="cliente">Modelo do Cliente</param>
        /// <response code="200">Cliente adicionado com sucesso</response>
        /// <response code="500">O modelo do cliente enviado é inválido</response>
        /// <response code="400">Modelo do usuário enviado é inválido</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 400)]
        public IActionResult Post(ClientesModel cliente)
        {
            try
            {
                return Ok();

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Atualização de Cliente
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="cliente">Modelo do Cliente</param>
        /// <response code="200">Atualização do Cliente efetuado com sucesso</response>
        /// <response code="500">O modelo do cliente enviado é inválido</response>
        /// <response code="400">Modelo do usuário enviado é inválido</response>
        /// <response code="404">Não foi possível encontrar o usuário</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 400)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Put([FromRoute] int id, ClientesModel cliente)
        {
            try
            {
                return Ok();

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="id">ID do Cliente</param>
        /// <response code="200">Registro excluido com sucesso</response>
        /// <response code="500">O modelo do cliente enviado é inválido</response>
        /// <response code="404">Não foi possível encontrar o cliente</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok();

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}