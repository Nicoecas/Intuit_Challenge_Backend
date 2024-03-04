using AutoMapper;
using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static Intuit_Challenge.Dtos.ClientDtos;

namespace Intuit_Challenge.Endpoints.ClientEndpoints
{
    public class GetAllClients : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public GetAllClients(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet("client/getAllClients")]
        [SwaggerOperation(
            Summary = "Get all Clients",
            Description = "Get all Clients but if you can put Name or LastName string",
            OperationId = "getAllClients",
            Tags = new[] { "Client" })
        ]
        public async Task<ActionResult<GetAllClientsResponse>> GetAllClient([FromRoute] GetAllClientsRequest req)
        {
            try
            {
                var clients = await _clientService.GetAllClients(req.Names, req.LastNames);
                GetAllClientsResponse getAllClientsResponse = new GetAllClientsResponse
                {
                    Clients = _mapper.Map<List<GetClientDto>>(clients)
                };
                return getAllClientsResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class GetAllClientsRequest
    {
        [FromQuery] public string Names { get; set; }
        [FromQuery] public string LastNames { get; set; }
    }

    public class GetAllClientsResponse
    {
        public List<GetClientDto> Clients { get; set; }
    }
}
