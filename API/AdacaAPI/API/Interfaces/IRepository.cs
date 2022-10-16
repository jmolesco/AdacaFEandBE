using System;

namespace Adaca.Api.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
    }
}
