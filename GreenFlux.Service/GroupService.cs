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

        public void CreateGroup(GroupCreateDto group)
        {
            var groupModel = _mapper.Map<Group>(group);
            _repository.Create(groupModel);
        }

        public void DeleteGroup(int groupId)
        {
            var groups = _repository.FindByCondition(g => g.Id == groupId).ToList();
            if(groups.Count > 0)
            {
                _repository.Delete(groups.FirstOrDefault());
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }

        public IEnumerable<GroupReadWithDetailDto> GetAllGroups()
        {
            var groups = _repository.FindAll();
            return _mapper.Map<IEnumerable<GroupReadWithDetailDto>>(groups);
        }

        public GroupReadWithDetailDto GetGroupById(int groupId)
        {
            var group = _repository.FindByCondition(g => g.Id == groupId).FirstOrDefault();
            return _mapper.Map<GroupReadWithDetailDto>(group);

        }

        public void UpdateGroup(GroupUpdateDto group)
        {
            var groupModel = _mapper.Map<Group>(group);
            var groupEntity = _repository.FindByCondition(g => g.Id == groupModel.Id).ToList();
            if (groupEntity.Count > 0)
            {
                var validationResult = GroupValidator.Validate(groupModel, groupEntity.FirstOrDefault());
                if (validationResult.IsValid)
                {
                    _repository.Update(groupEntity.FirstOrDefault());
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