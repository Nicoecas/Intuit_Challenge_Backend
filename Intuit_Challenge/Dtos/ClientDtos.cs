using Intuit_Challenge.Validations;
using System.ComponentModel.DataAnnotations;

namespace Intuit_Challenge.Dtos
{
    public class ClientDtos
    {
        public class GetClientDto
        {
            public int Id { get; set; }
            public string Names { get; set; }
            public string LastNames { get; set; }
            public string Cuit { get; set; }
            public string Address { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public DateTime Birthday { get; set; }
        }

        public class CreateClientDto
        {
            public string Names { get; set; }
            public string LastNames { get; set; }
            public string Cuit { get; set; }
            public string Address { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public DateTime Birthday { get; set; }
        }

        public class UpdateClientDto
        {
            public int Id { get; set; }
            public string Names { get; set; }
            public string LastNames { get; set; }
            public string Cuit { get; set; }
            public string Address { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public DateTime Birthday { get; set; }
        }
    }
}
