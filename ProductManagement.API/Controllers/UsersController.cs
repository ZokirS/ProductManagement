using Microsoft.AspNetCore.Mvc;
using ProductManagement.API.FilterAttributes;
using ProductManagement.BL.Models.User;
using ProductManagement.BL.Services.Abstract;
using ProductManagement.DAL.Helpers;
using ProductManagement.DAL.Models;

namespace ProductManagement.API.Controllers
{
    public class UsersController(IUserService userService): ControllerBase
    {
        [HttpPost("[controller]/[action]")]
        public async Task<ActionResult<UserSession>> Authenticate([FromBody] AuthenticateRequest request)
        {
            var response = await userService.AuthenticateAsync(request);
            return Ok(response);
        }
    }
}
