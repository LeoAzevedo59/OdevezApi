using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IUsuarioBusiness
    {
        Task<bool> InserirUsuario(UsuarioDTO usuario);
        Task<bool> VerificarCPF(string celular);
        Task<UsuarioDTO> ObterUsuarioPorCelular(string celular);
    }
}
