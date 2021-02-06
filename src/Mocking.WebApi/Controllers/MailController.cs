using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mocking.WebApi.Interfaces;
using System.Threading.Tasks;

namespace Mocking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {


        private readonly ILogger<MailController> _logger;
        private readonly IMailService _mailService;

        public MailController(ILogger<MailController> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [HttpGet("authenticate")]
        public Task<bool> Authenticate()
        {
            return Task.FromResult(_mailService.Authenticate());
        }

        [HttpGet("independentauthenticate")]
        public Task<bool> IndependentAuthenticate()
        {
            return Task.FromResult(_mailService.IndependentAuthenticate());
        }
    }
}
