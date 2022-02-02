using Microsoft.AspNetCore.Mvc;
using GreenFlux.DTO.Group;
using GreenFlux.ServiceAbstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenFlux.API.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllGroups()
        {
            var groupItems = await _groupService.GetAllGroups();

            return Ok(groupItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetGroupById(int id)
        {
            var groupItem = await _groupService.GetGroupById(id);

            if (groupItem == null)
            {
                return NotFound();
            }

            return Ok(groupItem);
        }

        [HttpPost]
        public async Task<ActionResult> CreateGroup(GroupCreateDto group)
        {
            await _groupService.CreateGroup(group);

            return Ok("Group created successfully");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGroup(GroupUpdateDto group)
        {
            await _groupService.UpdateGroup(group);

            return Ok("Group updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(int id)
        {
            await _groupService.DeleteGroup(id);

            return Ok("Group deleted successfully");
        }
    }
}
