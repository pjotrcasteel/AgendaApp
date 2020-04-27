using AgendaApp.API.Entities;
using AgendaApp.API.Models;
using AgendaApp.API.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaApp.API.Controllers
{
    [ApiController]
    [Route("api/clients/{clientId}/appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;

        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            this.appointmentService = appointmentService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsForClientAsync([FromRoute]Guid clientId)
        {
            if (!await ClientExists(clientId))
                return NotFound();

            var appointments = await appointmentService.GetAppointments(clientId);
            return Ok(mapper.Map<IEnumerable<AppointmentDto>>(appointments));
        }

        [HttpGet("{appointmentId}", Name = "GetAppointmentForClient")]
        public async Task<ActionResult<AppointmentDto>> GetAppointmentForClientAsync([FromRoute]Guid clientId, [FromRoute]Guid appointmentId)
        {
            if (!await ClientExists(clientId))
                return NotFound();

            var appointment = await appointmentService.GetAppointment(clientId, appointmentId);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AppointmentDto>(appointment));
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDto>> CreateAppointmentForClient([FromRoute]Guid clientId, [FromBody]AppointmentCDto appointment)
        {
            if (!await ClientExists(clientId))
                return NotFound();

            var appointmentEntity = mapper.Map<Appointment>(appointment);
            var (appointmentIdReturn, _) = await appointmentService.Create(clientId, appointmentEntity);

            return CreatedAtRoute("GetAppointmentForClient", new { clientId, appointmentId = appointmentIdReturn }, mapper.Map<AppointmentDto>(appointmentEntity));
        }

        [HttpPut("{appointmentId}")]
        public async Task<ActionResult> UpdateAppointmentForClient(Guid clientId, Guid appointmentId, [FromBody]AppointmentUDto appointment)
        {
            if (!await ClientExists(clientId))
                return NotFound();

            var appointmentEntity = await appointmentService.GetAppointment(appointmentId);

            if (appointmentEntity == null)
            {
                return NotFound();
            }

            mapper.Map(appointment, appointmentEntity);

            await appointmentService.Update(appointmentEntity);

            return Ok();
        }

        [HttpDelete("{appointmentId}")]
        public async Task<ActionResult<bool>> DeleteAppointmentForClient(Guid clientId, Guid appointmentId)
        {
            if (!await ClientExists(clientId))
                return NotFound();

            var deleted = await appointmentService.Delete(clientId, appointmentId);
            return Ok(deleted);
        }
        private async Task<bool> ClientExists(Guid clientId) 
            => await appointmentService.ClientExists(clientId);
    }
}
