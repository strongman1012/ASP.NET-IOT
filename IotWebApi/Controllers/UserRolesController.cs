using IotWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private IUserRoleService _userRoleService;

        public UserRolesController(IUserService userService, IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

    }
}
