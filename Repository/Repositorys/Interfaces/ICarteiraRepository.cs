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
        Task<bool> IncluirTransacaoCarteira(ExtratoDTO movimentacao);
        Task<List<CategoriaDTO>> ObterCategoriaCarteiraPorUsuario(int usuario);
        Task<bool> AlterarValorCarteira(int codigo, decimal valorCarteira);
        Task<decimal> ObterValorCarteira(int codigo);
    }
}
