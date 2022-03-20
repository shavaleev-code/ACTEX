namespace Server.Model
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime LastVisitDate { get; set; }
        public bool AllowEdit { get; set; }
        
        public UserViewModel(int id, string? login, string? password, string name, string type, DateTime lastVisitDate, bool allowEdit)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Type = type;
            LastVisitDate = lastVisitDate;
            AllowEdit = allowEdit;
        }

        public UserViewModel(User user, IEnumerable<UserType> types)
        {
            var type = types.Where(t => t.Id == user.TypeId).FirstOrDefault();
            Id = user.Id;
            Login = user.Login;
            Password = user.Password;
            Name = user.Name;
            Type = type.Name;
            LastVisitDate = user.LastVisitDate;
            AllowEdit = type.AllowEdit;
        }
    }
}
