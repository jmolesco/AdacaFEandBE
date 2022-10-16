using Adaca.Api.DTO;
using Adaca.Api.Extentions;
using Adaca.Api.Infrastructure.Enum;
using Adaca.Api.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Adaca.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ClientController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IClientService _clientService;
        public ClientController(ILogger<ClientController> logger, IConfiguration configuration, IMapper mapper
            , IClientService clientService)
        {
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
            _clientService = clientService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitLoan([FromBody] InsertClientDTO dtoModel)
        {
            var result = await _clientService.SubmitLoan(dtoModel);

            if (result.Decision != EnumDecisionType.Qualified.ToString())
                return result.Pipe(res => BadRequest(res));

            return result.Pipe(res => Ok(res));
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetLoanList([FromBody] SearchClientDTO dtoModel)
        {
            var result = await _clientService.GetClientList(dtoModel);
            return result.Pipe(res => Ok(res));
        }
    }
}
