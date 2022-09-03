using Odevez.Business.Interfaces;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business
{
    public class ClientController : IClientBusiness
    {
        private readonly IClientBusiness _clientBusiness;

        public ClientController(IClientBusiness clientBusiness)
        {
            _clientBusiness = clientBusiness;
        }

        public async Task CreatAsync(ClientDTO client)
        {
            await _clientBusiness.CreatAsync(client);
        }

        public async Task DeleteAsync(int clientId)
        {
            await _clientBusiness.DeleteAsync(clientId);
        }

        public async Task<bool> ExistsByIdAsync(int clientId)
        {
            return await _clientBusiness.ExistsByIdAsync(clientId);
        }

        public async Task<ClientDTO> GetByIdAsync(int clientId)
        {
            return await _clientBusiness.GetByIdAsync(clientId);
        }

        public async Task<List<ClientDTO>> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            return await _clientBusiness.ListByFilterAsync(clientId, name);
        }

        public async Task UpdateAsync(ClientDTO client)
        {
            await _clientBusiness.UpdateAsync(client);
        }
    }
}
