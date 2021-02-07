using Mocking.WebApi.Interfaces;
using Mocking.WebApi.Models;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;

namespace Mocking.WebApi.Services
{
    public class MailService : IMailService
    {
        private const string GetToken = "https://useapi.useinbox.com/token";

        private readonly IRestClient _restClient; 
        private readonly IRestRequest _restRequest; 
        private readonly IAuthenticator _authenticator;

        public MailService()
        {

        }
        public MailService(IRestClient restClient, IRestRequest restRequest, IAuthenticator authenticator)
        {
            _restClient = restClient;
            _restRequest = restRequest;
            _authenticator = authenticator;
        }

        public bool Authenticate()
        {
            var mailAccount = new
            {
                EmailAddress = "email",
                Password = "password"
            };

            string tokenParam = JsonSerializer.Serialize(mailAccount);

            var clientToken = new RestClient(GetToken);
            clientToken.Authenticator = new HttpBasicAuthenticator("username", "password");

            var requestToken = new RestRequest(Method.POST);
            requestToken.AddHeader("Content-Type", "application/json");
            requestToken.AddParameter("param", tokenParam, ParameterType.RequestBody);
            IRestResponse responsee = clientToken.Execute(requestToken);

            var contentToken = JsonSerializer.Deserialize<Content>(responsee.Content);

            return contentToken.resultStatus;
        }
        public bool IndependentAuthenticate()
        {
            var mailAccount = new
            {
                EmailAddress = "email",
                Password = "password"
            };

            string tokenParam = JsonSerializer.Serialize(mailAccount);

            _restClient.BaseUrl = new System.Uri(GetToken);
            _restClient.Authenticator = _authenticator;

            _restRequest.Method = Method.POST;
            _restRequest.AddHeader("Content-Type", "application/json");
            _restRequest.AddParameter("param", tokenParam, ParameterType.RequestBody);

            IRestResponse responsee = _restClient.Execute(_restRequest);

            var contentToken = JsonSerializer.Deserialize<Content>(responsee.Content);

            return contentToken.resultStatus;
        }
    }
}
