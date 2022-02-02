using GreenFlux.Data.Models;
using System.Linq.Expressions;

namespace GreenFlux.RepositoryAbstraction
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> FindAll();

        Task<IEnumerable<Group>> FindByCondition(Expression<Func<Group, bool>> expression);

        Task Create(Group entity);

        Task Update(Group entity);

        Task Delete(Group entity);
    }
}