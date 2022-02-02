using GreenFlux.RepositoryAbstraction;
using GreenFlux.Data.Models;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GreenFlux.Test
{
    public class FakeGroupRepository : IGroupRepository
    {
        PreparationDbTest dbTest = new PreparationDbTest();

        public async Task Create(Group entity)
        {
            var entityToAdd = new Group(4, entity.Name, entity.Capacity);
            dbTest._groups.Add(entityToAdd);
        }

        public async Task Delete(Group entity)
        {
            var entityToRemove = new Group(3, entity.Name, entity.Capacity);
            dbTest._groups.Remove(entityToRemove);
        }

        public async Task<IEnumerable<Group>> FindAll()
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

            return res.ToList();
            
        }

        public async Task<IEnumerable<Group>> FindByCondition(Expression<Func<Group, bool>> expression)
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

            return res.ToList();
        }

        public async Task Update(Group entity)
        {
            var entityToUpdate = dbTest._groups.Where(g => g.Id == entity.Id).ToList().FirstOrDefault();

            typeof(Group).GetProperty("Name").SetValue(entityToUpdate, entity.Name);

            typeof(Group).GetProperty("Capacity").SetValue(entityToUpdate, entity.Capacity);
        }
    }
}
