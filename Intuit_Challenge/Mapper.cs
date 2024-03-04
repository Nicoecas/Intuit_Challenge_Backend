using AutoMapper;
using Intuit_Challenge.Entities;
using static Intuit_Challenge.Dtos.ClientDtos;

namespace Intuit_Challenge
{
    public class Mapper
    {
        public class ClientProfile : Profile
        {
            public ClientProfile()
            {
                CreateMap<Client, GetClientDto>();
                CreateMap<CreateClientDto, Client>();
            }
        }
    }
}
