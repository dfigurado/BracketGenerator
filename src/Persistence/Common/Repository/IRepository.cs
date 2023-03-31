
namespace Persistence.Common.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
    }
}