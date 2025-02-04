using System.ComponentModel.DataAnnotations;

namespace Basic_Appointment_Management.Data.DTOs
{
    public class AppointmentShowingDTO
    {
        public int Id { get; set;}

        public string PatientName { get; set; }

       
        public string PatientContact { get; set; }

        
        public DateTime DateAndTime { get; set; }

        
        public int DoctorId { get; set; }

        public string DoctorName { get; set; }
    }
}
