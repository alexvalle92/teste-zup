using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClientesWebAPI.Entity;
using ClientesWebAPI.Helpers;
using ClientesWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClientesWebAPI.Services
{
    [Route("api/Token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Efetuar autenticação 
        /// </summary>
        /// <param name="autenticacao">Modelo Autenticacao</param>
        /// <response code="200">Token enviado com sucesso</response>
        /// <response code="500">Ocorreu um erro ao enviar requisição de token</response>
        /// <response code="404">Email ou senha inválidos</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErroModel), 500)]
        public IActionResult RequestToken([FromBody] AutenticacaoModel autenticacao)
        {
            try
            {
                Cliente cli = null;

                using (var db = new ZupContext())
                {
                    string senha = UtilsHelper.CriptografaSenha(autenticacao.Senha);
                    cli = db.Cliente.Where(w => w.Email.Equals(autenticacao) && w.Senha.Equals(senha)).FirstOrDefault();
                }

                if (cli != null)
                {
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Email, autenticacao.Email),
                    new Claim(ClaimTypes.Role, "Admin"),
                };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "teste",
                        audience: "teste",
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }

                return BadRequest("Email ou senha estão inválidos.");
            } catch(Exception er)
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