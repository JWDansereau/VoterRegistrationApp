using System.ComponentModel.DataAnnotations;

namespace VoterRegistrationApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }

        public string Role { get; set; } = "Citizen";
    }
}