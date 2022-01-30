using GreenFlux.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.Test
{
    public class PreparationDbTest
    {
        public List<Group> _groups;
        public List<ChargeStation> _chargeStations;
        public List<Connector> _connectors;

        public PreparationDbTest()
        {
            _groups = new List<Group>()
            {
                new Group(1, "First Test Group", 390),
                new Group(2, "Second Test Group", 130),
                new Group(3, "Third Test Group", 100)
            };

            _chargeStations = new List<ChargeStation>()
            {
                new ChargeStation(1, "First ChargeStation", 1),
                new ChargeStation(2, "Second ChargeStation", 1),
                new ChargeStation(3, "Third ChargeStation", 2)
            };

            _connectors = new List<Connector>()
            {
                new Connector(1, 1, 150),
                new Connector(2, 1, 130),
                new Connector(1, 2, 100),
                new Connector(1, 3, 100),
                new Connector(2, 3, 5),
                new Connector(3, 3, 5),
                new Connector(4, 3, 5),
                new Connector(5, 3, 5)
            };
        }
    }
}
