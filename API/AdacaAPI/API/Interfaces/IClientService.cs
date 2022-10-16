using Adaca.Api.DTO;
using Adaca.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adaca.Api.Interfaces
{
    public interface IClientService
    {
        Task<ClientResponse> SubmitLoan(InsertClientDTO dtoMode);
        Task<List<ClientListResponse>> GetClientList(SearchClientDTO dtoModel);
    }
}
