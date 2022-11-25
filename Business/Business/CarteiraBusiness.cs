using AutoMapper;
using Odevez.API.ViewModel;
using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class CarteiraBusiness : ICarteiraBusiness
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;

        public CarteiraBusiness(ICarteiraRepository carteiraRepository, IMapper mapper)
        {
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
        }

        public async Task<decimal> ObterValorCarteiraPorUsuario(int usuario)
        {
            return await _carteiraRepository.ObterValorCarteiraPorUsuario(usuario);
        }

        public async Task<List<CarteiraDescricaoViewModel>> ObterDescricaoCarteiraPorUsuario(int usuario)
        {
            var retorno = await _carteiraRepository.ObterDescricaoCarteiraPorUsuario(usuario);
            var viewModel = _mapper.Map<List<CarteiraDescricaoViewModel>>(retorno);
            return viewModel;
        }

        public async Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira()
        {
            return await _carteiraRepository.ObterMovimentacaoCarteira();
        }
    }
}
