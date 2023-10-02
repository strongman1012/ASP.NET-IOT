using IotWebApi.Dto;
using IotWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Base;

namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpPost("plist")]
        public IActionResult PostAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }


        // to be used with syncfusion datamanager
        [HttpPost("read")]
        public ActionResult Read(DataManagerRequest dm)
        {
            return Ok(_userService.GetAllWithDM(dm));
        }


        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create(UserDto u, string password)
        {
            var res = _userService.Create(u, password);
            if (!string.IsNullOrEmpty(res)) return Ok(res);
            return BadRequest("Error");
        }

        [HttpDelete]
        public IActionResult Remove(string id)
        {
            var res = _userService.Remove(id);
            if (res) return Ok(new { message = "Deleted successfully", state = 1 });
            return BadRequest(new { message = "Delete failed!", state = 0 });
        }

        [HttpDelete("username")]
        public IActionResult RemoveByUsername(string username)
        {
            var res = _userService.RemoveByUsername(username);
            if (res) return Ok(res);
            return BadRequest("Error");
        }

        [HttpPut("id")]
        public IActionResult Update(UserDto u, string id)
        {
            var res = _userService.Update(u, id);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Updated successfully", state = 1 });
            return BadRequest(new { message = "Update failed!", state = 0 });
        }
    }
}
