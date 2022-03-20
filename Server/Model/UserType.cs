using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Server.Model
{
    public class UserType
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        [Required]
        [JsonProperty("allow_edit")]
        public bool AllowEdit { get; set; }

        public UserType(int id, string name, bool allowEdit)
        {
            Id = id;
            Name = name;
            AllowEdit = allowEdit;
        }
    }
}
