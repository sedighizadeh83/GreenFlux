using GreenFlux.DTO.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.ServiceAbstraction
{
    public interface IConnectorService
    {
        IEnumerable<ConnectorReadWithDetailDto> GetAllConnectors();

        ConnectorReadWithDetailDto GetConnectorById(int connectorId, int chargeStationId);

        void CreateConnector(ConnectorCreateDto connector);

        void UpdateConnector(ConnectorUpdateDto connector);

        void DeleteConnector(int connectorId, int chargeStationId);
    }
}
