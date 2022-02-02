using GreenFlux.DTO.Group;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using System.Collections.Generic;
using GreenFlux.Service;
using AutoMapper;
using System.Threading.Tasks;

namespace GreenFlux.Test
{
    public class FakeGroupService : IGroupService
    {
        private readonly GroupService _service;
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;

        public FakeGroupService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new FakeGroupRepository();
            _service = new GroupService(_repository, _mapper);
        }
        public async Task CreateGroup(GroupCreateDto group)
        {
            await _service.CreateGroup(group);
        }

        public async Task DeleteGroup(int groupId)
        {
            await _service.DeleteGroup(groupId);
        }

        public async Task<IEnumerable<GroupReadWithDetailDto>> GetAllGroups()
        {
            return await _service.GetAllGroups();
        }

        public async Task<GroupReadWithDetailDto> GetGroupById(int groupId)
        {
            return await _service.GetGroupById(groupId);
        }

        public async Task UpdateGroup(GroupUpdateDto group)
        {
            await _service.UpdateGroup(group);
        }
    }
}
