using Odevez.Business.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business.Interfaces
{
    public interface IExtratoBsuiness
    {
        public Task<List<ExtratoViewModel>> ObterExtratoResumido(int usuario);
    }
}
