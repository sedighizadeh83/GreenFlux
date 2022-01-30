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
        public void Create(Group entity)
        {
            this._context.Set<Group>().Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(Group entity)
        {
            this._context.Set<Group>().Remove(entity);
            this._context.SaveChanges();
        }

        public IQueryable<Group> FindAll()
        {
            return this._context.Set<Group>().AsNoTracking()
                .Include(g => g.ChargeStationCollection)
                .ThenInclude(g => g.ConnectorCollection);
        }

        public IQueryable<Group> FindByCondition(Expression<Func<Group, bool>> expression)
        {
            return this._context.Set<Group>().Where(expression).AsNoTracking()
                .Include(g => g.ChargeStationCollection)
                .ThenInclude(g => g.ConnectorCollection);
        }

        public void Update(Group entity)
        {
            this._context.Set<Group>().Update(entity);
            this._context.SaveChanges();
        }
    }
}