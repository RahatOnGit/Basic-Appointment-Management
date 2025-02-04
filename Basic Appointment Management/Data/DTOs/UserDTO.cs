using System.ComponentModel.DataAnnotations;

namespace Basic_Appointment_Management.Data.DTOs
{
    public class UserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
