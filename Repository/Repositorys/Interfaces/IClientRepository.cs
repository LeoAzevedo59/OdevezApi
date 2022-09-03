using Odevez.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IClientRepository
    {
        Task CreatAsync(ClientModel client);
        Task UpdateAsync(ClientModel client);
        Task DeleteAsync(int clientId);
        Task<bool> ExistsByIdAsync(int clientId);
        Task<ClientModel> GetByIdAsync(int clientId);
        Task<List<ClientModel>> ListByFilterAsync(int? clientId = 0, string name = null);
    }
}
