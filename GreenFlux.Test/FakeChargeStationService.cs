using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using System.Collections.Generic;
using GreenFlux.Service;
using AutoMapper;
using System.Threading.Tasks;

namespace GreenFlux.Test
{
    internal class FakeChargeStationService : IChargeStationService
    {
        private readonly ChargeStationService _service;
        private readonly IChargeStationRepository _repository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public FakeChargeStationService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new FakeChargeStationRepository();
            _groupRepository = new FakeGroupRepository();
            _service = new ChargeStationService(_repository, _mapper, _groupRepository);
        }
        public async Task CreateChargeStation(ChargeStationCreateDto chargeStation)
        {
            await _service.CreateChargeStation(chargeStation);
        }

        public async Task DeleteChargeStation(int chargeStationId)
        {
            await _service.DeleteChargeStation(chargeStationId);
        }

        public async Task<IEnumerable<ChargeStationReadWithDetailDto>> GetAllChargeStations()
        {
            return await _service.GetAllChargeStations();
        }

        public async Task<ChargeStationReadWithDetailDto> GetChargeStationById(int chargeStationId)
        {
            return await _service.GetChargeStationById(chargeStationId);
        }

        public async Task UpdateChargeStation(ChargeStationUpdateDto chargeStation)
        {
            await _service.UpdateChargeStation(chargeStation);
        }
    }
}
