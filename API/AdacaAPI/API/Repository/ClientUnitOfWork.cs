using Adaca.Api.DataModels;
using Adaca.Api.Interfaces;
using System;
using System.Threading.Tasks;

namespace Adaca.Api.Repository
{
    public class ClientUnitOfWork : IClientUnitOfWork
    {
        public IClientRepository clientRepository { get; }

        private readonly ClientDBContext _clientDBContext;
        public ClientUnitOfWork(ClientDBContext clientDBContext,
            IClientRepository _clientRepository
                   )
        {
            _clientDBContext = clientDBContext;
            clientRepository = _clientRepository;
        }
        public int Save()
        {
            return _clientDBContext.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _clientDBContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientDBContext.Dispose();
            }
        }
    }
}
