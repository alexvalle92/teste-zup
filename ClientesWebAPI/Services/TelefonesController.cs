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
    [Route("api/Telefones")]
    [ApiController]
    public class TelefonesController : ControllerBase
    {
        /// <summary>
        /// Inserir um Telefone
        /// </summary>
        /// <param name="idCliente">ID do Cliente</param>
        /// <param name="telefone">Modelo do Telefone</param>
        /// <response code="200">Telefone adicionado com sucesso</response>
        /// <response code="500">Ocorreu um erro ao adicionar o telefone</response>
        /// <response code="400">Modelo do Telefone enviado é inválido</response>
        /// <response code="404">Não foi possível encontrar o cliente</response>
        [HttpPut("novoTelefone/{idCliente}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 400)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Post([FromRoute] int idCliente, Telefone telefone)
        {
            try
            {
                using (var db = new ZupContext())
                {

                    Cliente cliente = db.Cliente.Include(i => i.Telefones).Where(w => w.Id.Equals(idCliente)).FirstOrDefault();
                    if(cliente != null)
                    {
                        cliente.Telefones.Add(telefone);
                        db.SaveChanges();
                    }
                    else
                    {
                        ErroModel erro = new ErroModel()
                        {
                            Mensagem = $"Não foi possível encontrar o cliente com o ID {idCliente}"
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

        /// <summary>
        /// Atualização de Telefone
        /// </summary>
        /// <param name="idCliente">ID do Cliente</param>
        /// <param name="idTelefone">ID do Telefone</param>
        /// <param name="telefone">Modelo do Telefone</param>
        /// <response code="200">Atualização do Telefone efetuado com sucesso</response>
        /// <response code="500">Ocorreu um erro ao atualizar o Telefone</response>
        /// <response code="400">Modelo do Telefone enviado é inválido</response>
        /// <response code="404">Não foi possível encontrar o Telefone ou Cliente</response>
        [HttpPut("{idCliente}/{idTelefone}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 400)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Put([FromRoute] int idCliente, [FromRoute] int idTelefone, Telefone telefone)
        {
            try
            {
                bool valido = ModelState.IsValid;
                Cliente cli = new Cliente();
                ErroModel erro = null;

                using (var db = new ZupContext())
                {
                    if(!db.Cliente.Where(w => w.Id.Equals(idCliente)).Any())
                    {
                        erro = new ErroModel()
                        {
                            Mensagem = $"Não foi possível encontrar o cliente com o ID {idCliente}"
                        };
                        return NotFound(erro);
                    }

                    if (!db.Telefone.Where(w => w.Id.Equals(idTelefone)).Any())
                    {
                        erro = new ErroModel()
                        {
                            Mensagem = $"Não foi possível encontrar o telefone com o ID {idCliente}"
                        };
                        return NotFound(erro);
                    }


                    telefone.Id = idTelefone;
                    telefone.ClienteId = idCliente;
                    db.Entry(telefone).State = EntityState.Modified;
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
        /// Excluir Telefone
        /// </summary>
        /// <param name="id">ID do Telefone</param>
        /// <response code="200">Registro excluido com sucesso</response>
        /// <response code="500">Ocorreu um erro ao remover o telefone</response>
        /// <response code="404">Não foi possível encontrar o telefone</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        [ProducesResponseType(typeof(ErroModel), 404)]
        public IActionResult Delete(int id)
        {
            try
            {
                Telefone telefone = new Telefone();
                using (var db = new ZupContext())
                {

                    telefone = db.Telefone.Where(w => w.Id.Equals(id)).FirstOrDefault();
                    if (telefone != null)
                    {
                        db.Attach(telefone);
                        db.Remove(telefone);
                        db.SaveChanges();
                    }
                    else
                    {
                        ErroModel erro = new ErroModel()
                        {
                            Mensagem = $"Não foi possível encontrar o telefone com o ID {id}"
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