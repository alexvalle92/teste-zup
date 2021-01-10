using ClientesWebAPI;
using ClientesWebAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace ClientesWebAPITest
{
    public class TokenControllerTest
    {
        public TokenControllerTest()
        {
            RequestApi.ConfigRequest();
        }

        [Theory]
        [ClassData(typeof(TokenPostOk))]
        public void Post_OkResult(AutenticacaoModel autenticacao)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Post, $"Token", autenticacao);
            Assert.Equal((int)HttpStatusCode.OK, (int)resposta.StatusCode);
        }

        [Theory]
        [ClassData(typeof(TokenPostNotFound))]
        public void Post_NotFound(AutenticacaoModel autenticacao)
        {
            HttpResponseMessage resposta = RequestApi.SendRequest(HttpMethod.Post, $"Token", autenticacao);
            Assert.Equal((int)HttpStatusCode.NotFound, (int)resposta.StatusCode);
        }

        public class TokenPostOk : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new AutenticacaoModel { Email = "a.rvalle1@hotmail.com", Senha = "senha123" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class TokenPostNotFound : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new AutenticacaoModel { Email = "222222@hotmail.com", Senha = "3245senha" } };

                yield return new object[] { new AutenticacaoModel { Email = "33333@hotmail.com", Senha = "3245senha" } };
            }
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
