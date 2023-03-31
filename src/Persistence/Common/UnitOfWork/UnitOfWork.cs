using Microsoft.EntityFrameworkCore;
using Persistence.Common.Repository;

namespace Persistence.Common.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BracketsDbContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(BracketsDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            Type type = typeof(T);

            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<T>(_context);
            }

            return (IRepository<T>)_repositories[type];
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Members.Any(p => p.Metadata.Name == "CreatedBy") && entry.Members.Any(p => p.Metadata.Name == "CreatedOn"))
                        {
                            entry.Property("CreatedBy").CurrentValue = "System";
                            entry.Property("CreatedOn").CurrentValue = DateTime.Now;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Members.Any(p => p.Metadata.Name == "ModifiedBy") && entry.Members.Any(p => p.Metadata.Name == "ModifiedOn"))
                        {
                            entry.Property("ModifiedBy").CurrentValue = "System";
                            entry.Property("ModifiedOn").CurrentValue = DateTime.Now;
                        }
                        break;
                    default:
                        break;
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}