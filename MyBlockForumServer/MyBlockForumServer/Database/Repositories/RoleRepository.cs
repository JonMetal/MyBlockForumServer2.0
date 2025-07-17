using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public class RoleRepository : AbstractRepository<Role>
    {
        public RoleRepository() : base() { }
        public override async Task<Role> CreateAsync(Role entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _db.Roles.AddAsync(entity);
            return entity;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _db.Roles.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public override async Task<Role?> GetAsync(Guid id)
        {
            return await _db.Roles.FirstOrDefaultAsync(l => l.Id == id);
        }
        public override async Task<Role?> GetAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _db.Roles.FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _db.Roles.ToArrayAsync();
        }

        public override async Task<IEnumerable<Role>> GetAllAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _db.Roles.Where(predicate).ToArrayAsync();
        }

        public override Task UpdateAsync(Role entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _db.Roles.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
