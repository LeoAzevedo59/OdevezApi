using AutoMapper;
using Odevez.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientBusiness(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task CreatAsync(ClientDTO client)
        {
            if (client.Id <= 0 || !string.IsNullOrEmpty(client.Name) || !string.IsNullOrEmpty(client.Email) || !string.IsNullOrEmpty(client.PhoneNumber) || !string.IsNullOrEmpty(client.Adress))
                new Exception("Campo inválido");

            await _clientRepository.CreatAsync(_mapper.Map<ClientModel>(client));
        }

        public async Task DeleteAsync(int clientId)
        {
            await _clientRepository.DeleteAsync(clientId);
        }

        public async Task<bool> ExistsByIdAsync(int clientId)
        {
            return await _clientRepository.ExistsByIdAsync(clientId);
        }

        public async Task<ClientDTO> GetByIdAsync(int clientId)
        {
            var retorno = await _clientRepository.GetByIdAsync(clientId);
            return _mapper.Map<ClientDTO>(retorno);
        }

        public async Task<List<ClientDTO>> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            var retorno = await _clientRepository.ListByFilterAsync(clientId, name);
            return _mapper.Map<List<ClientDTO>>(retorno);
        }

        public async Task UpdateAsync(ClientDTO client)
        {
            await _clientRepository.UpdateAsync(_mapper.Map<ClientModel>(client));
        }
    }
}
