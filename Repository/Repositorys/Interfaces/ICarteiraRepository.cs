using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface ICarteiraRepository
    {
        Task<decimal> ObterValorCarteiraPorUsuario(int usuario);
        Task<List<CarteiraDTO>> ObterDescricaoCarteiraPorUsuario(int usuario);
        Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira();
    }
}
