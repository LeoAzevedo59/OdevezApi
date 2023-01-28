
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
                string query = @"INSERT EXTRATO(DATULTALT, DATACRIACAO, VALOR, CARTEIRA, CATEGORIA, MOVIMENTACAO, DESCRICAO, STATUS)
                                        VALUES (@DATULTALT, @DATACRIACAO, @VALOR, @CARTEIRA, @CATEGORIA, @MOVIMENTACAO, @DESCRICAO, @STATUS)";

                parameters.Add("@DATULTALT", extrato.DatUltAlt);
                parameters.Add("@DATACRIACAO", extrato.DataCriacao);
                parameters.Add("@VALOR", extrato.Valor);
                parameters.Add("@STATUS", extrato.Status);
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
                                    E.STATUS,
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
                                    INNER JOIN CARTEIRA C ON E.CARTEIRA = C.CODIGO
                                    INNER JOIN MOVIMENTACAO M ON E.MOVIMENTACAO = M.CODIGO
                                    INNER JOIN CATEGORIA CT ON E.CATEGORIA = CT.CODIGO
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

        public async Task<decimal> ObterValorExtratoPorCodigo(int extrato)
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
                var parameters = new DynamicParameters();

                decimal valorMes = await ObterValorExtratoPorData(dtInicio, dtFim, carteira);

                string query = @$"  SELECT
                                    E.CODIGO,
                                    E.DATACRIACAO,
                                    E.VALOR,
                                    E.DESCRICAO,
                                    E.STATUS,
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
                                    INNER JOIN CARTEIRA C ON E.CARTEIRA = C.CODIGO
                                    INNER JOIN MOVIMENTACAO M ON E.MOVIMENTACAO = M.CODIGO
                                    INNER JOIN CATEGORIA CT ON E.CATEGORIA = CT.CODIGO
                                    WHERE C.USUARIO = @USUARIO AND E.DATACRIACAO BETWEEN @DATAINICIO AND @DATAFIM
                                    CONDICAO
                                    ORDER BY E.CODIGO DESC";

                parameters.Add("@USUARIO", usuario);
                parameters.Add("@DATAINICIO", dtInicio);
                parameters.Add("@DATAFIM", dtFim);

                if (carteira > 0)
                {
                    query = query.Replace("CONDICAO", "AND C.CODIGO = @CARTEIRA");
                    parameters.Add("@CARTEIRA", carteira);
                }
                else
                {
                    query = query.Replace("CONDICAO", "");
                }

                retorno = (await _dbConnector.dbConnection.QueryAsync<ExtratoDTO, CategoriaDTO, CarteiraDTO, MovimentacaoDTO, ExtratoDTO>(
                    sql: query,
                    param: parameters,
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
                                    WHERE STATUS = 1 AND DATACRIACAO BETWEEN '{dtInicio}' AND '{dtFim}'";

                if (carteira > 0)
                    query += $" AND CARTEIRA = {carteira}";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AlterarStatus(ExtratoStatusDTO extrato)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"UPDATE EXTRATO
	                                SET STATUS = @STATUS
                                 WHERE CODIGO = @CODIGO";

                parameters.Add("@STATUS", extrato.Status);
                parameters.Add("@CODIGO", extrato.Codigo);


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

        public async Task<ExtratoDTO> ObterExtratoPorCodigo(int extrato)
        {
            try
            {
                var retorno = new ExtratoDTO();

                string query = @$"  SELECT
                                    E.CODIGO,
                                    E.DATACRIACAO,
                                    E.VALOR,
                                    E.DESCRICAO,
                                    E.STATUS,
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
                                    INNER JOIN CARTEIRA C ON E.CARTEIRA = C.CODIGO
                                    INNER JOIN MOVIMENTACAO M ON E.MOVIMENTACAO = M.CODIGO
                                    INNER JOIN CATEGORIA CT ON E.CATEGORIA = CT.CODIGO
                                    WHERE E.CODIGO = {extrato}";

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
                    )).FirstOrDefault();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Alterar(ExtratoDTO objAlterar)
        {
            try
            {
                var parameters = new DynamicParameters();
                string setParam = "";
                string query = @"   UPDATE EXTRATO
	                                    SET {SET}
                                    WHERE CODIGO = @CODIGO";

                parameters.Add("@CODIGO", objAlterar.Codigo);

                #region VALIDAR CAMPO VAZIO

                setParam = " DATULTALT = @DATULTALT";

                if (objAlterar.DataCriacao != null)
                {
                    setParam += ",DATACRIACAO = @DATACRIACAO";
                    parameters.Add("@DATACRIACAO", objAlterar.DataCriacao);
                }

                if (!string.IsNullOrEmpty(objAlterar.Descricao))
                {
                    setParam += ",DESCRICAO = @DESCRICAO";
                    parameters.Add("@DESCRICAO", objAlterar.Descricao);
                }

                if (objAlterar.Valor != null)
                {
                    setParam += ",VALOR = @VALOR";
                    parameters.Add("@VALOR", objAlterar.Valor);
                }

                if ((int)objAlterar.Status > 0)
                {
                    setParam += ",STATUS = @STATUS";
                    parameters.Add("@STATUS", objAlterar.Status);
                }

                if (objAlterar.Carteira != null && objAlterar.Carteira.Codigo > 0)
                {
                    setParam += ",CARTEIRA = @CARTEIRA";
                    parameters.Add("@CARTEIRA", objAlterar.Carteira.Codigo);
                }

                if (objAlterar.Categoria != null && objAlterar.Categoria.Codigo > 0)
                {
                    setParam += ",CATEGORIA = @CATEGORIA";
                    parameters.Add("@CATEGORIA", objAlterar.Categoria.Codigo);
                }

                if (objAlterar.Movimentacao != null && objAlterar.Movimentacao.Codigo > 0)
                {
                    setParam += ",MOVIMENTACAO = @MOVIMENTACAO";
                    parameters.Add("@MOVIMENTACAO", objAlterar.Movimentacao.Codigo);
                }

                query = query.Replace("{SET}", setParam);
                parameters.Add("@DATULTALT", objAlterar.DatUltAlt);

                #endregion

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
    }
}
