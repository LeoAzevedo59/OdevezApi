using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IUsuarioBusiness
    {
        Task<long> InserirUsuario(UsuarioDTO usuario);
        Task<bool> VerificarCPF(string celular);
        Task<UsuarioDTO> ObterUsuarioPorCelular(string celular);
        Task<string> InserirApelido(UsuarioDTO usuario);
        Task<string> ObterNomePorCodigo(long usuario);
        Task<string> IncluirImagemPerfilAzure(UploadImageDTO uploadImage);
        Task<bool> Excluir(int user);
    }
}
