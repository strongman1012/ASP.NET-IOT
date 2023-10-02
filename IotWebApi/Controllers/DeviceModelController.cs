using IotWebApi.Dto;
using IotWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModelController : ControllerBase
    {
        private IDeviceModelService _deviceModelService;

        public DeviceModelController(IDeviceModelService deviceModelService)
        {
            _deviceModelService = deviceModelService;
        }
        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var roles = _deviceModelService.GetAll();
            return Ok(roles);
        }
        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            var user = _deviceModelService.GetById(id);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Create(DeviceModelDto u)
        {
            var res = _deviceModelService.Create(u);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Device Model registered successfully", state = 1 });
            return BadRequest(new { message = "The ModelNo is in use!", state = 0 });
        }
        [HttpDelete]
        public IActionResult Remove(string id)
        {
            var res = _deviceModelService.Remove(id);
            if (res) return Ok(new { message = "Deleted successfully", state = 1 });
            return BadRequest(new { message = "Delete failed!", state = 0 });
        }
        [HttpPut("id")]
        public IActionResult Update(DeviceModelDto u, string id)
        {
            var res = _deviceModelService.Update(u, id);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Updated successfully", state = 1 });
            return BadRequest(new { message = "Update failed!", state = 0 });
        }
    }
}
