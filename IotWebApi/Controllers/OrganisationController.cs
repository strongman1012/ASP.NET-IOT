using IotWebApi.Dto;
using IotWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private IOrganisationService _organisationService;

        public OrganisationController(IOrganisationService organisationService)
        {
            _organisationService = organisationService;
        }
        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var organisation = _organisationService.GetAll();
            return Ok(organisation);
        }
        [HttpGet("parent")]
        public IActionResult GetAllParent()
        {
            var organisation = _organisationService.GetAllParent();
            return Ok(organisation);
        }
        [HttpGet("id")]
        public IActionResult GetById(string id)
        {
            var organisation = _organisationService.GetById(id);
            return Ok(organisation);
        }
        [HttpPost]
        public IActionResult Create(OrganisationDto u)
        {
            var res = _organisationService.Create(u);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Organisation registered successfully", state = 1 });
            return BadRequest(new { message = "The User is already blong to a organisation!", state = 0 });
        }
        [HttpDelete]
        public IActionResult Remove(string id)
        {
            var res = _organisationService.Remove(id);
            if (res) return Ok(new { message = "Deleted successfully", state = 1 });
            return BadRequest(new { message = "Delete failed!", state = 0 });
        }
        [HttpPut("id")]
        public IActionResult Update(OrganisationDto u, string id)
        {
            var res = _organisationService.Update(u, id);
            if (!string.IsNullOrEmpty(res)) return Ok(new { message = "Updated successfully", state = 1 });
            return BadRequest(new { message = "Update failed!", state = 0 });
        }
    }
}
