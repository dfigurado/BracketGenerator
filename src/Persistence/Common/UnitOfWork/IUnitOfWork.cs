using Persistence.Common.Repository;

namespace Persistence.Common.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;

        void SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
