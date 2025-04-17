using Microsoft.AspNetCore.Mvc;

namespace frontend_product.Controllers
{
    public class BaseController : Controller
    {
        protected bool IsUserLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"));
        }
    }
}
