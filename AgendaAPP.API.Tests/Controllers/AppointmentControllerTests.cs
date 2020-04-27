using AgendaApp.API.Controllers;
using AgendaApp.API.Services.Interface;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AgendaApp.API.Entities;

namespace AgendaApp.API.Tests.Controllers
{
    [TestFixture]
    public class AppointmentControllerTests
    {
        private AppointmentsController controller;
        [SetUp]
        public void Setup()
        {
            var appointmentService = new Mock<IAppointmentService>();
            appointmentService
                .Setup(o => o.ClientExists(new Guid("f441f863-bfff-49c3-8037-31fd6cb364d6")))
                .Returns(Task.FromResult(true));
            appointmentService
                .Setup(o => o.GetAppointments(new Guid("f441f863-bfff-49c3-8037-31fd6cb364d6")))
                .Returns(Task.FromResult(GenerateAppointments(2, new Guid("f441f863-bfff-49c3-8037-31fd6cb364d6"))));
            appointmentService
                .Setup(o => o.GetAppointment(new Guid("f441f863-bfff-49c3-8037-31fd6cb364d6"), new Guid("e1e3f1b4-38a5-4035-b63a-1f14e9517584")))
                .Returns(Task.FromResult(new Appointment()));

            var mapper = new Mock<IMapper>();
            controller = new AppointmentsController(appointmentService.Object, mapper.Object);
        }

        [Test]
        public async Task TestGetAppointmentsOfClient_Returns_ActionResultAsNotFound_Client_NotFound()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            // Act
            var response = await controller.GetAppointmentsForClientAsync(clientId);
            var result = response.Result;

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task TestGetAppointmentsOfClient_Returns_ActionResult_OkObjectResult_WithAppointments()
        {
            // Arrange
            var clientId = new Guid("f441f863-bfff-49c3-8037-31fd6cb364d6");

            // Act
            var response = await controller.GetAppointmentsForClientAsync(clientId);
            var result = response.Result;

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }


        [TestCase("f441f863-bfff-49c3-8037-31fd6cb364d6", "e1e3f1b4-38a5-4035-b63a-1f14e9517584", true)]
        [TestCase("e1e3f1b4-38a5-4035-b63a-1f14e9517584", "f441f863-bfff-49c3-8037-31fd6cb364d6", false)]
        public async Task TestGetAppointmentOfClientByAppointmentId_Returns_ActionResult(string c, string a, bool r)
        {
            // Arrange
            var clientId = new Guid(c);
            var appointmentId = new Guid(a);

            // Act
            var response = await controller.GetAppointmentForClientAsync(clientId, appointmentId);
            var result = response.Result;

            // Assert
            if (r)
            {
                result.Should().BeOfType<OkObjectResult>();
            }
            else
            {
                result.Should().BeOfType<NotFoundResult>();
            }
        }

        private IEnumerable<Appointment> GenerateAppointments(int amount, Guid clientId)
        {
            var appointments = new List<Appointment>();
            for(var i = 0; i < amount; i++)
            {
                var random = new Random();
                appointments.Add(
                    new Appointment
                    {
                        ClientId = clientId,
                        IsFullDay = random.Next(0, 1) != 0,
                        Start = DateTime.Today,
                        End = DateTime.Today.AddHours(1),
                        Title = $"Test_{i}",
                    }
                );
            }
            return appointments;
        }
    }
}
