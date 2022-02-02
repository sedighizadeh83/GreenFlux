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

        public async Task CreateChargeStation(ChargeStationCreateDto chargeStation)
        {
            var connectorsModel = _mapper.Map <IEnumerable<Connector>>(chargeStation.connectors);
            var chargeStationModel = _mapper.Map<ChargeStation>(chargeStation);
            var groupEntity = await _groupRepository.FindByCondition(g => g.Id == chargeStationModel.GroupId);
            if (groupEntity.Count() > 0)
            {
                foreach (var connector in connectorsModel)
                {
                    chargeStationModel.ConnectorCollection.Add(connector);
                }
                var validationResult = ChargeStationValidator.Validate(chargeStationModel, groupEntity.FirstOrDefault());
                if (validationResult.IsValid)
                {
                    await _repository.Create(chargeStationModel);
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

        public async Task DeleteChargeStation(int chargeStationId)
        {
            var chargeStationModelList = await _repository.FindByCondition(cs => cs.Id == chargeStationId);
            if (chargeStationModelList.Count() > 0)
            {
                await _repository.Delete(chargeStationModelList.FirstOrDefault());
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task<IEnumerable<ChargeStationReadWithDetailDto>> GetAllChargeStations()
        {
            var chargeStations = await _repository.FindAll();
            return _mapper.Map<IEnumerable<ChargeStationReadWithDetailDto>>(chargeStations);
        }

        public async Task<ChargeStationReadWithDetailDto> GetChargeStationById(int chargeStationId)
        {
            var chargeStation = await _repository.FindByCondition(cs => cs.Id == chargeStationId);
            return _mapper.Map<ChargeStationReadWithDetailDto>(chargeStation.FirstOrDefault());
        }

        public async Task UpdateChargeStation(ChargeStationUpdateDto chargeStation)
        {
            var chargeStationModel = _mapper.Map<ChargeStation>(chargeStation);
            var chargeStationEntity = await _repository.FindByCondition(cs => cs.Id == chargeStationModel.Id);
            if (chargeStationEntity.Count() > 0)
            {
                await _repository.Update(chargeStationModel);
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
