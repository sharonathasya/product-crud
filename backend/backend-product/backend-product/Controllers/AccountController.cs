using backend_product.Interfaces;
using backend_product.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend_product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService accountService = accountService;


        [HttpPost("Login")]
        public async Task<ResLogin> Login([FromBody] ReqLogin request)
        {
            return await accountService.Login(request);
        }
    }
}
