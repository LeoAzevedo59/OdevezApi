using Odevez.Business.Business.Interfaces;
using Odevez.Repository.Repositorys.Interfaces;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class CarteiraBusiness : ICarteiraBusiness
    {
        private readonly ICarteiraRepository _carteiraRepository;

        public CarteiraBusiness(ICarteiraRepository carteiraRepository)
        {
            _carteiraRepository = carteiraRepository;
        }

        public async Task<decimal> ObterValorCarteiraPorUsuario(int usuario)
        {
            return await _carteiraRepository.ObterValorCarteiraPorUsuario(usuario);
        }
    }
}
