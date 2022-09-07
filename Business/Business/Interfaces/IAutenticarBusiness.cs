using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IAutenticarBusiness
    {
        string CriptografarSenha(string password);
        Task<TokenDTO> LoginClient(long phoneNumber, string password);
    }
}
