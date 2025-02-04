using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Basic_Appointment_Management.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        
        public string PatientName { get; set; }

        
        public string PatientContact { get; set; }

        public DateTime DateAndTime { get; set; }

        
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor TheDoctor { get; set; }



    }
}
