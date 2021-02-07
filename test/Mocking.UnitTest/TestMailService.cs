using Mocking.WebApi.Models;
using Mocking.WebApi.Services;
using Moq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;
using Xunit;

namespace Mocking.UnitTest
{
    public class TestMailService
    {
        [Fact]
        public void Authenticate()
        {
            var mailService = new MailService();
            Assert.True(mailService.Authenticate());
        }

        [Fact]
        public void IndependentAuthenticate()
        {
            var restClient = new Mock<IRestClient>();
            var restRequest = new Mock<IRestRequest>();
            var authenticator = new Mock<IAuthenticator>();

            var content = new Content()
            {
                resultObject = new Resultobject(),
                resultStatus = true
            };

            string contentString = JsonSerializer.Serialize(content);

            restClient.Setup(rc => rc.Execute(It.IsAny<IRestRequest>()))
               .Returns(new RestResponse() { StatusCode = System.Net.HttpStatusCode.OK, Content = contentString });

            var mailService = new MailService(restClient.Object, restRequest.Object, authenticator.Object);
            Assert.True(mailService.IndependentAuthenticate());
        }
    }
}
