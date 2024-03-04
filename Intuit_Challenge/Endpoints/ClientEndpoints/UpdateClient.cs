using AutoMapper;
using Intuit_Challenge.Entities;
using Intuit_Challenge.Services;
using Microsoft.AspNetCore.Mvc;
using static Intuit_Challenge.Dtos.ClientDtos;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;

namespace Intuit_Challenge.Endpoints.ClientEndpoints
{
    public class UpdateClient : ControllerBase
    {
        private readonly IClientService _clientService;

        public UpdateClient(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPut("client/updateClient")]
        [SwaggerOperation(
            Summary = "Update client",
            Description = "Update client",
            OperationId = "updateClient",
            Tags = new[] { "Client" })
        ]
        public async Task<ActionResult<UpdateClientResponse>> InsertNewClient([FromBody] UpdateClientRequest req)
        {
            try
            {
                var client = await _clientService.GetClientId(req.UpdateClientDto.Id);
                client.Address = !string.IsNullOrEmpty(req.UpdateClientDto.Address) ? req.UpdateClientDto.Address : client.Address;
                client.CellPhone = !string.IsNullOrEmpty(req.UpdateClientDto.CellPhone) ? req.UpdateClientDto.CellPhone : client.CellPhone;
                client.Birthday = req.UpdateClientDto.Birthday != null ? req.UpdateClientDto.Birthday : client.Birthday;
                client.Cuit = !string.IsNullOrEmpty(req.UpdateClientDto.Cuit) ? req.UpdateClientDto.Cuit : client.Cuit;
                client.Email = !string.IsNullOrEmpty(req.UpdateClientDto.Email) ? req.UpdateClientDto.Email : client.Email;
                client.Names = !string.IsNullOrEmpty(req.UpdateClientDto.Names) ? req.UpdateClientDto.Names : client.Names;
                client.LastNames = !string.IsNullOrEmpty(req.UpdateClientDto.LastNames) ? req.UpdateClientDto.LastNames : client.LastNames;
                return new UpdateClientResponse { Id = await _clientService.UpdateClient(client) };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class UpdateClientRequest
    {
        [FromQuery]
        public UpdateClientDto UpdateClientDto { get; set; }
    }

    public class UpdateClientResponse
    {
        public int Id { get; set; }
    }
}
