using IotWebApi.Dto;
using IotWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceCategoryController : ControllerBase    {
        private ICategoryService _categoryService;

        public DeviceCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var roles = _categoryService.GetAll();
            return Ok(roles);
        }
        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            var user = _categoryService.GetById(id);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult Create(CategoryDto u)
        {
            var res = _categoryService.Create(u);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Device Category registered successfully", state = 1 });
            return BadRequest(new { message = "The CategoryName is in use!", state = 0 });
        }
        [HttpDelete]
        public IActionResult Remove(string id)
        {
            var res = _categoryService.Remove(id);
            if (res) return Ok(new { message = "Deleted successfully", state = 1 });
            return BadRequest(new { message = "Delete failed!", state = 0 });
        }
        [HttpPut("id")]
        public IActionResult Update(CategoryDto u, string id)
        {
            var res = _categoryService.Update(u, id);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Updated successfully", state = 1 });
            return BadRequest(new { message = "Update failed!", state = 0 });
        }
    }
}
