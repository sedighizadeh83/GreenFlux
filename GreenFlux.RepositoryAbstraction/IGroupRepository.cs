using GreenFlux.Data.Models;
using System.Linq.Expressions;

namespace GreenFlux.RepositoryAbstraction
{
    public interface IGroupRepository
    {
        IQueryable<Group> FindAll();

        IQueryable<Group> FindByCondition(Expression<Func<Group, bool>> expression);

        void Create(Group entity);

        void Update(Group entity);

        void Delete(Group entity);
    }
}