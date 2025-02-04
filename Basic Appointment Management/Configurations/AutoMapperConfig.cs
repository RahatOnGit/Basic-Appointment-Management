using AutoMapper;
using Basic_Appointment_Management.Data.DTOs;
using Basic_Appointment_Management.Data.Models;

namespace Basic_Appointment_Management.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<AppointmentCreationDTO, Appointment>();

            CreateMap<Appointment, AppointmentShowingDTO>();
        }
    }
}
