using IotWebApi.Dto;
using IotWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private IUserService _userService;

        public AuditLogController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var logs = _userService.GetAllLog();
            return Ok(logs);
        }
        [HttpPost]
        public IActionResult CreateLog(AuditLogDto u)
        {
            _userService.CreateLog(u);
            return Ok();
        }


    }
}
