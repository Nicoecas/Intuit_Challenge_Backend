using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using static Intuit_Challenge.Dtos.ClientDtos;
using Swashbuckle.AspNetCore.Annotations;
using Intuit_Challenge.Entities;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Intuit_Challenge.Endpoints.ClientEndpoints
{
    public class InsertClient : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public InsertClient(IClientService clientService,
            IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpPost("client/insertClient")]
        [SwaggerOperation(
            Summary = "Create client",
            Description = "Create client",
            OperationId = "insertClient",
            Tags = new[] { "Client" })
        ]
        public async Task<ActionResult<InsertClientResponse>> InsertNewClient([FromBody] InsertClientRequest req)
        {
            try
            {
                Client client = _mapper.Map<Client>(req.CreateClientDto);
                return new InsertClientResponse { Id = await _clientService.CreateClient(client) };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class InsertClientRequest
    {
        [FromQuery]
        [Required]
        public CreateClientDto CreateClientDto { get; set; }
    }

    public class InsertClientResponse
    {
        public int Id { get; set; }
    }
}
