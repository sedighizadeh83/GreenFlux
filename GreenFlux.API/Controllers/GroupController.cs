using Microsoft.AspNetCore.Mvc;
using GreenFlux.DTO.Group;
using GreenFlux.ServiceAbstraction;
using System.Collections.Generic;

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
        public ActionResult GetAllGroups()
        {
            var groupItems = _groupService.GetAllGroups();

            return Ok(groupItems);
        }

        [HttpGet("{id}")]
        public ActionResult GetGroupById(int id)
        {
            var groupItem = _groupService.GetGroupById(id);

            if (groupItem == null)
            {
                return NotFound();
            }

            return Ok(groupItem);
        }

        [HttpPost]
        public ActionResult CreateGroup(GroupCreateDto group)
        {
            _groupService.CreateGroup(group);

            return Ok("Group created successfully");
        }

        [HttpPut]
        public ActionResult UpdateGroup(GroupUpdateDto group)
        {
            _groupService.UpdateGroup(group);

            return Ok("Group updated successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGroup(int id)
        {
            _groupService.DeleteGroup(id);

            return Ok("Group deleted successfully");
        }
    }
}
