using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Adaca.Api.Services
{
    public class BaseService<T>
    {
        public BaseService(IBaseDependenciesService<T> dependencies)
        {
            Mapper = dependencies.Mapper;
            Logger = dependencies.Logger;
            Configuration = dependencies.Configuration;
        }

        public IMapper Mapper { get; private set; }
        public ILogger<T> Logger { get; private set; }
        public IConfiguration Configuration { get; }
    }
}
