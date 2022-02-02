using GreenFlux.DTO.Connector;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using AutoMapper;
using GreenFlux.Data.Models;
using GreenFlux.Service.Validators;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.Service
{
    public sealed class ConnectorService : IConnectorService
    {
        private readonly IConnectorRepository _repository;
        private readonly IMapper _mapper;
        private readonly IChargeStationRepository _chargeStationRepository;

        public ConnectorService(IConnectorRepository repository, IMapper mapper, IChargeStationRepository chargeStationRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _chargeStationRepository = chargeStationRepository;
        }

        public async Task CreateConnector(ConnectorCreateDto connector)
        {
            var connectorModel = _mapper.Map<Connector>(connector);
            var chargeStationEntity = await _chargeStationRepository.FindByCondition(cs => cs.Id == connector.ChargeStationId);
            if (chargeStationEntity.Count() > 0)
            {
                var validationResult = ConnectorValidator.ValidateForCreate(connectorModel, chargeStationEntity.FirstOrDefault(), chargeStationEntity.FirstOrDefault().Group);
                if (validationResult.IsValid)
                {
                    await _repository.Create(connectorModel);
                }
                else
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task DeleteConnector(int connectorId, int chargeStationId)
        {
            var connectors = await _repository.FindByCondition(g => g.Id == connectorId && g.ChargeStationId == chargeStationId);
            if (connectors.Count() > 0)
            {
                var chargeStationEntity = await _chargeStationRepository.FindByCondition(cs => cs.Id == chargeStationId);
                var validationResult = ConnectorValidator.ValidateForDelete(chargeStationEntity.FirstOrDefault());
                if (validationResult.IsValid)
                {
                    await _repository.Delete(connectors.FirstOrDefault());
                }
                else
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task<IEnumerable<ConnectorReadWithDetailDto>> GetAllConnectors()
        {
            var connectors = await _repository.FindAll();
            return _mapper.Map<IEnumerable<ConnectorReadWithDetailDto>>(connectors);
        }

        public async Task<ConnectorReadWithDetailDto> GetConnectorById(int connectorId, int chargeStationId)
        {
            var connector = await _repository.FindByCondition(g => g.Id == connectorId && g.ChargeStationId == chargeStationId);
            return _mapper.Map<ConnectorReadWithDetailDto>(connector.FirstOrDefault());
        }

        public async Task UpdateConnector(ConnectorUpdateDto connector)
        {
            var connectors = await _repository.FindByCondition(g => g.Id == connector.Id && g.ChargeStationId == connector.ChargeStationId);
            if (connectors.Count() > 0)
            {
                var chargeStationEntity = await _chargeStationRepository.FindByCondition(cs => cs.Id == connector.ChargeStationId);
                var validationResult = ConnectorValidator.ValidateForUpdate(connectors.FirstOrDefault(), chargeStationEntity.FirstOrDefault().Group);
                if (validationResult.IsValid)
                {
                    var connectorModel = _mapper.Map<Connector>(connector);
                    await _repository.Update(connectorModel);
                }
                else
                {
                    throw new ValidationException(validationResult.Errors);
                }
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
