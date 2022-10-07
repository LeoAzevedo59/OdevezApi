using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IAutenticarRepository
    {
        Task<UsuarioDTO> ObterPasswordHash(string celular);
    }
}
