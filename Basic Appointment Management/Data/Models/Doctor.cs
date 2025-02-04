namespace Basic_Appointment_Management.Data.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Appointment>? Appointment { get; set; }
    }
}
