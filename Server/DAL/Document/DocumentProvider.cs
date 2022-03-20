using Newtonsoft.Json;
using Server.Interfaces;
using Server.Model;

namespace Server.DAL.Document
{
    public class DocumentProvider : IDataProvider
    {
        private readonly string _fileName;
        private readonly List<User> _users;
        private readonly List<UserType> _userTypes;
        private DocumentProvider(List<User> users, List<UserType> userTypes, string fileName)
        {
            _users = users;
            _fileName = fileName;
            _userTypes = userTypes;
        }

        public static DocumentProvider DocumentProviderInit(string userFileName, string userTypesFileName)
        {
            var users = GetItems<User>(userFileName);
            var userTypes = GetItems<UserType>(userTypesFileName);

            return new DocumentProvider(users, userTypes, userFileName);
        }

        public List<User> GetUsers()
        {
            return _users;
        }

        public List<UserType> GetUserTypes()
        {
            return _userTypes;
        }

        public List<User> GetUsersWithName(string name)
        {
            return _users?.Where(s => s.Name.Contains(name)).ToList();
        }

        public List<User> GetUsersWithType(int type)
        {
            return _users?.Where(s => s.TypeId == type).ToList();
        }

        public List<User> GetUsersWithDate(DateTime from, DateTime to)
        {
            return _users?.Where(s => s.LastVisitDate > from && s.LastVisitDate < to).ToList();
        }

        private static List<T> LoadJson<T>(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var json = sr.ReadToEnd();
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
        }

        public static List<T>? GetItems<T>(string typeFileName)
        {
            try
            {
                return LoadJson<T>(typeFileName);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetUser(string login, string password)
        {
            return _users?.Where(s => s.Login == login && s.Password == password).FirstOrDefault();
        }

        public void CreateUser(string login, string password, string name)
        {
            if(_users is null)
            {
                return;
            }

            var id = _users.Select(s => s.Id).Max() + 1;
            var user = new User() {
                    Id = id,
                    Login = login,
                    Password = password,
                    Name = name,
                    LastVisitDate = DateTime.Now,
                    TypeId = 1
                };
            _users?.Add(user);
            Save();
        }

        public void EditUser(int id, string login, string password, string name)
        {
            if (_users is null)
            {
                return;
            }

            var user = _users.Where(s => s.Id == id).FirstOrDefault();
            if(user is not null)
            {
                user.Login = login;
                user.Password = password;
                user.Name = name;
                user.LastVisitDate = DateTime.Now;
            }

            Save();
        }

        public void DeleteUser(int id)
        {
            if (_users is null)
            {
                return;
            }

            var user = _users.Where(s => s.Id == id).FirstOrDefault();
            _users.Remove(user);
            Save();
        }

        private void Save()
        {
            try
            {
                var serializer = new JsonSerializer();
                using (var sw = new StreamWriter(_fileName))
                using (var writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, _users);
                }
            }
            catch(Exception)
            {

            }
        }
    }
}
