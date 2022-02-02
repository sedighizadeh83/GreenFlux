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
        public async Task Create(Connector entity)
        {
            this._context.Set<Connector>().Add(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task Delete(Connector entity)
        {
            this._context.Set<Connector>().Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Connector>> FindAll()
        {
            return await this._context.Set<Connector>().AsNoTracking()
                .Include(g => g.ChargeStation)
                .ToListAsync();
        }

        public async Task<IEnumerable<Connector>> FindByCondition(Expression<Func<Connector, bool>> expression)
        {
            return await this._context.Set<Connector>().Where(expression).AsNoTracking()
                .Include(g => g.ChargeStation)
                .ToListAsync();
        }

        public async Task Update(Connector entity)
        {
            this._context.Set<Connector>().Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}
