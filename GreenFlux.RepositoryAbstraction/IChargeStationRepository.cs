using GreenFlux.Data.Models;
using System.Linq.Expressions;

namespace GreenFlux.RepositoryAbstraction
{
    public interface IChargeStationRepository
    {
        Task<IEnumerable<ChargeStation>> FindAll();

        Task<IEnumerable<ChargeStation>> FindByCondition(Expression<Func<ChargeStation, bool>> expression);

        Task Create(ChargeStation entity);

        Task Update(ChargeStation entity);

        Task Delete(ChargeStation entity);
    }
}