using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GreenFlux.Data;
using GreenFlux.Data.Models;
using GreenFlux.RepositoryAbstraction;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Repository
{
    public sealed class ChargeStationRepository : IChargeStationRepository
    {
        private readonly AppDbContext _context;

        public ChargeStationRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(ChargeStation entity)
        {
            this._context.Set<ChargeStation>().Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(ChargeStation entity)
        {
            this._context.Set<ChargeStation>().Remove(entity);
            this._context.SaveChanges();
        }

        public IQueryable<ChargeStation> FindAll()
        {
            return this._context.Set<ChargeStation>().AsNoTracking()
                .Include(g => g.Group)
                .Include(g => g.ConnectorCollection);
        }

        public IQueryable<ChargeStation> FindByCondition(Expression<Func<ChargeStation, bool>> expression)
        {
            return this._context.Set<ChargeStation>().Where(expression).AsNoTracking()
                .Include(g => g.Group)
                .Include(g => g.ConnectorCollection);
        }

        public void Update(ChargeStation entity)
        {
            this._context.Set<ChargeStation>().Update(entity);
            this._context.SaveChanges();
        }
    }
}
