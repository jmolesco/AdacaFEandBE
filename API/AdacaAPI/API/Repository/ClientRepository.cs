using Adaca.Api.DataModels;
using Adaca.Api.Infrastructure.Cache;
using Adaca.Api.Interfaces;
using Adaca.Api.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Adaca.Api.Repository
{
    public class ClientRepository : GenericRepository<Client>,  IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ClientDBContext _clientDBContext;
        public ClientRepository(ILogger<ClientRepository> logger, IConfiguration configuration, ICacheService cacheService, IHttpClientFactory clientFactory, ClientDBContext clientDBContext):base(clientDBContext)
        {
            _logger = logger;
            _configuration = configuration;
            _cacheService = cacheService;
            _clientFactory = clientFactory;
            _clientDBContext = clientDBContext;
        }

        public async Task<List<Client>> RetrieveClient()
        {
            _logger.LogInformation("WorkFlowTaskRepository - RetrieveClient - Started method ");
            _logger.LogInformation("WorkFlowTaskRepository - RetrieveClient");
           var  _result = await _clientDBContext.Clients
                                                           .OrderByDescending(x => x.TimeTrading)
                                                           .AsNoTracking()
                                                           .ToListAsync();
            _logger.LogInformation("WorkFlowTaskRepository - RetrieveWorkFlowTaskById");
            return _result;
        }
    }
}
