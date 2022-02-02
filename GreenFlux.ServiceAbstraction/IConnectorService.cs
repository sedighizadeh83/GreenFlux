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
        Task<IEnumerable<ConnectorReadWithDetailDto>> GetAllConnectors();

        Task<ConnectorReadWithDetailDto> GetConnectorById(int connectorId, int chargeStationId);

        Task CreateConnector(ConnectorCreateDto connector);

        Task UpdateConnector(ConnectorUpdateDto connector);

        Task DeleteConnector(int connectorId, int chargeStationId);
    }
}
