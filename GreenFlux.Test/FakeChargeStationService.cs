using GreenFlux.DTO.ChargeStation;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using System.Collections.Generic;
using GreenFlux.Service;
using AutoMapper;

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
        public void CreateChargeStation(ChargeStationCreateDto chargeStation)
        {
            _service.CreateChargeStation(chargeStation);
        }

        public void DeleteChargeStation(int chargeStationId)
        {
            _service.DeleteChargeStation(chargeStationId);
        }

        public IEnumerable<ChargeStationReadWithDetailDto> GetAllChargeStations()
        {
            return _service.GetAllChargeStations();
        }

        public ChargeStationReadWithDetailDto GetChargeStationById(int chargeStationId)
        {
            return _service.GetChargeStationById(chargeStationId);
        }

        public void UpdateChargeStation(ChargeStationUpdateDto chargeStation)
        {
            _service.UpdateChargeStation(chargeStation);
        }
    }
}
