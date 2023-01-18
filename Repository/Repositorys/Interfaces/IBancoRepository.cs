using Odevez.DTO;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IBancoRepository
    {
        Task<int> Incluir(BancoDTO banco);
        Task<int> ObterPorIspb(string ispb);
    }
}
