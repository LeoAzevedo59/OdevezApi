using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> InserirUsuario(UsuarioDTO usuario);
        Task<bool> VerificarCPF(string celular);
        Task<bool> ObterUsuarioPorId(long id);
        Task<UsuarioDTO> ObterUsuarioPorCelular(string celular);
        Task<string> InserirApelido(UsuarioDTO usuario);
        Task<string> ObterNomePorCodigo(long usuario);
        Task IncluirImagemPerfil(string nomeImagem, int usuario);
        Task<bool> Excluir(int user);
    }
}
