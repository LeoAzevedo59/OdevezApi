using Odevez.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public interface IExtratoRepository
    {
        public Task<List<ExtratoDTO>> ObterExtratoResumido(int usuario);
    }
}
