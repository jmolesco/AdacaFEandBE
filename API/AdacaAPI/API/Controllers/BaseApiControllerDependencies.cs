using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Adaca.Api.Controllers
{
    public interface IBaseApiControllerDependencies<T>
    {
        IMapper Mapper { get; }
        IConfiguration Configuration { get; }
        ILogger<T> Logger { get; }
    }

}


namespace Adaca.Api.Controllers.Internal
{
    public class BaseApiControllerDependencies<T> : IBaseApiControllerDependencies<T>
    {
        public BaseApiControllerDependencies(IConfiguration _configuration, IMapper _mapper, ILogger<T> _logger)
        {
            Mapper = _mapper;
            Configuration = _configuration;
            Logger = _logger;
        }
        public IMapper Mapper { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public ILogger<T> Logger { get; private set; }
    }
}
