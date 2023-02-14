using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IExtratoRepository
    {
        Task<List<ExtratoDTO>> ObterExtratoResumido(int usuario);
        Task<bool> IncluirExtrato(ExtratoDTO extrato);
        Task ExcluirExtrato(int extrato, int carteira);
        Task<decimal> ObterValorExtratoPorCodigo(int extrato);
        Task<ExtratoMesFiltroDTO> ObterExtrato(int usuario, string dtInicio, string dtFim, int carteira);
        Task<decimal> ObterValorExtratoPorData(string dataInicio, string dtFim, int carteira);
        Task<bool> AlterarStatus(ExtratoStatusDTO extrato);
        Task<ExtratoDTO> ObterExtratoPorCodigo(int extrato);
        Task<bool> Alterar(ExtratoDTO objAlterar);
        Task<List<BalancoDTO>> ObterBalancoMensal(int usuario);
        Task<ResponseDashDTO> ObterDashboardPizza(FiltroDashPizzaDTO filtro);
    }
}
