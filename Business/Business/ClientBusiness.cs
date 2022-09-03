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

        public async Task<List<ClientDTO>> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            try
            {
                var retorno = await _clientRepository.ListByFilterAsync(clientId, name);
                return _mapper.Map<List<ClientDTO>>(retorno);
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
    }
}
