using Odevez.Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IClientRepository
    {
        Task<List<ClientModel>> ListByFilterAsync(int? clientId = 0, string name = null);
    }
}
