using IotWebApi.Services;
using IotWebApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }
        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            var user = _roleService.GetById(id);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Create(RoleDto u)
        {
            var res = _roleService.Create(u);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Role registered successfully", state = 1 });
            return BadRequest(new { message = "The RoleName is in use!", state = 0 });
        }
        [HttpDelete]
        public IActionResult Remove(string id)
        {
            var res = _roleService.Remove(id);
            if (res) return Ok(new { message = "Deleted successfully", state = 1 });
            return BadRequest(new { message = "Delete failed!", state = 0 });
        }
        [HttpPut("id")]
        public IActionResult Update(RoleDto u, string id)
        {
            var res = _roleService.Update(u, id);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Updated successfully", state = 1 });
            return BadRequest(new { message = "Update failed!", state = 0 });
        }
    }
}
