using Dapper;
using Odevez.DTO;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys
{
    public class ExtratoRepository : IExtratoRepository
    {
        private readonly IDbConnector _dbConnector;

        public ExtratoRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<bool> IncluirExtrato(ExtratoDTO extrato)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"INSERT EXTRATO(DATULTALT, DATACRIACAO, VALOR, CARTEIRA, CATEGORIA, MOVIMENTACAO, DESCRICAO)
                                        VALUES (@DATULTALT, @DATACRIACAO, @VALOR, @CARTEIRA, @CATEGORIA, @MOVIMENTACAO, @DESCRICAO)";

                parameters.Add("@DATULTALT", extrato.DatUltAlt);
                parameters.Add("@DATACRIACAO", extrato.DataCriacao);
                parameters.Add("@VALOR", extrato.Valor);
                parameters.Add("@CARTEIRA", extrato.Carteira.Codigo);
                parameters.Add("@CATEGORIA", extrato.Categoria.Codigo);
                parameters.Add("@MOVIMENTACAO", extrato.Movimentacao.Codigo);
                parameters.Add("@DESCRICAO", extrato.Descricao);

                var retorno = await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);
                if (retorno > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ExtratoDTO>> ObterExtratoResumido(int usuario)
        {
            try
            {
                var retorno = new List<ExtratoDTO>();

                string query = @$"  SELECT
                                    TOP 2
                                    E.CODIGO,
                                    E.DATACRIACAO,
                                    E.VALOR,
                                    E.DESCRICAO,
                                    --CATEGORIA (CT)
                                    CT.CODIGO,
                                    CT.DESCRICAO,
                                    -- CARTEIRA (C)
                                    C.CODIGO,
                                    C.USUARIO,
                                    C.DESCRICAO,
                                    -- MOVIMENTACAO (M)
                                    M.CODIGO,
                                    M.DESCRICAO
                                    FROM EXTRATO E
                                    INNER JOIN CARTEIRA C ON C.CODIGO = E.CARTEIRA
                                    INNER JOIN MOVIMENTACAO M ON M.CODIGO = E.MOVIMENTACAO
                                    INNER JOIN CATEGORIA CT ON CT.CODIGO = E.CATEGORIA
                                    WHERE C.USUARIO = {usuario}
                                    ORDER BY E.CODIGO DESC";

                retorno = (await _dbConnector.dbConnection.QueryAsync<ExtratoDTO, CategoriaDTO, CarteiraDTO, MovimentacaoDTO, ExtratoDTO>(
                    sql: query,
                    splitOn: "Codigo",
                    map: (extrato, categoria, carteira, movimentacao) =>
                    {
                        extrato.Categoria = categoria;
                        extrato.Carteira = carteira;
                        extrato.Movimentacao = movimentacao;
                        return extrato;
                    },
                    transaction: _dbConnector.dbTransaction
                    )).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task ExcluirExtrato(int extrato, int carteira)
        {
            try
            {
                var parameters = new DynamicParameters();
                string queryExtrato = @"  DELETE FROM EXTRATO
                                          WHERE CODIGO = @EXTRATO AND CARTEIRA = @CARTEIRA";

                parameters.Add("@EXTRATO", extrato);
                parameters.Add("@CARTEIRA", carteira);

                await _dbConnector.dbConnection.ExecuteAsync(queryExtrato, param: parameters, transaction: _dbConnector.dbTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> ObterExtratoPorCodigo(int extrato)
        {
            try
            {
                string query = $"SELECT VALOR FROM EXTRATO WHERE CODIGO = {extrato}";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ExtratoMesFiltroDTO> ObterExtrato(int usuario, string dtInicio, string dtFim, int carteira)
        {
            try
            {
                var extratos = new ExtratoMesFiltroDTO();
                var retorno = new List<ExtratoDTO>();

                decimal valorMes = await ObterValorExtratoPorData(dtInicio, dtFim, carteira);

                string query = @$"  SELECT
                                    E.CODIGO,
                                    E.DATACRIACAO,
                                    E.VALOR,
                                    E.DESCRICAO,
                                    --CATEGORIA (CT)
                                    CT.CODIGO,
                                    CT.DESCRICAO,
                                    -- CARTEIRA (C)
                                    C.CODIGO,
                                    C.USUARIO,
                                    C.DESCRICAO,
                                    -- MOVIMENTACAO (M)
                                    M.CODIGO,
                                    M.DESCRICAO
                                    FROM EXTRATO E
                                    INNER JOIN CARTEIRA C ON C.CODIGO = E.CARTEIRA
                                    INNER JOIN MOVIMENTACAO M ON M.CODIGO = E.MOVIMENTACAO
                                    INNER JOIN CATEGORIA CT ON CT.CODIGO = E.CATEGORIA
                                    WHERE C.USUARIO = {usuario} AND E.DATACRIACAO BETWEEN '{dtInicio}' AND '{dtFim}'
                                    ORDER BY E.CODIGO DESC";

                retorno = (await _dbConnector.dbConnection.QueryAsync<ExtratoDTO, CategoriaDTO, CarteiraDTO, MovimentacaoDTO, ExtratoDTO>(
                    sql: query,
                    splitOn: "Codigo",
                    map: (extrato, categoria, carteira, movimentacao) =>
                    {
                        extrato.Categoria = categoria;
                        extrato.Carteira = carteira;
                        extrato.Movimentacao = movimentacao;
                        return extrato;
                    },
                    transaction: _dbConnector.dbTransaction
                    )).ToList();

                extratos.Extratos = retorno;
                extratos.ValorMes = valorMes;

                return extratos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> ObterValorExtratoPorData(string dtInicio, string dtFim, int carteira)
        {
            try
            {
                string query = @$"  SELECT COALESCE(SUM(VALOR),0) FROM EXTRATO
                                    WHERE DATACRIACAO BETWEEN '{dtInicio}' AND '{dtFim}'";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
