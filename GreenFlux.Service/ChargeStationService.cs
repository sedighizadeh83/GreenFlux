using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using AutoMapper;
using GreenFlux.Data.Models;
using GreenFlux.Service.Validators;
using GreenFlux.GlobalErrorHandling.Exceptions;

namespace GreenFlux.Service
{
    public sealed class ChargeStationService : IChargeStationService
    {
        private readonly IChargeStationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public ChargeStationService(IChargeStationRepository repository, IMapper mapper, IGroupRepository groupRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public void CreateChargeStation(ChargeStationCreateDto chargeStation)
        {
            var connectorsModel = _mapper.Map <IEnumerable<Connector>>(chargeStation.connectors);
            var chargeStationModel = _mapper.Map<ChargeStation>(chargeStation);
            var groupEntity = _groupRepository.FindByCondition(g => g.Id == chargeStationModel.GroupId).ToList();
            if (groupEntity.Count > 0)
            {
                foreach (var connector in connectorsModel)
                {
                    chargeStationModel.ConnectorCollection.Add(connector);
                }
                var validationResult = ChargeStationValidator.Validate(chargeStationModel, groupEntity.FirstOrDefault());
                if (validationResult.IsValid)
                {
                    _repository.Create(chargeStationModel);
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

        public void DeleteChargeStation(int chargeStationId)
        {
            var chargeStationModelList = _repository.FindByCondition(cs => cs.Id == chargeStationId).ToList();
            if (chargeStationModelList.Count > 0)
            {
                _repository.Delete(chargeStationModelList.FirstOrDefault());
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public IEnumerable<ChargeStationReadWithDetailDto> GetAllChargeStations()
        {
            var chargeStations = _repository.FindAll();
            return _mapper.Map<IEnumerable<ChargeStationReadWithDetailDto>>(chargeStations);
        }

        public ChargeStationReadWithDetailDto GetChargeStationById(int chargeStationId)
        {
            var chargeStation = _repository.FindByCondition(cs => cs.Id == chargeStationId).FirstOrDefault();
            return _mapper.Map<ChargeStationReadWithDetailDto>(chargeStation);
        }

        public void UpdateChargeStation(ChargeStationUpdateDto chargeStation)
        {
            var chargeStationModel = _mapper.Map<ChargeStation>(chargeStation);
            var chargeStationEntity = _repository.FindByCondition(cs => cs.Id == chargeStationModel.Id).ToList();
            if (chargeStationEntity.Count > 0)
            {
                _repository.Update(chargeStationModel);
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
