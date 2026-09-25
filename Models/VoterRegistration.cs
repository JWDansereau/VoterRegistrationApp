using System.ComponentModel.DataAnnotations;

namespace VoterRegistrationApp.Models
{
    public class VoterRegistration
    {
        [Key]
        public int RegistrationID { get; set; }

        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string ZipCode { get; set; }

        public string Status { get; set; } = "Pending";
    }
}