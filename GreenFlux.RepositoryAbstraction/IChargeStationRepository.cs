using GreenFlux.Data.Models;
using System.Linq.Expressions;

namespace GreenFlux.RepositoryAbstraction
{
    public interface IChargeStationRepository
    {
        IQueryable<ChargeStation> FindAll();

        IQueryable<ChargeStation> FindByCondition(Expression<Func<ChargeStation, bool>> expression);

        void Create(ChargeStation entity);

        void Update(ChargeStation entity);

        void Delete(ChargeStation entity);
    }
}