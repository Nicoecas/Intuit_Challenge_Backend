using AutoMapper;
using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using static Intuit_Challenge.Dtos.ClientDtos;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Intuit_Challenge.Endpoints.ClientEndpoints
{
    public class GetClientId : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public GetClientId(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet("client/getClientId")]
        [SwaggerOperation(
            Summary = "Get a Client",
            Description = "Get a client with a ID value",
            OperationId = "getClientId",
            Tags = new[] { "Client" })
        ]
        public async Task<ActionResult<GetClientIdResponse>> GetClient([FromRoute] getClientIdRequest req)
        {
            try
            {
                var clients = await _clientService.GetClientId(req.Id);
                GetClientIdResponse getAllClientsResponse = new GetClientIdResponse
                {
                    Client = _mapper.Map<GetClientDto>(clients)
                };
                return getAllClientsResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class getClientIdRequest
    {
        [Required]
        [FromQuery]
        public int Id { get; set; }
    }

    public class GetClientIdResponse
    {
        public GetClientDto Client { get; set; }
    }
}
