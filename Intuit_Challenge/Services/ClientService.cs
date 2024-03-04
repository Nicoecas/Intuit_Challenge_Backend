using AutoMapper;
using Intuit_Challenge.Data;
using Intuit_Challenge.Entities;
using Microsoft.EntityFrameworkCore;
using static Intuit_Challenge.Dtos.ClientDtos;

namespace Intuit_Challenge.Services
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClients(string name, string lastName);
        Task<Client> GetClientId(int id);
        Task<int> CreateClient(Client clientReq);
        Task<int> UpdateClient(Client client);
        Task DeleteClient(int id);
    }
    public class ClientService : IClientService
    {
        private readonly IAsyncRepository<Client> _repository;
        private readonly IMapper _mapper;

        public ClientService(IAsyncRepository<Client> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<Client>> GetAllClients(string name, string lastName)
        {
            var clients = await _repository.GetSet()
                .Where(x => !string.IsNullOrEmpty(name) ? x.Names.Trim().Contains(name) : x.Id >= 0
                && !string.IsNullOrEmpty(lastName) ? x.Names.Trim().Contains(lastName) : x.Id >= 0)
                .ToListAsync();
            return clients;
        }
        public async Task<Client> GetClientId(int id)
        {
            var client = await _repository.GetSet().Where(x => id == x.Id).FirstOrDefaultAsync();
            if (client == null)
            {
                throw new Exception("Client Not Found");
            }
            return client;
        }

        public async Task<int> CreateClient(Client clientReq)
        {
            var client = await _repository.AddAsync(clientReq);
            return client.Id;
        }
        public async Task<int> UpdateClient(Client client)
        {
            var exist = _repository.ExistId(client.Id);
            if (!exist)
            {
                throw new Exception("Client Not Found");
            }
            await _repository.UpdateAsync(client);
            return client.Id;
        }

        public async Task DeleteClient(int id)
        {
            var client = await _repository.GetSet().Where(x => id == x.Id).FirstOrDefaultAsync();
            if (client == null)
            {
                throw new Exception("Client Not Found");
            }
            await _repository.DeleteAsync(client);
        }
    }
}
