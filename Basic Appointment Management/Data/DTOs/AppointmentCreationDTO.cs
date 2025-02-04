using Basic_Appointment_Management.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Basic_Appointment_Management.Validators;

namespace Basic_Appointment_Management.Data.DTOs
{
    public class AppointmentCreationDTO
    {
        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientContact { get; set; }

        [Required]
        [DateTimeChecker]
        public DateTime DateAndTime { get; set; }

        [Required]
        public int DoctorId { get; set; }

        
    }
}
