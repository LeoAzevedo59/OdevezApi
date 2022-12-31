using Odevez.Business.ViewModel;
using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IExtratoBsuiness
    {
        Task<List<ExtratoViewModel>> ObterExtratoResumido(int usuario);
        Task<bool> IncluirTransacaoCarteira(ExtratoViewModel extratoViewModel);
        Task ExcluirExtrato(int extrato, int carteira);
        Task<ExtratoMesFiltroViewModel> ObterExtrato(int usuario, string data, int carteira);
        Task<bool> AlterarStatus(ExtratoStatusDTO extrato);
        Task<ExtratoViewModel> ObterExtratoPorCodigo(int extrato);
        Task<bool> AlterarExtrato(ExtratoViewModel extrato);
    }
}
