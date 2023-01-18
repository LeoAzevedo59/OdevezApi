using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Repository.UnitOfWork;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class BancoBusiness : IBancoBusiness
    {
        private readonly IBancoRepository _bancoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BancoBusiness(IBancoRepository bancoRepository, IUnitOfWork unitOfWork)
        {
            _bancoRepository = bancoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Incluir(BancoDTO banco)
        {
            return await _bancoRepository.Incluir(banco);
        }

        public async Task<int> ObterPorIspb(string ispb)
        {
            return await _bancoRepository.ObterPorIspb(ispb);
        }
    }
}
