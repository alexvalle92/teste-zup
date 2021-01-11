using ClientesWebAPI;
using ClientesWebAPI.Entity;
using ClientesWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ClientesWebAPITest
{
    public class ClientesControllerTest
    {
        public ClientesControllerTest()
        {
            RequestApi.ConfigRequest(@"https://localhost:44390/api/");
        }

        [Fact]
        public void Get_OkResult()
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Get, $"Clientes");
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }
        
        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void GetById_OkResult(int id)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Get, $"Clientes/{id}");
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        public void GetById_NotFound(int id)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Get, $"Clientes/{id}");
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ClientePostOk))]
        public void Post_OkResult(Cliente cliente)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Post, $"Clientes", cliente);
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ClientePostBadRequest))]
        public void Post_BadRequest(Cliente cliente)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Post, $"Clientes", cliente);
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ClientePutOk))]
        public void Put_OkResult(Cliente cliente)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Clientes/{cliente.Id}", cliente);
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ClientePutBadRequest))]
        public void Put_BadRequest(Cliente cliente)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Clientes/{cliente.Id}", cliente);
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(ClientePutNotFound))]
        public void Put_NotFound(Cliente cliente)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Clientes/{cliente.Id}", cliente);
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(8)]
        public void Delete_OkResult(int id)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Delete, $"Clientes/{id}");
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        public void Delete_NotFound(int id)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Delete, $"Clientes/{id}");
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }



        #region Dados para testes
        public class ClientePostOk : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                List<Telefone> listaTelefones = new List<Telefone>();
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(11)1111-1111" });
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(22)2222-2222" });
                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle1@gmail.com", Senha = "senhateste123", Telefones = listaTelefones } };

                listaTelefones = new List<Telefone>();
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(32)1145-9874" });
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(21)95632-3214" });
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(55)32145-5823" });
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(22)65489-3697" });
                yield return new object[] { new Cliente { Nome = "Alexandre Rafael", Sobrenome = "Valle", Email = "alex.rvalle2@gmail.com", Senha = "senhateste321", Telefones = listaTelefones } };

                yield return new object[] { new Cliente { Nome = "Alexandre Valle", Sobrenome = "Rafael", Email = "alex.rvalle3@gmail.com", Senha = "465497as" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ClientePostBadRequest : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                List<Telefone> listaTelefones = new List<Telefone>();
                listaTelefones.Add(new Telefone() { NumeroTelefone = "(11)111-1111" });
                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle1@gmail.com", Senha = "senhateste123", Telefones = listaTelefones } };

                yield return new object[] { new Cliente { Nome = "", Sobrenome = "Valle", Email = "alex.rvalle2@gmail.com", Senha = "senhateste321" } };

                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "", Email = "alex.rvalle2@gmail.com", Senha = "senhateste321" } };

                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Valle", Email = "", Senha = "senhateste321" } };

                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle2@gmail.com", Senha = "" } };

                yield return new object[] { new Cliente { Nome = "Al", Sobrenome = "Valle", Email = "alex.rvalle2@gmail.com", Senha = "senhateste321" } };
                
                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Va", Email = "alex.rvalle2@gmail.com", Senha = "senhateste321" } };

                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Valle", Email = "emailInvalido", Senha = "senhateste321" } };

                yield return new object[] { new Cliente { Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle2@gmail.com", Senha = "5digi" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ClientePutOk : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Cliente { Id = 6, Nome = "Alexandre 22", Sobrenome = " 3Valle", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 8, Nome = "Alexandre 11", Sobrenome = " 2Valle", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ClientePutBadRequest : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Cliente { Id = 6, Nome = "", Sobrenome = " 3Valle", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 6, Nome = "Alexandre", Sobrenome = "", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 6, Nome = "Alexandre", Sobrenome = "Valle", Email = "", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 6, Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle1@gmail.com", Senha = "" } };

                yield return new object[] { new Cliente { Id = 8, Nome = "Al", Sobrenome = " 2Valle", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 8, Nome = "Alexandre", Sobrenome = "Va", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 8, Nome = "Alexandre", Sobrenome = "Valle", Email = "emailInvalido", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 8, Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle0@gmail.com", Senha = "55555" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ClientePutNotFound : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Cliente { Id = 100, Nome = "Alexandre", Sobrenome = " Valle", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };

                yield return new object[] { new Cliente { Id = 101, Nome = "Alexandre", Sobrenome = "Valle", Email = "alex.rvalle0@gmail.com", Senha = "22222222222222" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        #endregion
    }
}
