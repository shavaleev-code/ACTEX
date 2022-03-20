using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class User
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("login")]
        public string? Login { get; set; }
        [Required]
        [JsonProperty("password")]
        public string? Password { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [Required]
        [JsonProperty("type_id")]
        public int TypeId { get; set; }
        [Required]
        [JsonProperty("last_visit_date")]
        public DateTime LastVisitDate { get; set; }

        public User(int id, string login, string password, string name, int typeId, DateTime lastVisitDate)
        {
            Id = id;
            Login = login;
            Password = password;
            Name = name;
            TypeId = typeId;
            LastVisitDate = lastVisitDate;
        }

        public User()
        {
        }
    }
}
