using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Adaca.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<T> _logger;

        public BaseApiController(IBaseApiControllerDependencies<T> dependencies)
        {
            _configuration = dependencies.Configuration;
            _mapper = dependencies.Mapper;
            _logger = dependencies.Logger;
        }

    }
}
