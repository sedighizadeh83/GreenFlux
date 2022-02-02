using GreenFlux.RepositoryAbstraction;
using GreenFlux.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GreenFlux.Test
{
    public class FakeChargeStationRepository : IChargeStationRepository
    {
        PreparationDbTest dbTest = new PreparationDbTest();

        public async Task Create(ChargeStation entity)
        {
            var entityToAdd = new ChargeStation(4, entity.Name, entity.GroupId);
            dbTest._chargeStations.Add(entityToAdd);
        }

        public async Task Delete(ChargeStation entity)
        {
            var entityToRemove = new ChargeStation(3, entity.Name, entity.GroupId);
            dbTest._chargeStations.Remove(entityToRemove);
        }

        public async Task<IEnumerable<ChargeStation>> FindAll()
        {
            var res = (from ch in dbTest._chargeStations
                       let connectors = (from co in dbTest._connectors
                                         where co.ChargeStationId == ch.Id
                                         select new Connector()
                                         {
                                             Id = co.Id,
                                             ChargeStationId = ch.Id,
                                             MaxCurrent = co.MaxCurrent,
                                         })
                       let parentGroup = (from g in dbTest._groups
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
                                          }).FirstOrDefault()
                       select new ChargeStation()
                       {
                           Id = ch.Id,
                           Name = ch.Name,
                           Group = parentGroup,
                           GroupId = parentGroup.Id,
                           ConnectorCollection = connectors.ToList()
                       });

            return res.ToList();
        }

        public async Task<IEnumerable<ChargeStation>> FindByCondition(Expression<Func<ChargeStation, bool>> expression)
        {
            var condition = expression.Compile();
            var res = (from ch in dbTest._chargeStations
                       where condition.Invoke(ch)
                       let connectors = (from co in dbTest._connectors
                                         where co.ChargeStationId == ch.Id
                                         select new Connector()
                                         {
                                             Id = co.Id,
                                             ChargeStationId = ch.Id,
                                             MaxCurrent = co.MaxCurrent,
                                         })
                       let parentGroup = (from g in dbTest._groups
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
                                          }).FirstOrDefault()
                       select new ChargeStation()
                       {
                           Id = ch.Id,
                           Name = ch.Name,
                           Group = parentGroup,
                           GroupId = parentGroup.Id,
                           ConnectorCollection = connectors.ToList()
                       });

            return res.ToList();
        }

        public async Task Update(ChargeStation entity)
        {
            var entityToUpdate = dbTest._chargeStations.Where(g => g.Id == entity.Id).ToList().FirstOrDefault();

            typeof(ChargeStation).GetProperty("Name").SetValue(entityToUpdate, entity.Name);

            typeof(ChargeStation).GetProperty("GroupId").SetValue(entityToUpdate, entity.GroupId);
        }
    }
}
