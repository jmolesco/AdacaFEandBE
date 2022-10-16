using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Adaca.Api.Repository.Generic
{
    public class BaseRepositoryDependencies<T> : IBaseRepositoryDependencies<T>
    {
        public BaseRepositoryDependencies(ILogger<T> logger, IConfiguration configuration,
            IMapper mapper)
        {
            Mapper = mapper;
            Logger = logger;
            Configuration = configuration;
        }

        public IMapper Mapper { get; private set; }
        public ILogger<T> Logger { get; private set; }
        public IConfiguration Configuration { get; }
    }
}
