using Adaca.Api.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adaca.Api.Interfaces
{
    public interface IClientRepository : IGenericRepository<Client>
    {
         Task<List<Client>> RetrieveClient();
    }
}
