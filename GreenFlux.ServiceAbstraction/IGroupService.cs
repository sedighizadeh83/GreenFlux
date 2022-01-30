using GreenFlux.DTO.Group;

namespace GreenFlux.ServiceAbstraction
{
    public interface IGroupService
    {
        IEnumerable<GroupReadWithDetailDto> GetAllGroups();

        GroupReadWithDetailDto GetGroupById(int groupId);

        void CreateGroup(GroupCreateDto group);

        void UpdateGroup(GroupUpdateDto group);

        void DeleteGroup(int groupId);
    }
}