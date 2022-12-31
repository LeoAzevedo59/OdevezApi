using Odevez.API.ViewModel;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface ICarteiraBusiness
    {
        Task<List<CarteiraExtratoViewModel>> ObterDescricaoCarteiraPorUsuario(int usuario);
        Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira();
        Task<List<CategoriaExtratoViewModel>> ObterCategoriaCarteiraPorUsuario(int usuario);
        Task<List<TipoCarteiraViewModel>> ObterTipoCarteira();
        Task<bool> Incluir(TipoCarteiraDTO tipoCarteira);
        Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira);
        Task ExcluirCarteira(int usuario, int carteira);
        Task<decimal> ObterValorPorTipo(int tipoCarteira, int usuario);
        Task<decimal> ObterValorPorUsuario(int usuario);
        Task<decimal> ObterValorPorCodigo(int carteira);
    }
}
