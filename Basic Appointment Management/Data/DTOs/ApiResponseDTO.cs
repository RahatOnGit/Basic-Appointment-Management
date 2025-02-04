using System.Net;

namespace Basic_Appointment_Management.Data.DTOs
{
    public class ApiResponseDTO
    {
        

        public HttpStatusCode Status { get; set; }


        public dynamic? Data { get; set; }

        public string? Message {  get; set; }

        public List<string>? Errors { get; set; }

        
    }
}
