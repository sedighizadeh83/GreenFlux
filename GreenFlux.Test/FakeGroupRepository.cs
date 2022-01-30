using GreenFlux.RepositoryAbstraction;
using GreenFlux.Data.Models;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Test
{
    public class FakeGroupRepository : IGroupRepository
    {
        PreparationDbTest dbTest = new PreparationDbTest();

        public void Create(Group entity)
        {
            var entityToAdd = new Group(4, entity.Name, entity.Capacity);
            dbTest._groups.Add(entityToAdd);
        }

        public void Delete(Group entity)
        {
            var entityToRemove = new Group(3, entity.Name, entity.Capacity);
            dbTest._groups.Remove(entityToRemove);
        }

        public IQueryable<Group> FindAll()
        {
            var res = (from g in dbTest._groups
                       let chargeStations = (from ch in dbTest._chargeStations
                                             where ch.GroupId == g.Id
                                             let connectors = (from co in dbTest._connectors
                                                               where co.ChargeStationId == ch.Id
                                                               select new Connector()
                                                               {
                                                                   Id = co.Id,
                                                                   ChargeStationId = ch.Id,
                                                                   MaxCurrent = co.MaxCurrent,
                                                               })
                                             select new ChargeStation()
                                             {
                                                 Id = ch.Id,
                                                 Name = ch.Name,
                                                 ConnectorCollection = connectors.ToList()
                                             })
                        select new Group()
                        {
                            Id = g.Id,
                            Name = g.Name,
                            Capacity = g.Capacity,
                            ChargeStationCollection = chargeStations.ToList()
                        });

            return res.AsQueryable();
            
        }

        public IQueryable<Group> FindByCondition(Expression<Func<Group, bool>> expression)
        {
            var condition = expression.Compile();
            var res = (from g in dbTest._groups
                       where condition.Invoke(g)
                       let chargeStations = (from ch in dbTest._chargeStations
                                             where ch.GroupId == g.Id
                                             let connectors = (from co in dbTest._connectors
                                                               where co.ChargeStationId == ch.Id
                                                               select new Connector()
                                                               {
                                                                   Id = co.Id,
                                                                   ChargeStationId = ch.Id,
                                                                   MaxCurrent = co.MaxCurrent,
                                                               })
                                             select new ChargeStation()
                                             {
                                                 Id = ch.Id,
                                                 Name = ch.Name,
                                                 ConnectorCollection = connectors.ToList()
                                             })
                       select new Group()
                       {
                           Id = g.Id,
                           Name = g.Name,
                           Capacity = g.Capacity,
                           ChargeStationCollection = chargeStations.ToList()
                       });

            return res.AsQueryable();
            //return dbTest._groups.AsQueryable().Where(expression).AsNoTracking();
        }

        public void Update(Group entity)
        {
            var entityToUpdate = dbTest._groups.Where(g => g.Id == entity.Id).ToList().FirstOrDefault();

            typeof(Group).GetProperty("Name").SetValue(entityToUpdate, entity.Name);

            typeof(Group).GetProperty("Capacity").SetValue(entityToUpdate, entity.Capacity);
        }
    }
}
