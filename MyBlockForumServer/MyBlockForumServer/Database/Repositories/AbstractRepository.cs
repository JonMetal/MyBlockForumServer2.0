using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected MyBlockForumDbContext _db;
        private bool _disposed = false;

        protected AbstractRepository()
        {
            _db = new();
        }

        public abstract Task<T> CreateAsync(T entity);

        public abstract Task DeleteAsync(Guid id);

        public abstract Task UpdateAsync(T entity);

        public abstract Task<T?> GetAsync(Guid id);

        public abstract Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

        public abstract Task<IEnumerable<T>> GetAllAsync();

        public abstract Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        public virtual async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _db.Dispose();
            }
            _disposed = true;
        }

        ~AbstractRepository()
        {
            Dispose(false);
        }
    }
}
