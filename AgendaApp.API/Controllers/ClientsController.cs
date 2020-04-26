using AgendaApp.API.Entities;
using AgendaApp.API.Helpers.Binders;
using AgendaApp.API.Models;
using AgendaApp.API.Resources;
using AgendaApp.API.Services.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;
        public ClientsController(IClientService clientService, IMapper mapper)
        {
            this.clientService = clientService;
            this.mapper = mapper;
        }
        
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClients([FromQuery]ClientsResourceParameters clientsResourceParameters)
        {
            var clients = await clientService.GetClients(clientsResourceParameters);
            return Ok(mapper.Map<IEnumerable<ClientDto>>(clients));
        }

        [HttpGet("{clientId:guid}", Name = "GetClient")]
        public async Task<IActionResult> GetClient([FromRoute]Guid clientId)
        {
            var client = await clientService.GetClient(clientId);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ClientDto>(client));
        }

        [HttpGet("({clientIds})", Name = "GetClients")]      
        public async Task<ActionResult> GetClients([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinding))] IEnumerable<Guid> clientIds)
        {
            if(clientIds == null)
            {
                return BadRequest();
            }

            var clientEntities = await clientService.GetClients(clientIds);

            if(clientIds.Count() != clientEntities.Count())
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<ClientDto>>(clientEntities));
        }


        [HttpPost]
        public async Task<ActionResult<ClientDto>> CreateClientAsync([FromBody]ClientCDto client)
        {
            var clientEntity = mapper.Map<Client>(client);          
            var clientIdReturn = await clientService.Create(clientEntity);
            
            return CreatedAtRoute("GetClient", new { clientId = clientIdReturn }, mapper.Map<ClientDto>(clientEntity));
        }

        [HttpPost("list")]
        public async Task<ActionResult<IEnumerator<ClientDto>>> CreateClientsAsync([FromBody]IEnumerable<ClientCDto> clients)
        {
            var clientEntities = mapper.Map<IEnumerable<Client>>(clients);
            var clientIdReturn = await clientService.Create(clientEntities);

            return CreatedAtRoute("GetClients", new { clientIds = string.Join(",", clientIdReturn) }, mapper.Map<IEnumerable<ClientDto>>(clientEntities));
        }

        [HttpOptions]
        public IActionResult GetClientOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
