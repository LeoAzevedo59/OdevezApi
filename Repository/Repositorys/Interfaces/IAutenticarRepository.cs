using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IAutenticarRepository
    {
        Task<ClientDTO> ObterPasswordHash(long phoneNumber);
    }
}
