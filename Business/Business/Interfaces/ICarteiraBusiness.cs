using Odevez.API.ViewModel;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using Odevez.Utils.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface ICarteiraBusiness
    {
        Task<decimal> ObterValorCarteiraPorUsuario(int usuario);
        Task<List<CarteiraExtratoViewModel>> ObterDescricaoCarteiraPorUsuario(int usuario);
        Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira();
        Task<List<CategoriaExtratoViewModel>> ObterCategoriaCarteiraPorUsuario(int usuario);
        Task<List<TipoCarteiraViewModel>> ObterTipoCarteira();
        Task<bool> IncluirTipoCarteira(TipoCarteiraDTO tipoCarteira);
        Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira);
        Task<decimal> ObterValorCarteiraPorTipoCarteira(int usuario, int tipoCarteira);
        Task ExcluirCarteira(int usuario, int carteira);
        Task<decimal> ObterValorCarteira(int codigo);
        Task<bool> AlterarValorCarteira(int codigo, decimal valorCarteira);
    }
}
