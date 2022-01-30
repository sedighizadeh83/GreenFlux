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
    public sealed class ConnectorRepository : IConnectorRepository
    {
        private readonly AppDbContext _context;

        public ConnectorRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Connector entity)
        {
            this._context.Set<Connector>().Add(entity);
            this._context.SaveChanges();
        }

        public void Delete(Connector entity)
        {
            this._context.Set<Connector>().Remove(entity);
            this._context.SaveChanges();
        }

        public IQueryable<Connector> FindAll()
        {
            return this._context.Set<Connector>().AsNoTracking()
                .Include(g => g.ChargeStation);
        }

        public IQueryable<Connector> FindByCondition(Expression<Func<Connector, bool>> expression)
        {
            return this._context.Set<Connector>().Where(expression).AsNoTracking()
                .Include(g => g.ChargeStation);
        }

        public void Update(Connector entity)
        {
            this._context.Set<Connector>().Update(entity);
            this._context.SaveChanges();
        }
    }
}
