using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IAutenticarBusiness
    {
        string CriptografarSenha(string senha);
        Task<TokenDTO> LoginUsuario(string celular, string senha);
    }
}
