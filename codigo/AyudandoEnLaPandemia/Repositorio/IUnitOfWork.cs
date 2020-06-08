using System;

namespace Repositorio
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
