using Microsoft.EntityFrameworkCore;
using MyBlockForumServer.Database.Entities;
using System.Linq.Expressions;

namespace MyBlockForumServer.Database.Repositories
{
    public class StatusRepository : AbstractRepository<Status>
    {
        public override async Task<Status> CreateAsync(Status entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            await _db.Statuses.AddAsync(entity);
            return entity;
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _db.Statuses.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public override async Task<Status?> GetAsync(Guid id)
        {
            return await _db.Statuses.FirstOrDefaultAsync(l => l.Id == id);
        }

        public override async Task<Status?> GetAsync(Expression<Func<Status, bool>> predicate)
        {
            return await _db.Statuses.FirstOrDefaultAsync(predicate);
        }

        public override async Task<IEnumerable<Status>> GetAllAsync()
        {
            return await _db.Statuses.ToArrayAsync();
        }

        public override async Task<IEnumerable<Status>> GetAllAsync(Expression<Func<Status, bool>> predicate)
        {
            return await _db.Statuses.ToArrayAsync();
        }

        public override Task UpdateAsync(Status entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _db.Statuses.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
