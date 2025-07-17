using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public class UserRepository : AbstractRepository<User>
    {
        public override async Task<User> CreateAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _db.Users.AddAsync(entity);
            return entity;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _db.Users.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public override async Task<User?> GetAsync(Guid id)
        {
            return await _db.Users.Include(l => l.Status).Include(l => l.FromUsers).Include(l => l.Threads).FirstOrDefaultAsync(l => l.Id == id);
        }

        public override async Task<User?> GetAsync(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users.Include(l => l.Status).Include(l => l.FromUsers).Include(l => l.Threads).FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users.Include(l => l.Status).ToArrayAsync();
        }

        public override async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users.Include(l => l.Status).Where(predicate).ToArrayAsync();
        }

        public override Task UpdateAsync(User entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _db.Users.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
