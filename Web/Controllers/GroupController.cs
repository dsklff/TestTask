using Application.Interfaces;
using Application.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("getGroups")]
        public async Task<ActionResult<List<ProductGroupListVm>>> GetGroups()
        {
            var groups = await _groupService.GetProductGroupList();

            return Ok(groups);
        }

        [HttpGet("getGroupItemsById")]
        public async Task<ActionResult<List<ProductGroupItemListVm>>> GetGroupItemsById(Guid groupId)
        {
            var groups = await _groupService.GetProductGroupItemsByGroupId(groupId);

            return Ok(groups);
        }
    }
}
