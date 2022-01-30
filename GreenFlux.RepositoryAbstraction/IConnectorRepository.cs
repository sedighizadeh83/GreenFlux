using GreenFlux.Data.Models;
using System.Linq.Expressions;

namespace GreenFlux.RepositoryAbstraction
{
    public interface IConnectorRepository
    {
        IQueryable<Connector> FindAll();

        IQueryable<Connector> FindByCondition(Expression<Func<Connector, bool>> expression);

        void Create(Connector entity);

        void Update(Connector entity);

        void Delete(Connector entity);
    }
}