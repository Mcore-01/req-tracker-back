using System.ComponentModel.DataAnnotations;

namespace req_tracker_back.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Nickname { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}