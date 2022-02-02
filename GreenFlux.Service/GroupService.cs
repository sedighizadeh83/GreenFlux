using GreenFlux.DTO.Group;
using GreenFlux.ServiceAbstraction;
using GreenFlux.RepositoryAbstraction;
using GreenFlux.Data.Models;
using GreenFlux.GlobalErrorHandling.Exceptions;
using GreenFlux.Service.Validators;
using AutoMapper;

namespace GreenFlux.Service
{
    public sealed class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateGroup(GroupCreateDto group)
        {
            var groupModel = _mapper.Map<Group>(group);
            await _repository.Create(groupModel);
        }

        public async Task DeleteGroup(int groupId)
        {
            var groups = await _repository.FindByCondition(g => g.Id == groupId);
            if(groups.Count() > 0)
            {
                await _repository.Delete(groups.FirstOrDefault());
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public async Task<IEnumerable<GroupReadWithDetailDto>> GetAllGroups()
        {
            var groups = await _repository.FindAll();
            return _mapper.Map<IEnumerable<GroupReadWithDetailDto>>(groups);
        }

        public async Task<GroupReadWithDetailDto> GetGroupById(int groupId)
        {
            var group = await _repository.FindByCondition(g => g.Id == groupId);
            return _mapper.Map<GroupReadWithDetailDto>(group.FirstOrDefault());

        }

        public async Task UpdateGroup(GroupUpdateDto group)
        {
            var groupModel = _mapper.Map<Group>(group);
            var groupEntity = await _repository.FindByCondition(g => g.Id == groupModel.Id);
            if (groupEntity.Count() > 0)
            {
                var validationResult = GroupValidator.Validate(groupModel, groupEntity.FirstOrDefault());
                if (validationResult.IsValid)
                {
                    await _repository.Update(groupEntity.FirstOrDefault());
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