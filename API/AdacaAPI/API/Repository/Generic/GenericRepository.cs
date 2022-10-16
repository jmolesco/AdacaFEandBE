using Adaca.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Adaca.Api.Repository.Generic
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {    
        protected readonly DbContext _context;

        public GenericRepository(DbContext context)
        {         
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entity)
        {
           await _context.Set<T>().AddRangeAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entity)
        {
            _context.Set<T>().UpdateRange(entity);
        }
    }
}
