using Odevez.DTO;
using Odevez.Utils.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface ICarteiraRepository
    {
        Task<decimal> ObterValorCarteiraPorUsuario(int usuario);
        Task<List<CarteiraDTO>> ObterDescricaoCarteiraPorUsuario(int usuario);
        Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira();
        Task<List<CategoriaDTO>> ObterCategoriaCarteiraPorUsuario(int usuario);
        Task<bool> AlterarValorCarteira(int codigo, decimal valorCarteira);
        Task<decimal> ObterValorCarteira(int codigo);
        Task<List<TipoCarteiraDTO>> ObterTipoCarteira();
        Task<bool> IncluirTransacaoCarteira(TipoCarteiraDTO tipoCarteira);
        Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira);
        Task<decimal> ObterValorCarteiraPorTipoCarteira(int usuario, int tipoCarteira);
        Task ExcluirCarteira(int usuario, int carteira);
    }
}
