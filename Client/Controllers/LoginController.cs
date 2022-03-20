using Client.Extensions;
using Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{   
    public class LoginController : Controller
    {
        private const string parentUrl = "https://localhost:7090/Api/User";
        public IActionResult Index()
        {
            var authModel = new AuthModel();
            return View(authModel);
        }

        [HttpGet]
        public IActionResult Auth(AuthModel model)
        {
            var user = GetUser(model);
            if (user is null)
            {
                return View("Index", new AuthModel());
            }

            if(user.AllowEdit)
            {
                HttpContext.Session.SetInt32("AllowEdit", 1);
            }
            else
            {
                HttpContext.Session.SetInt32("AllowEdit", 0);
            }

            return RedirectToAction("Index", "Home");
        }

        private User GetUser(AuthModel model)
        {
            var url = $"{parentUrl}/{model.Login}/{model.Password}";
            return ExtensionMethods.Get<User>(url);
        }
    }
}
