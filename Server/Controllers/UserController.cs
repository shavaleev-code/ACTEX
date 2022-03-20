using Microsoft.AspNetCore.Mvc;
using Server.DAL.Database;
using Server.DAL.Document;
using Server.Enums;
using Server.Interfaces;
using Server.Model;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IDataProvider _dataProvider;
        private static string _userFileName = "Users.json";
        private static string _userTypeFileName = "UserTypes.json";
        private static DataProviders _dataProviderType = DataProviders.Database;
        
        public UserController()
        {
            if(_dataProviderType == DataProviders.Database)
            {
                _dataProvider = new DatabaseProvider();
            }

            if(_dataProviderType == DataProviders.Document)
            {
                _dataProvider = string.IsNullOrWhiteSpace(_userFileName) || string.IsNullOrWhiteSpace(_userTypeFileName) ? 
                    null :
                    DocumentProvider.DocumentProviderInit(_userFileName, _userTypeFileName);

                //new DatabaseProvider().CreateDatabase(_dataProvider.GetUsers(), _dataProvider.GetUserTypes());
            }
        }

        /// <summary>
        /// Возвращает список всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<UserViewModel> GetUsers()
        {
            var users = _dataProvider?.GetUsers();
            return GetUserViewModel(users);
        }

        /// <summary>
        /// Возвращает список всех типов пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet("Types")]
        public async Task<List<string>> GetUserTypes()
        {
            return _dataProvider?.GetUserTypes().Select(s => s.Name).ToList();
        }

        /// <summary>
        /// Возвращает список всех типов пользователей, у которых имя содержит name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("UsersWithName/{name}")]
        public async Task<List<UserViewModel>> GetUsersWithName(string name)
        {
            var users = _dataProvider?.GetUsersWithName(name);
            return GetUserViewModel(users);
        }

        [HttpGet("{login}/{password}")]
        public async Task<UserViewModel> GetUser(string login, string password)
        {
            var user = _dataProvider?.GetUser(login, password);
            var types = _dataProvider?.GetUserTypes();
            if (user is null)
            { 
                return null;
            }

            return new UserViewModel(user, types);
        }

        /// <summary>
        /// Возвращает список всех типов пользователей c типом type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("UsersWithType/{typeName}")]
        public async Task<List<UserViewModel>> GetUsersWithType(string typeName)
        {
            var typeId = _dataProvider.GetUserTypes().Where(s => s.Name == typeName).Select(s => s.Id).FirstOrDefault();
            var users = _dataProvider?.GetUsersWithType(typeId);
            return GetUserViewModel(users);
        }

        /// <summary>
        /// Возвращает список всех типов пользователей, с датой последнего визита между from и to
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet("UsersWithDate/{from}/{to}")]
        public async Task<List<UserViewModel>> GetUsersWithDate(DateTime from, DateTime to)
        {
            var users = _dataProvider?.GetUsersWithDate(from, to);
            return GetUserViewModel(users);
        }

        /// <summary>
        /// Устанавливает источник данных - базу данных
        /// </summary>
        [HttpGet("Database")]
        public async void SetDataProviderDb()
        {
            _dataProviderType = DataProviders.Database;
        }

        /// <summary>
        /// Устанавливает источник данных - файл
        /// </summary>
        [HttpGet("Document")]
        public async void SetDataProviderDoc()
        {
            _dataProviderType = DataProviders.Document;
        }

        private List<UserViewModel> GetUserViewModel(IEnumerable<User> users)
        {
            var types = _dataProvider?.GetUserTypes();
            return users.Join(types, u => u.TypeId, t => t.Id, (u, t) => new UserViewModel(u.Id, u.Login, u.Password, u.Name, t.Name, u.LastVisitDate, t.AllowEdit)).ToList();
        }

        [HttpGet("create/{login}/{password}/{name}")]
        public async void CreateUser(string login, string password, string name)
        {
            _dataProvider.CreateUser(login, password, name);
        }

        [HttpGet("edit/{id}/{login}/{password}/{name}")]
        public async void EditUser(int id, string login, string password, string name)
        {
            _dataProvider.EditUser(id, login, password, name);
        }

        [HttpGet("delete/{id}")]
        public async void DeleteUser(int id)
        {
            _dataProvider.DeleteUser(id);
        }
    }
}
