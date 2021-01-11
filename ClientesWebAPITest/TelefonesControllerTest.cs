using ClientesWebAPI;
using ClientesWebAPI.Entity;
using ClientesWebAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ClientesWebAPITest
{
    public class TelefonesControllerTest
    {

        public TelefonesControllerTest()
        {
            RequestApi.ConfigRequest(@"https://localhost:44390/api/");
        }

        [Theory]
        [ClassData(typeof(TelefoneDataCorreta))]
        public void Put_OkResult(Telefone telefone)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Telefones/{telefone.ClienteId}/{telefone.Id}", telefone);
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(TelefoneNotFound))]
        public void Put_NotFoundResult(Telefone telefone)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Telefones/{telefone.ClienteId}/{telefone.Id}", telefone);
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(TelefoneBadRequest))]
        public void Put_Errodata(Telefone telefone)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Telefones/{telefone.ClienteId}/{telefone.Id}", telefone);
            Assert.Equal((int)HttpStatusCode.BadRequest, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(NovoTelefone))]
        public void Put_OkNovoTelefone(Telefone telefone)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Telefones/novoTelefone/{telefone.ClienteId}", telefone);
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(TelefoneNotFound))]
        public void Put_NotFoundRes(Telefone telefone)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Put, $"Telefones/novoTelefone/{telefone.ClienteId}", telefone);
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        public void Delete_NotFoundResult(int id)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Delete, $"Telefones/{id}");
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Delete_OkResult(int id)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Delete, $"Telefones/{id}");
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }
        
        #region Dados para testes
        public class TelefoneDataCorreta : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Telefone { NumeroTelefone = "(41)1234-1234", Id = 2, ClienteId = 3 } };
                yield return new object[] { new Telefone { NumeroTelefone = "(41)3434-8888", Id = 2, ClienteId = 3 } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class TelefoneNotFound : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Telefone { NumeroTelefone = "(41)3434-8888", Id = 6, ClienteId = 6 } };
                yield return new object[] { new Telefone { NumeroTelefone = "(41)3434-8888", Id = 5, ClienteId = 5 } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class TelefoneBadRequest : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Telefone { NumeroTelefone = "(41)45-45", Id = 2, ClienteId = 3 } };
                yield return new object[] { new Telefone { NumeroTelefone = "(41)454-445", Id = 2, ClienteId = 3 } };
                yield return new object[] { new Telefone { NumeroTelefone = "(41)454-44445", Id = 2, ClienteId = 3 } };
                yield return new object[] { new Telefone { NumeroTelefone = "", Id = 2, ClienteId = 3 } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class NovoTelefone : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Telefone { NumeroTelefone = "(41)9877-3214", ClienteId = 3 } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        #endregion


    }
}
