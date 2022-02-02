using GreenFlux.Data.Models;
using System.Linq.Expressions;

namespace GreenFlux.RepositoryAbstraction
{
    public interface IConnectorRepository
    {
        Task<IEnumerable<Connector>> FindAll();

        Task<IEnumerable<Connector>> FindByCondition(Expression<Func<Connector, bool>> expression);

        Task Create(Connector entity);

        Task Update(Connector entity);

        Task Delete(Connector entity);
    }
}