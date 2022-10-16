using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adaca.Api.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);        
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entity);
    }
}
