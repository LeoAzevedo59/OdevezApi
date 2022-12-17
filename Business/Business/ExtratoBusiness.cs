using AutoMapper;
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
    public class ExtratoBusiness : IExtratoBsuiness
    {
        private readonly IExtratoRepository _extratoRepository;
        private readonly ICarteiraBusiness _carteiraBusiness;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExtratoBusiness(IExtratoRepository extratoRepository, IMapper mapper, IUnitOfWork unitOfWork, ICarteiraBusiness carteiraBusiness)
        {
            _extratoRepository = extratoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _carteiraBusiness = carteiraBusiness;
        }

        public async Task<bool> IncluirTransacaoCarteira(ExtratoViewModel extratoViewModel)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                decimal valorCarteira = 0;

                ExtratoDTO extrato = PopularExtrato(extratoViewModel);
                var retorno = await _extratoRepository.IncluirExtrato(extrato);

                if (retorno)
                    valorCarteira = await _carteiraBusiness.ObterValorCarteira(extrato.Carteira.Codigo);

                valorCarteira += extrato.Valor;

                retorno = await _carteiraBusiness.AlterarValorCarteira(extrato.Carteira.Codigo, valorCarteira);

                _unitOfWork.CommitTransaction();
                return retorno;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private ExtratoDTO PopularExtrato(ExtratoViewModel extratoViewModel)
        {
            var extratoDTO = _mapper.Map<ExtratoDTO>(extratoViewModel);

            if (extratoDTO.Movimentacao.Codigo == (int)MovimentacaoEnum.Saida)
                extratoDTO.Valor *= -1;

            extratoDTO.DatUltAlt = DateTime.Now.Date;
            return extratoDTO;
        }

        public async Task<List<ExtratoViewModel>> ObterExtratoResumido(int usuario)
        {
            try
            {
                var retornoDTO = await _extratoRepository.ObterExtratoResumido(usuario);
                var extratoViewModel = _mapper.Map<List<ExtratoViewModel>>(retornoDTO);
                return extratoViewModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ExcluirExtrato(int extrato, int carteira)
        {
            try
            {
                decimal valorExtrato = await ObterExtratoPorCodigo(extrato);
                decimal valorCarteira = await _carteiraBusiness.ObterValorCarteira(carteira);
                decimal novoValorCarteira = valorCarteira - valorExtrato;

                _unitOfWork.BeginTransaction();

                await _extratoRepository.ExcluirExtrato(extrato, carteira);
                await _carteiraBusiness.AlterarValorCarteira(carteira, novoValorCarteira);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<decimal> ObterExtratoPorCodigo(int extrato)
        {
            try
            {
                return await _extratoRepository.ObterExtratoPorCodigo(extrato);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ExtratoMesFiltroViewModel> ObterExtrato(int usuario, string data, int carteira)
        {
            try
            {
                var datas = ObterDatasStringFormat(data);
                var retornoDTO = await _extratoRepository.ObterExtrato(usuario, datas[0], datas[1], carteira);
                var extratoViewModel = _mapper.Map<ExtratoMesFiltroViewModel>(retornoDTO);
                return extratoViewModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<string> ObterDatasStringFormat(string data)
        {
            var datas = new List<string>();

            int[] ultimoDiaMes = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            var mes = data.Split("-");
            var ultimoDia = ultimoDiaMes[ObterMesNumerico(mes[1]) - 1];

            int mesNumeric = ObterMesNumerico(mes[1]);
            string ano = mes[0];


            string dataInicio = $"{ano}-{mesNumeric}-1";
            string dataFim = $"{ano}-{mesNumeric}-{ultimoDia}";

            datas.Add(dataInicio);
            datas.Add(dataFim);

            return datas;
        }

        private int ObterMesNumerico(string mesString)
        {
            switch (mesString.ToUpper())
            {
                case "JANEIRO":
                    return 1;
                case "FEVEREIRO":
                    return 2;
                case "MARÇO":
                    return 3;
                case "ABRIL":
                    return 4;
                case "MAIO":
                    return 5;
                case "JUNHO":
                    return 6;
                case "JULHO":
                    return 7;
                case "AGOSTO":
                    return 8;
                case "SETEMBRO":
                    return 9;
                case "OUTUBRO":
                    return 10;
                case "NOVEMBRO":
                    return 11;
                case "DEZEMBRO":
                    return 12;
            }
            return 0;
        }
    }
}
