using Odevez.API.ViewModel;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface ICarteiraBusiness
    {
        Task<decimal> ObterValorCarteiraPorUsuario(int usuario);
        Task<List<CarteiraExtratoViewModel>> ObterDescricaoCarteiraPorUsuario(int usuario);
        Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira();
        Task<bool> IncluirTransacaoCarteira(ExtratoViewModel extratoViewModel);
        Task<List<CategoriaExtratoViewModel>> ObterCategoriaCarteiraPorUsuario(int usuario);
    }
}
