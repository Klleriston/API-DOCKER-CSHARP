using System.Text.Json.Serialization;

namespace src.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public virtual required ICollection<Movie> Movie { get; set; }

    }
}
