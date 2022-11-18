using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface ICarteiraRepository
    {
        Task<decimal> ObterValorCarteiraPorUsuario(int usuario);
    }
}
