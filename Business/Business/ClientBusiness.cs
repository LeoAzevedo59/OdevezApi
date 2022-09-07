using AutoMapper;
using Odevez.Business.Business.Interfaces;
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
        private readonly IAutenticarBusiness _autenticarBusiness;
        private readonly IMapper _mapper;

        public ClientBusiness(IClientRepository clientRepository, IMapper mapper, IAutenticarBusiness autenticarBusiness)
        {
            _clientRepository = clientRepository;
            _autenticarBusiness = autenticarBusiness;
            _mapper = mapper;
        }

        public async Task<List<ClientDTO>> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                    name = name.Trim();

                var retorno = await _clientRepository.ListByFilterAsync(clientId, name);
                return _mapper.Map<List<ClientDTO>>(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InserirClient(ClientDTO client)
        {
            try
            {
                if (client != null
                    && !string.IsNullOrEmpty(client.Name)
                    && !string.IsNullOrEmpty(client.Email)
                    && client.PhoneNumber > 0
                    && !string.IsNullOrEmpty(client.Adress)
                    && !string.IsNullOrEmpty(client.Password))
                {
                    string hashPassword = _autenticarBusiness.CriptografarSenha(client.Password);
                    client.PasswordHash = hashPassword;

                    return await _clientRepository.InserirClient(_mapper.Map<ClientModel>(client));
                }
                else
                    throw new Exception($"Campo inválido");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerifyPhoneNumber(long phoneNumber)
        {
            return await _clientRepository.VerifyPhoneNumber(phoneNumber);
        }
    }
}
