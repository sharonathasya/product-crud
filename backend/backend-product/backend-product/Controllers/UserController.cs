using backend_product.Impl;
using backend_product.Interfaces;
using backend_product.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService userService = userService;

        [HttpPost("Register")]
        public async Task<UserRes> RegisterUser([FromBody] ReqAddUser request)
        {
            return await userService.Register(request);
        }

    }
}
