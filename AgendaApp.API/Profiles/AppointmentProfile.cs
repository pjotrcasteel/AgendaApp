using AgendaApp.API.Entities;
using AgendaApp.API.Models;
using AutoMapper;
using System;

namespace AgendaApp.API.Profiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentDto>();
            CreateMap<AppointmentCDto, Appointment>();
            CreateMap<AppointmentUDto, Appointment>();

        }
    }
}
