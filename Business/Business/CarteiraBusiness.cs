﻿using AutoMapper;
using Odevez.API.ViewModel;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Repository.UnitOfWork;
using Odevez.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class CarteiraBusiness : ICarteiraBusiness
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IBancoRepository _bancoRepository;
        private readonly IExtratoRepository _extratoRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarteiraBusiness(ICarteiraRepository carteiraRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBancoRepository bancoRepository,
            IExtratoRepository extratoBsuiness
            )
        {
            _carteiraRepository = carteiraRepository;
            _bancoRepository = bancoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _extratoRepository = extratoBsuiness;
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

        public async Task<bool> IncluirTipo(TipoCarteiraDTO tipoCarteira)
        {
            return await _carteiraRepository.IncluirTipo(tipoCarteira);
        }

        public async Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira)
        {
            return await _carteiraRepository.ObterCarteira(usuario, tipoCarteira);
        }

        public async Task ExcluirCarteira(int usuario, int carteira)
        {
            await _carteiraRepository.ExcluirCarteira(usuario, carteira);
        }

        public async Task<decimal> ObterValorPorTipo(int tipoCarteira, int usuario)
        {
            return await _carteiraRepository.ObterValorPorTipo(tipoCarteira, usuario);
        }

        public async Task<decimal> ObterValorPorUsuario(int usuario)
        {
            return await _carteiraRepository.ObterValorPorUsuario(usuario);
        }

        public async Task<decimal> ObterValorPorCodigo(int carteira)
        {
            return await _carteiraRepository.ObterValorPorCodigo(carteira);
        }

        public async Task<bool> Incluir(CarteiraDTO carteira)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var codigoBanco = await _bancoRepository.ObterPorIspb(carteira.BancoDTO.ispb);

                if (codigoBanco > 0)
                    carteira.BancoDTO.Codigo = codigoBanco;
                else
                {
                    var retBanco = await _bancoRepository.Incluir(carteira.BancoDTO);

                    if (retBanco > 0)
                        codigoBanco = await _bancoRepository.ObterPorIspb(carteira.BancoDTO.ispb);
                    else
                        new Exception();
                }

                var retorno = await _carteiraRepository.Incluir(carteira);

                var codigoCarteira = await ObterUltimaCarteiraPorUsuario(carteira.Usuario);

                if (carteira.Valor > 0)
                {
                    var extrato = new ExtratoDTO
                    {
                        Valor = carteira.Valor,
                        Descricao = "Carga inicial",
                        Status = ExtratoStatusEnum.Efetivado,
                        DatUltAlt = DateTime.Now,
                        DataCriacao = DateTime.Now.Date,
                    };

                    var categoria = new CategoriaDTO
                    {
                        Codigo = 6 // Outros
                    };

                    var movimentacao = new MovimentacaoDTO
                    {
                        Codigo = 1 // Entrada
                    };

                    var carteiradto = new CarteiraDTO
                    {
                        Codigo = codigoCarteira
                    };

                    extrato.Categoria = categoria;
                    extrato.Movimentacao = movimentacao;
                    extrato.Carteira = carteiradto;

                    await _extratoRepository.IncluirExtrato(extrato);
                }

                _unitOfWork.CommitTransaction();

                return retorno;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();

                throw;
            }
        }

        public async Task<int> ObterUltimaCarteiraPorUsuario(int usuario)
        {
            return (await _carteiraRepository.ObterUltimaCarteiraPorUsuario(usuario));
        }

        public async Task<List<BancoDTO>> Obter()
        {
            string urlApiContagem = "https://brasilapi.com.br/api/banks/v1/";
            var client = new HttpClient();
            var retorno = new List<BancoDTO>();

            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var response = await client.GetAsync(urlApiContagem);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                retorno = JsonSerializer
                    .Deserialize<List<BancoDTO>>(conteudo, jsonOptions);

                return retorno;
            }

            return retorno;
        }
    }
}
