using AgendaApp.API.Entities;
using AgendaApp.API.Models;
using AutoMapper;

namespace AgendaApp.API.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentCDto, Appointment>()
                .ForMember(
                    dest => dest.End,
                    opt => opt.MapFrom(src => src.End ?? src.Start.AddHours(1)));
            CreateMap<AppointmentUDto, Appointment>()
                .ForMember(
                    dest => dest.End,
                    opt => opt.MapFrom(src => src.End ?? src.Start.AddHours(1)));
        }
    }
}
