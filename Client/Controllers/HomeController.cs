using Client.Extensions;
using Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<string> _types;
        private const string parentUrl = "https://localhost:7090/Api/User";

        public HomeController()
        {
            _types = GetTypes();
        }

        public IActionResult Index()
        {
            var model = new IndexModel() { Types = _types };
            return View(model);
        }

        public void Delete()
        {
            Index();
        }

        public JsonResult GetUsers(string name, string type, DateTime dateFrom, DateTime dateTo)
        {
            var url = parentUrl;
            if (dateFrom != null && dateTo != null && dateFrom != DateTime.MinValue && dateTo != DateTime.MinValue)
            {
                url = GetUsersWithDateUrl(dateFrom, dateTo);
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                url = GetUsersWithTypeUrl(type);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                url = GetUsersWithNameUrl(name);
            }

            var users = ExtensionMethods.Get<List<User>>(url);
            return GetUserJson(users);
        }

        private string GetUsersWithNameUrl(string name)
        {
            return $"{parentUrl}/UsersWithName/{name}";
        }

        private string GetUsersWithTypeUrl(string typeName)
        {
            return $"{parentUrl}/UsersWithType/{typeName}";
        }
        private string GetUsersWithDateUrl(DateTime dateFrom, DateTime dateTo)
        {
            return $"{parentUrl}/UsersWithDate/{dateFrom.ToString("MM.dd.yyyy")}/{dateTo.ToString("MM.dd.yyyy")}";
        }

        private List<string> GetTypes()
        {
            var url = $"{parentUrl}/Types";
            return ExtensionMethods.Get<List<string>>(url);
        }

        private JsonResult GetUserJson(List<User> users)
        {
            if (users is null)
            {
                return Json(0);
            }

            List<string[]> JsStrings = new List<string[]>();
        
            foreach (var user in users)
            {             
                JsStrings.Add(new string[]
                {
                    user.Id.ToString(),
                    user.Name.ToString(),
                    user.Login.ToString(),
                    user.Password.ToString(),
                    user.Type.ToString(),
                    user.LastVisitDate.ToString()
                });
            }

            dynamic js = Json(new
            {
                aaData = JsStrings
            });

            return js;
        }

        public IActionResult CreateModalWindow()
        {
            var model = new ModelInfo() { User = new User(), Types = _types, ModalType = Enums.ModalType.Create };
            return PartialView("Modal", model);
        }

        public IActionResult EditModalWindow(int id, string name, string login, string password, string type, DateTime date)
        {
            var model = new ModelInfo() { User = new User(id, login, password, name, type, date), Types = _types, ModalType = Enums.ModalType.Edit };
            return PartialView("Modal", model);
        }

        public void UserAction(int id, string name, string login, string password, string actionName)
        {
            var url = string.Empty;
            switch(actionName)
            {
                case "create":
                    url = $"{parentUrl}/create/{login}/{password}/{name}";
                    break;
                case "edit":
                    url = $"{parentUrl}/edit/{id}/{login}/{password}/{name}";
                    break;
                case "delete":
                    url = $"{parentUrl}/delete/{id}";
                    break;
            }

            if(!string.IsNullOrWhiteSpace(url))
            {
                ExtensionMethods.Set(url);
            }
        }

        public int IsUserAllowEdit()
        {
            return HttpContext.Session.GetInt32("AllowEdit").GetValueOrDefault();
        }

        public void ChangeProvider(int provider)
        {
            var url = string.Empty;
            switch (provider)
            {
                case 0:
                    url = $"{parentUrl}/Database";
                    break;
                case 1:
                    url = $"{parentUrl}/Document";
                    break;
            }

            if(!string.IsNullOrWhiteSpace(url))
            {
                ExtensionMethods.Set(url);
            }
        }
    }
}