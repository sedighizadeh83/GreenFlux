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

        public void CreateConnector(ConnectorCreateDto connector)
        {
            var connectorModel = _mapper.Map<Connector>(connector);
            var chargeStationEntity = _chargeStationRepository.FindByCondition(cs => cs.Id == connector.ChargeStationId).ToList();
            if (chargeStationEntity.Count > 0)
            {
                var validationResult = ConnectorValidator.ValidateForCreate(connectorModel, chargeStationEntity.FirstOrDefault(), chargeStationEntity.FirstOrDefault().Group);
                if (validationResult.IsValid)
                {
                    _repository.Create(connectorModel);
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

        public void DeleteConnector(int connectorId, int chargeStationId)
        {
            var connectors = _repository.FindByCondition(g => g.Id == connectorId && g.ChargeStationId == chargeStationId).ToList();
            if (connectors.Count > 0)
            {
                var chargeStationEntity = _chargeStationRepository.FindByCondition(cs => cs.Id == chargeStationId).ToList();
                var validationResult = ConnectorValidator.ValidateForDelete(chargeStationEntity.FirstOrDefault());
                if (validationResult.IsValid)
                {
                    _repository.Delete(connectors.FirstOrDefault());
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

        public IEnumerable<ConnectorReadWithDetailDto> GetAllConnectors()
        {
            var connectors = _repository.FindAll();
            return _mapper.Map<IEnumerable<ConnectorReadWithDetailDto>>(connectors);
        }

        public ConnectorReadWithDetailDto GetConnectorById(int connectorId, int chargeStationId)
        {
            var connector = _repository.FindByCondition(g => g.Id == connectorId && g.ChargeStationId == chargeStationId).FirstOrDefault();
            return _mapper.Map<ConnectorReadWithDetailDto>(connector);
        }

        public void UpdateConnector(ConnectorUpdateDto connector)
        {
            var connectors = _repository.FindByCondition(g => g.Id == connector.Id && g.ChargeStationId == connector.ChargeStationId).ToList();
            if (connectors.Count > 0)
            {
                var chargeStationEntity = _chargeStationRepository.FindByCondition(cs => cs.Id == connector.ChargeStationId).ToList();
                var validationResult = ConnectorValidator.ValidateForUpdate(connectors.FirstOrDefault(), chargeStationEntity.FirstOrDefault().Group);
                if (validationResult.IsValid)
                {
                    var connectorModel = _mapper.Map<Connector>(connector);
                    _repository.Update(connectorModel);
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
