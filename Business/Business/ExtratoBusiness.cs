using AutoMapper;
using Odevez.Business.Business.Interfaces;
using Odevez.Business.ViewModel;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Repository.UnitOfWork;
using Odevez.Utils;
using Odevez.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> Incluir(ExtratoViewModel extratoViewModel)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                ExtratoDTO extrato = PopularExtrato(extratoViewModel);
                var retorno = await _extratoRepository.IncluirExtrato(extrato);

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
                decimal valorExtrato = await ObterValorExtratoPorCodigo(extrato);

                _unitOfWork.BeginTransaction();

                await _extratoRepository.ExcluirExtrato(extrato, carteira);

                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        public async Task<decimal> ObterValorExtratoPorCodigo(int extrato)
        {
            try
            {
                return await _extratoRepository.ObterValorExtratoPorCodigo(extrato);
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

        public async Task<bool> AlterarStatus(ExtratoStatusDTO extrato)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                if (extrato.StatusOld == ExtratoStatusEnum.Efetivado)
                    extrato.Status = ExtratoStatusEnum.Pendente;
                else if (extrato.StatusOld == ExtratoStatusEnum.Pendente)
                    extrato.Status = ExtratoStatusEnum.Efetivado;

                var retorno = await _extratoRepository.AlterarStatus(extrato);

                _unitOfWork.CommitTransaction();

                return retorno;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }

        }

        public async Task<ExtratoViewModel> ObterExtratoPorCodigo(int extrato)
        {
            try
            {
                var retornoDTO = await _extratoRepository.ObterExtratoPorCodigo(extrato);
                var extratoViewModel = _mapper.Map<ExtratoViewModel>(retornoDTO);
                return extratoViewModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> AlterarExtrato(ExtratoViewModel extrato)
        {
            try
            {
                bool retorno = false;
                var extratoNew = _mapper.Map<ExtratoDTO>(extrato);

                var extratoViewOld = await ObterExtratoPorCodigo(extratoNew.Codigo);
                var extratoOld = _mapper.Map<ExtratoDTO>(extratoViewOld);

                var objAlterar = ValidarAlteracao(extratoOld, extratoNew);

                _unitOfWork.BeginTransaction();

                #region Alterou Status

                if (!extratoOld.Status.Equals(extratoNew.Status))
                    objAlterar.Status = extratoNew.Status;

                #endregion

                #region Alterou Carteira

                if (!extratoOld.Carteira.Codigo.Equals(extratoNew.Carteira.Codigo))
                    objAlterar.Carteira.Codigo = extratoNew.Carteira.Codigo;

                #endregion

                #region Alterou Movimentacao

                if (!extratoOld.Movimentacao.Codigo.Equals(extratoNew.Movimentacao.Codigo))
                    objAlterar.Movimentacao.Codigo = extratoNew.Movimentacao.Codigo;

                #endregion

                retorno = await _extratoRepository.Alterar(objAlterar);

                _unitOfWork.CommitTransaction();

                return retorno;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private ExtratoDTO ValidarAlteracao(ExtratoDTO extratoOld, ExtratoDTO extratoNew)
        {
            var retorno = new ExtratoDTO();
            retorno.Categoria = new CategoriaDTO();
            retorno.Movimentacao = new MovimentacaoDTO();
            retorno.Carteira = new CarteiraDTO();

            retorno.DatUltAlt = DateTime.Now.Date;
            retorno.Codigo = extratoNew.Codigo;

            if (!extratoOld.DataCriacao.Equals(extratoNew.DataCriacao))
                retorno.DataCriacao = extratoNew.DataCriacao;

            if (extratoNew.Movimentacao.Codigo == (int)MovimentacaoEnum.Saida)
                extratoNew.Valor *= -1;

            if (!extratoOld.Valor.Equals(extratoNew.Valor))
                retorno.Valor = extratoNew.Valor;

            if (!extratoOld.Descricao.Equals(extratoNew.Descricao))
                retorno.Descricao = extratoNew.Descricao;

            if (!extratoOld.Categoria.Codigo.Equals(extratoNew.Categoria.Codigo))
                retorno.Categoria.Codigo = extratoNew.Categoria.Codigo;

            return retorno;
        }

        public async Task<List<BalancoDTO>> ObterBalancoMensal(int usuario)
        {
            try
            {
                var balancos = await _extratoRepository.ObterBalancoMensal(usuario);

                if (!balancos.Any())
                {
                    var mesRetroativo = DateTime.Now.Date.AddMonths(-1).Month.ToString();
                    var ano = DateTime.Now.Year;
                    var anoString = ano.ToString().Substring(2, 2);

                    var mesAtual = DateTime.Now.Date.Month.ToString();

                    var retorno = new List<BalancoDTO>
                    {
                        new BalancoDTO{   Data = $"{mesRetroativo}/{anoString}", Valor = 0},
                        new BalancoDTO{   Data = $"{mesAtual}/{anoString}", Valor = 0}
                    };

                    return retorno;
                }

                if (balancos.Count() == 1)
                {
                    var mes = DateTime.Now.Date.AddMonths(-1).Month;
                    var ano = DateTime.Now.Year;
             
                    var retorno = new List<BalancoDTO>
                    {
                        new BalancoDTO{   Mes = mes, Ano = ano, Valor = 0},
                    };

                    retorno.AddRange(balancos);
                    balancos.Clear();
                    balancos = retorno;
                }

                foreach (var balanco in balancos)
                {
                    string mes = DataUtils.ObterMesString(balanco.Mes);
                    string ano = balanco.Ano.ToString().Substring(2, 2);
                    balanco.Data = $"{mes}/{ano}";
                }

                return balancos;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
