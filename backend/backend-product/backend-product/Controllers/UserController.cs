using backend_product.Impl;
using backend_product.Interfaces;
using backend_product.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService userService = userService;

        [Authorize]
        [HttpPost("Register")]
        public async Task<UserRes> RegisterUser([FromBody] ReqAddUser request)
        {
            return await userService.Register(request);
        }

        [Authorize]
        [HttpPost("GetUserByEmail")]
        public async Task<UserRes> GetUserByEmail([FromBody] ReqIdUser request)
        {
            return await userService.GetUserByEmail(request);
        }

    }
}
