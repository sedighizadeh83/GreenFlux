using GreenFlux.DTO.Group;

namespace GreenFlux.ServiceAbstraction
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupReadWithDetailDto>> GetAllGroups();

        Task<GroupReadWithDetailDto> GetGroupById(int groupId);

        Task CreateGroup(GroupCreateDto group);

        Task UpdateGroup(GroupUpdateDto group);

        Task DeleteGroup(int groupId);
    }
}