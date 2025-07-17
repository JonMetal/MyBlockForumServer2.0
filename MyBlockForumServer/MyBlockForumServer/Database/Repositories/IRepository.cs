using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        public Task<T> CreateAsync(T entity);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(T entity);

        public Task<T?> GetAsync(Guid id);

        public Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        Task SaveAsync();
    }
}
