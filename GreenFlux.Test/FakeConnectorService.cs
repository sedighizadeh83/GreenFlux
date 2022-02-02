using GreenFlux.DTO.Connector;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using System.Collections.Generic;
using GreenFlux.Service;
using AutoMapper;
using System.Threading.Tasks;

namespace GreenFlux.Test
{
    public class FakeConnectorService : IConnectorService
    {
        private readonly ConnectorService _service;
        private readonly IConnectorRepository _repository;
        private readonly IChargeStationRepository _chargeStationRepository;
        private readonly IMapper _mapper;

        public FakeConnectorService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new FakeConnectorRepository();
            _chargeStationRepository = new FakeChargeStationRepository();
            _service = new ConnectorService(_repository, _mapper, _chargeStationRepository);
        }
        public async Task CreateConnector(ConnectorCreateDto connector)
        {
            await _service.CreateConnector(connector);
        }

        public async Task DeleteConnector(int connectorId, int chargeStationId)
        {
            await _service.DeleteConnector(connectorId, chargeStationId);
        }

        public async Task<IEnumerable<ConnectorReadWithDetailDto>> GetAllConnectors()
        {
            return await _service.GetAllConnectors();
        }

        public async Task<ConnectorReadWithDetailDto> GetConnectorById(int connectorId, int chargeStationId)
        {
            return await _service.GetConnectorById(connectorId, chargeStationId);
        }

        public async Task UpdateConnector(ConnectorUpdateDto connector)
        {
            await _service.UpdateConnector(connector);
        }
    }
}
