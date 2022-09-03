using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Interfaces
{
    public interface IClientBusiness
    {
        Task CreatAsync(ClientDTO client);
        Task UpdateAsync(ClientDTO client);
        Task DeleteAsync(int clientId);
        Task<bool> ExistsByIdAsync(int clientId);
        Task<ClientDTO> GetByIdAsync(int clientId);
        Task<List<ClientDTO>> ListByFilterAsync(int? clientId = 0, string name = null);
    }
}
