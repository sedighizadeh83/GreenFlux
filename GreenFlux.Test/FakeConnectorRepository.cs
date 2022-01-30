using GreenFlux.RepositoryAbstraction;
using GreenFlux.Data.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Test
{
    public class FakeConnectorRepository : IConnectorRepository
    {
        PreparationDbTest dbTest = new PreparationDbTest();

        public void Create(Connector entity)
        {
            var entityToAdd = new Connector(entity.Id, entity.ChargeStationId, entity.MaxCurrent);
            dbTest._connectors.Add(entityToAdd);
        }

        public void Delete(Connector entity)
        {
            var entityToRemove = new Connector(entity.Id, entity.ChargeStationId, entity.MaxCurrent);
            dbTest._connectors.Remove(entityToRemove);
        }

        public IQueryable<Connector> FindAll()
        {
            var res = (from co in dbTest._connectors
                       let parentChargeStation = (from ch in dbTest._chargeStations
                                                  where co.ChargeStationId == ch.Id
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
                                                      Group = parentGroup,
                                                      GroupId = parentGroup.Id,
                                                      ConnectorCollection = connectors.ToList()
                                                  }).FirstOrDefault()
                       select new Connector()
                       {
                           Id = co.Id,
                           ChargeStationId = parentChargeStation.Id,
                           ChargeStation = parentChargeStation,
                           MaxCurrent = co.MaxCurrent,
                       });

            return res.AsQueryable();
        }

        public IQueryable<Connector> FindByCondition(Expression<Func<Connector, bool>> expression)
        {
            var condition = expression.Compile();
            var res = (from co in dbTest._connectors
                       where condition.Invoke(co)
                       let parentChargeStation = (from ch in dbTest._chargeStations
                                                  where co.ChargeStationId == ch.Id
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
                                                      Group = parentGroup,
                                                      GroupId = parentGroup.Id,
                                                      ConnectorCollection = connectors.ToList()
                                                  }).FirstOrDefault()
                       select new Connector()
                       {
                           Id = co.Id,
                           ChargeStationId = parentChargeStation.Id,
                           ChargeStation = parentChargeStation,
                           MaxCurrent = co.MaxCurrent,
                       });

            return res.AsQueryable();
        }

        public void Update(Connector entity)
        {
            var entityToUpdate = dbTest._connectors.Where(g => g.Id == entity.Id && g.ChargeStationId == entity.ChargeStationId).ToList().FirstOrDefault();

            typeof(Connector).GetProperty("MaxCurrent").SetValue(entityToUpdate, entity.MaxCurrent);
        }
    }
}
