namespace Client.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime LastVisitDate { get; set; }
        public bool AllowEdit { get; set; }
        public User()
        {

        }

        public User(int id, string? login, string? password, string name, string type, DateTime lastVisitDate)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Type = type;
            LastVisitDate = lastVisitDate;
        }

        public User(int id, string? login, string? password, string name, string type, DateTime lastVisitDate, bool allowEdit)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            Type = type;
            LastVisitDate = lastVisitDate;
            AllowEdit = allowEdit;
        }
    }
}
