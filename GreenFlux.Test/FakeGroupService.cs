using GreenFlux.DTO.Group;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using System.Collections.Generic;
using GreenFlux.Service;
using AutoMapper;

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
        public void CreateGroup(GroupCreateDto group)
        {
            _service.CreateGroup(group);
        }

        public void DeleteGroup(int groupId)
        {
            _service.DeleteGroup(groupId);
        }

        public IEnumerable<GroupReadWithDetailDto> GetAllGroups()
        {
            return _service.GetAllGroups();
        }

        public GroupReadWithDetailDto GetGroupById(int groupId)
        {
            return _service.GetGroupById(groupId);
        }

        public void UpdateGroup(GroupUpdateDto group)
        {
            _service.UpdateGroup(group);
        }
    }
}
