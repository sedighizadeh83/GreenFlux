using GreenFlux.DTO.Connector;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using System.Collections.Generic;
using GreenFlux.Service;
using AutoMapper;

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
        public void CreateConnector(ConnectorCreateDto connector)
        {
            _service.CreateConnector(connector);
        }

        public void DeleteConnector(int connectorId, int chargeStationId)
        {
            _service.DeleteConnector(connectorId, chargeStationId);
        }

        public IEnumerable<ConnectorReadWithDetailDto> GetAllConnectors()
        {
            return _service.GetAllConnectors();
        }

        public ConnectorReadWithDetailDto GetConnectorById(int connectorId, int chargeStationId)
        {
            return _service.GetConnectorById(connectorId, chargeStationId);
        }

        public void UpdateConnector(ConnectorUpdateDto connector)
        {
            _service.UpdateConnector(connector);
        }
    }
}
