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
        public async Task Create(ChargeStation entity)
        {
            this._context.Set<ChargeStation>().Add(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task Delete(ChargeStation entity)
        {
            this._context.Set<ChargeStation>().Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ChargeStation>> FindAll()
        {
            return await this._context.Set<ChargeStation>().AsNoTracking()
                .Include(g => g.Group)
                .Include(g => g.ConnectorCollection)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChargeStation>> FindByCondition(Expression<Func<ChargeStation, bool>> expression)
        {
            return await this._context.Set<ChargeStation>().Where(expression).AsNoTracking()
                .Include(g => g.Group)
                .Include(g => g.ConnectorCollection)
                .ToListAsync();
        }

        public async Task Update(ChargeStation entity)
        {
            this._context.Set<ChargeStation>().Update(entity);
            await this._context.SaveChangesAsync();
        }
    }
}
