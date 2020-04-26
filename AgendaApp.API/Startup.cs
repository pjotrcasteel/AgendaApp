using AgendaApp.API.Context;
using AgendaApp.API.Repositories.Implementation;
using AgendaApp.API.Repositories.Interface;
using AgendaApp.API.Services.Implementation;
using AgendaApp.API.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AgendaApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setup => {
                setup.InvalidModelStateResponseFactory = context =>
                {
                    var detailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();
                    var details = detailsFactory.CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState);

                    details.Detail = "Refer to the 'Errors' field for details";
                    details.Instance = context.HttpContext.Request.Path;

                    if ((context.ModelState.ErrorCount > 0) && ((context as ActionExecutingContext)?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)){
                        details.Type = "Link from config to page with details about model validationtypes";
                        details.Status = StatusCodes.Status422UnprocessableEntity;
                        details.Title = $"{context.ModelState.ErrorCount} validation error{(context.ModelState.ErrorCount > 1 ? "s" : "")} occured";
                    }
                    else
                    {
                        details.Status = StatusCodes.Status400BadRequest;
                        details.Title = "Input values are invalid";
                    }
                    return new UnprocessableEntityObjectResult(details)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();


            services.AddDbContext<AgendaDbContext>(options => {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=AgendaAppDb;Trusted_Connection=True");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( appBuilder => {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An Unexpected error has occured");
                    });
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
