using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface ICarteiraBusiness
    {
        Task<decimal> ObterValorCarteiraPorUsuario(int usuario);
    }
}
