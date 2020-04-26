using AgendaApp.API.Entities;
using AgendaApp.API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaApp.API.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.GetName()))
                .ForMember(
                    dest => dest.AppointmentTodayCount,
                    opt => opt.MapFrom(src => CalcAppointmentsToday(src.Appointments)));

            CreateMap<ClientCDto, Client>();
        }

        private int CalcAppointmentsToday(ICollection<Appointment> appointments)
        {
            var amount = appointments.Count(a => a.Start.Date == DateTime.Now.Date);
            return amount;
        }
    }
}
