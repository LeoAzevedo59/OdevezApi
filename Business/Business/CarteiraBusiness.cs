using AutoMapper;
using Odevez.API.ViewModel;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Repository.UnitOfWork;
using Odevez.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class CarteiraBusiness : ICarteiraBusiness
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarteiraBusiness(ICarteiraRepository carteiraRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _carteiraRepository = carteiraRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> ObterValorCarteiraPorUsuario(int usuario)
        {
            return await _carteiraRepository.ObterValorCarteiraPorUsuario(usuario);
        }

        public async Task<List<CarteiraExtratoViewModel>> ObterDescricaoCarteiraPorUsuario(int usuario)
        {
            try
            {
                var retornoDTO = await _carteiraRepository.ObterDescricaoCarteiraPorUsuario(usuario);
                var carteiraViewModel = _mapper.Map<List<CarteiraExtratoViewModel>>(retornoDTO);
                return carteiraViewModel;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira()
        {
            return await _carteiraRepository.ObterMovimentacaoCarteira();
        }

        public async Task<List<CategoriaExtratoViewModel>> ObterCategoriaCarteiraPorUsuario(int usuario)
        {
            try
            {
                var retornoDTO = await _carteiraRepository.ObterCategoriaCarteiraPorUsuario(usuario);
                var categoriaViewModel = _mapper.Map<List<CategoriaExtratoViewModel>>(retornoDTO);
                return categoriaViewModel;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> IncluirTransacaoCarteira(ExtratoViewModel extratoViewModel)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                decimal valorCarteira = 0;

                ExtratoDTO extrato = popularExtrato(extratoViewModel);
                var retorno = await _carteiraRepository.IncluirTransacaoCarteira(extrato);

                if (retorno)
                    valorCarteira = await ObterValorCarteira(extrato.Carteira.Codigo);

                if (extrato.Movimentacao.Codigo == (int)MovimentacaoEnum.Entrada)
                    valorCarteira += extrato.Valor;
                else if (extrato.Movimentacao.Codigo == (int)MovimentacaoEnum.Saida)
                    valorCarteira -= extrato.Valor;

                retorno = await _carteiraRepository.AlterarValorCarteira(extrato.Carteira.Codigo, valorCarteira);

                _unitOfWork.CommitTransaction();
                return retorno;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private async Task<decimal> ObterValorCarteira(int codigo)
        {
            return await _carteiraRepository.ObterValorCarteira(codigo);
        }

        private ExtratoDTO popularExtrato(ExtratoViewModel extratoViewModel)
        {
            var extratoDTO = _mapper.Map<ExtratoDTO>(extratoViewModel);
            extratoDTO.DatUltAlt = DateTime.Now.Date;
            return extratoDTO;
        }

        public async Task<List<TipoCarteiraViewModel>> ObterTipoCarteira()
        {
            try
            {
                var retornoDTO = await _carteiraRepository.ObterTipoCarteira();
                var categoriaViewModel = _mapper.Map<List<TipoCarteiraViewModel>>(retornoDTO);
                return categoriaViewModel;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> IncluirTipoCarteira(TipoCarteiraDTO tipoCarteira)
        {
            return await _carteiraRepository.IncluirTransacaoCarteira(tipoCarteira);
        }

        public async Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira)
        {
            return await _carteiraRepository.ObterCarteira(usuario, tipoCarteira);
        }

        public async Task<decimal> ObterValorCarteiraPorTipoCarteira(int usuario, int tipoCarteira)
        {
            return await _carteiraRepository.ObterValorCarteiraPorTipoCarteira(usuario, tipoCarteira);
        }

        public async Task ExcluirCarteira(int usuario, int carteira)
        {
            await _carteiraRepository.ExcluirCarteira(usuario, carteira);
        }
    }
}
