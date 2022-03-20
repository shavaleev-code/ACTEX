using Server.Interfaces;
using Server.Model;

namespace Server.DAL.Database
{
    public class DatabaseProvider : IDataProvider
    {
        private readonly List<User> _users;
        private  List<UserType> _userTypes;

        public DatabaseProvider()
        {
            using (var dbc = new DatabaseContext())
            {
                dbc.Database.EnsureCreated();               
                _users = dbc.Users?.ToList();
                _userTypes = dbc.UserTypes?.ToList();
            }
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

        public bool? IsUserExist(string login, string password)
        {
            return _users?.Where(s => s.Login == login && s.Password == password).Any();
        }

        public User GetUser(string login, string password)
        {
            return _users?.Where(s => s.Login == login && s.Password == password).FirstOrDefault();
        }

        public void CreateUser(string login, string password, string name)
        {
            using (var dbc = new DatabaseContext())
            {
                var id = _users.Select(s => s.Id).Max() + 1;
                var user = new User()
                {
                    Id = id,
                    Login = login,
                    Password = password,
                    Name = name,
                    LastVisitDate = DateTime.Now,
                    TypeId = 1
                };
                dbc.Users.Add(user);
                dbc.SaveChanges();
            }
        }

        public void EditUser(int id, string login, string password, string name)
        {
            using (var dbc = new DatabaseContext())
            { 
                var user = dbc.Users.Where(s => s.Id == id).FirstOrDefault();           
                if (user is not null)
                {
                    user.Login = login;
                    user.Password = password;
                    user.Name = name;
                    user.LastVisitDate = DateTime.Now;
                }

                dbc.Users.Update(user);
                dbc.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            using (var dbc = new DatabaseContext())
            {
                var user = _users.Where(s => s.Id == id).FirstOrDefault();
                dbc.Users.Remove(user);
                dbc.SaveChanges();
            }
        }

        public void CreateDatabase(List<User> users, List<UserType> types)
        {
            using (var dbc = new DatabaseContext())
            {
                dbc.Users.AddRange(users);
                dbc.UserTypes.AddRange(types);
                dbc.SaveChanges();
            }
        }
    }
}
