using GreenFlux.Data;
using GreenFlux.Data.Models;
using GreenFlux.RepositoryAbstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GreenFlux.Repository
{
    public sealed class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Group entity)
        {
            this._context.Set<Group>().Add(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task Delete(Group entity)
        {
            this._context.Set<Group>().Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Group>> FindAll()
        {
            return await this._context.Set<Group>().AsNoTracking()
                .Include(g => g.ChargeStationCollection)
                .ThenInclude(g => g.ConnectorCollection)
                .ToListAsync();
        }

        public async Task<IEnumerable<Group>> FindByCondition(Expression<Func<Group, bool>> expression)
        {
            return await this._context.Set<Group>().Where(expression).AsNoTracking()
                .Include(g => g.ChargeStationCollection)
                .ThenInclude(g => g.ConnectorCollection)
                .ToListAsync();
        }

        public async Task Update(Group entity)
        {
            this._context.Set<Group>().Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}