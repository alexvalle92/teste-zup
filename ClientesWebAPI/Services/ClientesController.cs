using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ClientesWebAPI.Entity;
using ClientesWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientesWebAPI.Services
{
    [Route("api/Clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        /// <summary>
        /// Obter todos os Clientes
        /// </summary>
        /// <response code="200">A lista retornou todos os clientes com sucesso</response>
        /// <response code="500">O correu um erro ao obter a lista de clientes</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Cliente>), 200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        public IActionResult Get()
        {
            try
            {
                List<Cliente> listCli = new List<Cliente>();
                using (var db = new ZupContext())
                {
                    listCli = db.Cliente.ToList();
                }
                return Ok(listCli);
            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErroModel()
                {
                    Mensagem = er.Message,
                    StackTrace = er.StackTrace
                });
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
        [ProducesResponseType(typeof(Cliente), 200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Get(int id)
        {
            try
            {
                Cliente cli = new Cliente();

                using (var db = new ZupContext())
                {
                    cli = db.Cliente.Where(w => w.Id.Equals(id)).FirstOrDefault();
                    cli.Telefones = db.Telefone.Where(w => w.ClienteId.Equals(id)).ToList();
                }

                if (cli != null)
                {
                    return Ok(cli);
                }
                else
                {
                    ErroModel erro = new ErroModel()
                    {
                        Mensagem = $"Não foi possível encontrar o usuário com o ID {id}"
                    };
                    return NotFound(erro);
                }

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErroModel()
                {
                    Mensagem = er.Message,
                    StackTrace = er.StackTrace
                });
            }
        }

        /// <summary>
        /// Inserir um Cliente
        /// </summary>
        /// <param name="cliente">Modelo do Cliente</param>
        /// <response code="200">Cliente adicionado com sucesso</response>
        /// <response code="500">Ocorreu um erro ao adicionar o Cliente</response>
        /// <response code="400">Modelo do cliente enviado é inválido</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 400)]
        public IActionResult Post(Cliente cliente)
        {
            try
            {
                using (var db = new ZupContext())
                {
                    db.Cliente.Add(cliente);
                    db.SaveChanges();
                }

                return Ok();

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErroModel()
                {
                    Mensagem = er.Message,
                    StackTrace = er.StackTrace
                });
            }
        }

        /// <summary>
        /// Atualização de Cliente
        /// </summary>
        /// <param name="id">ID do cliente</param>
        /// <param name="cliente">Modelo do Cliente</param>
        /// <response code="200">Atualização do Cliente efetuado com sucesso</response>
        /// <response code="500">Ocorreu um erro ao atualizar o Cliente</response>
        /// <response code="400">Modelo do cliente enviado é inválido</response>
        /// <response code="404">Não foi possível encontrar o cliente</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 400)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Put([FromRoute] int id, Cliente cliente)
        {
            try
            {
                Cliente cli = new Cliente();

                using (var db = new ZupContext())
                {
                    cliente.Id = id;
                    db.Entry(cliente).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Ok();

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErroModel()
                {
                    Mensagem = er.Message,
                    StackTrace = er.StackTrace
                });
            }
        }

        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="id">ID do Cliente</param>
        /// <response code="200">Registro excluido com sucesso</response>
        /// <response code="500">Ocorreu um erro ao remover o cliente</response>
        /// <response code="404">Não foi possível encontrar o cliente</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Cliente cli = new Cliente();
                using (var db = new ZupContext())
                {
                    cli = db.Cliente.Where(w => w.Id.Equals(id)).FirstOrDefault();
                    cli.Telefones = db.Telefone.Where(w => w.ClienteId.Equals(id)).ToList();
                    if (cli != null)
                    {
                        db.Attach(cli);
                        db.Remove(cli);
                        db.SaveChanges();
                    }
                    else
                    {
                        ErroModel erro = new ErroModel()
                        {
                            Mensagem = $"Não foi possível encontrar o usuário com o ID {id}"
                        };
                        return NotFound(erro);
                    }
                }
                return Ok();

            }
            catch (Exception er)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new ErroModel()
                {
                    Mensagem = er.Message,
                    StackTrace = er.StackTrace
                });
            }
        }

    }
}