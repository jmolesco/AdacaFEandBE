using System;
using System.Threading.Tasks;

namespace Adaca.Api.Interfaces
{
    public interface IClientUnitOfWork : IDisposable
    {
        IClientRepository clientRepository { get; }
        int Save();
        Task<int> SaveAsync();
    }
}
