using System.ComponentModel.DataAnnotations;

namespace JwtAuth.Models
{
    public class AddRoleModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}