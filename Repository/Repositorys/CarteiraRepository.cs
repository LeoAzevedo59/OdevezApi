using Dapper;
using Dapper.Contrib.Extensions;
using Odevez.DTO;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Utils.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys
{
    public class CarteiraRepository : ICarteiraRepository
    {
        private readonly IDbConnector _dbConnector;

        public CarteiraRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<List<CarteiraDTO>> ObterDescricaoCarteiraPorUsuario(int usuario)
        {
            try
            {
                var retorno = new List<CarteiraDTO>();
                string query = $"SELECT CODIGO,DESCRICAO FROM CARTEIRA WHERE USUARIO = '{usuario}'";

                retorno = (await _dbConnector.dbConnection.QueryAsync<CarteiraDTO>(query, transaction: _dbConnector.dbTransaction)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MovimentacaoDTO>> ObterMovimentacaoCarteira()
        {
            try
            {
                var retorno = new List<MovimentacaoDTO>();
                string query = $"SELECT CODIGO,DESCRICAO FROM MOVIMENTACAO";

                retorno = (await _dbConnector.dbConnection.QueryAsync<MovimentacaoDTO>(query, transaction: _dbConnector.dbTransaction)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CategoriaDTO>> ObterCategoriaCarteiraPorUsuario(int usuario)
        {
            var retorno = new List<CategoriaDTO>();

            try
            {
                string query = @$"SELECT 
                                    C.CODIGO,
                                    C.DESCRICAO,
                                    C.USUARIO
                                  FROM 
                                    CATEGORIA C
                                    LEFT JOIN USUARIO U ON C.USUARIO = U.CODIGO
                                WHERE
                                    C.USUARIO IS NULL OR C.USUARIO = {usuario}
                                ORDER BY CODIGO DESC";

                retorno = (await _dbConnector.dbConnection.QueryAsync<CategoriaDTO>(query, transaction: _dbConnector.dbTransaction)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }

        public async Task<List<TipoCarteiraDTO>> ObterTipoCarteira()
        {
            try
            {
                var retorno = new List<TipoCarteiraDTO>();
                string query = $"SELECT * FROM TIPOCARTEIRA";

                retorno = (await _dbConnector.dbConnection.QueryAsync<TipoCarteiraDTO>(query, transaction: _dbConnector.dbTransaction)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IncluirTipo(TipoCarteiraDTO tipoCarteira)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"   INSERT CARTEIRA(DATULTALT, DATACRIACAO, USUARIO, TIPOCARTEIRA, DESCRICAO, FECHAMENTOFATURA, VENCIMENTOFATURA)
                                    VALUES(@DATULTALT, @DATACRIACAO, @USUARIO, @TIPOCARTEIRA, @DESCRICAO, @FECHAMENTOFATURA, @VENCIMENTOFATURA)";

                parameters.Add("@DATULTALT", DateTime.Now.Date);
                parameters.Add("@DATACRIACAO", DateTime.Now.Date);
                parameters.Add("@USUARIO", tipoCarteira.Usuario);
                parameters.Add("@TIPOCARTEIRA", tipoCarteira.TipoCarteira);
                parameters.Add("@DESCRICAO", tipoCarteira.Descricao);
                parameters.Add("@FECHAMENTOFATURA", tipoCarteira.FechamentoFatura);
                parameters.Add("@VENCIMENTOFATURA", tipoCarteira.VencimentoFatura);

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

        public async Task<List<CarteiraDTO>> ObterCarteira(int usuario, int tipoCarteira)
        {
            try
            {

                var retorno = new List<CarteiraDTO>();
                string query = @$"    SELECT 
                                      C.CODIGO,
                                      C.USUARIO,
                                      C.TIPOCARTEIRA,
                                      C.DESCRICAO,
                                      C.FECHAMENTOFATURA,
                                      C.VENCIMENTOFATURA,
                                      C.CHKEXIBIRHOME,
                                      B.CODIGO,
                                      B.NAME
                                      FROM CARTEIRA C
                                      LEFT JOIN BANCO B ON C.BANCO = B.CODIGO
                                      WHERE C.USUARIO = {usuario}";

                if (tipoCarteira > 0)
                    query += $" AND TIPOCARTEIRA = { tipoCarteira }";

                retorno = (await _dbConnector.dbConnection.QueryAsync<CarteiraDTO, BancoDTO, CarteiraDTO>(
                    sql: query,
                    splitOn: "Codigo",
                    map: (carteira, banco) =>
                    {
                        carteira.BancoDTO = banco;
                        return carteira;
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

        public async Task ExcluirCarteira(int usuario, int carteira)
        {
            try
            {
                var parameters = new DynamicParameters();
                string queryExtrato = @" DELETE FROM EXTRATO
                                         WHERE CARTEIRA = @CODIGO";

                parameters.Add("@CODIGO", carteira);

                await _dbConnector.dbConnection.ExecuteAsync(queryExtrato, param: parameters, transaction: _dbConnector.dbTransaction);

                string query = @"   DELETE FROM CARTEIRA
                                    WHERE CODIGO = @CODIGO AND USUARIO = @USUARIO";

                parameters.Add("@USUARIO", usuario);

                await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> ObterValorPorTipo(int tipoCarteira, int usuario)
        {
            try
            {
                decimal retorno = decimal.Zero;
                string query = @$"  SELECT COALESCE(SUM(E.VALOR),0) FROM EXTRATO E
                                    INNER JOIN CARTEIRA C ON E.CARTEIRA = C.CODIGO
                                    WHERE E.STATUS = 1 AND C.USUARIO = {usuario} AND C.CHKNAOSOMARPATRIMONIO = 0";

                if (tipoCarteira > 1)
                    query += $" AND C.TIPOCARTEIRA = {tipoCarteira}";

                retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> ObterValorPorUsuario(int usuario)
        {
            try
            {
                decimal retorno = decimal.Zero;
                string query = @$"  SELECT COALESCE(SUM(E.VALOR),0) FROM EXTRATO E
                                    INNER JOIN CARTEIRA C ON E.CARTEIRA = C.CODIGO
                                    WHERE E.STATUS = 1 AND C.USUARIO = {usuario} AND C.CHKNAOSOMARPATRIMONIO = 0";

                retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> ObterValorPorCodigo(int carteira)
        {
            try
            {
                decimal retorno = decimal.Zero;
                string query = @$"  SELECT COALESCE(SUM(E.VALOR),0) FROM EXTRATO E
                                    INNER JOIN CARTEIRA C ON E.CARTEIRA = C.CODIGO
                                    WHERE E.STATUS = 1 AND E.CARTEIRA = {carteira}";

                retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Incluir(CarteiraDTO carteira)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"   INSERT CARTEIRA(DATULTALT, DATACRIACAO, USUARIO, TIPOCARTEIRA, DESCRICAO, FECHAMENTOFATURA, VENCIMENTOFATURA, CHKEXIBIRHOME, CHKNAOSOMARPATRIMONIO :BANCO)
                                    VALUES(@DATULTALT, @DATACRIACAO, @USUARIO, @TIPOCARTEIRA, @DESCRICAO, @FECHAMENTOFATURA, @VENCIMENTOFATURA, @CHKEXIBIRHOME, @CHKNAOSOMARPATRIMONIO :@BANCO)";

                parameters.Add("@DATULTALT", DateTime.Now);
                parameters.Add("@DATACRIACAO", DateTime.Now.Date);
                parameters.Add("@USUARIO", carteira.Usuario);
                parameters.Add("@TIPOCARTEIRA", carteira.TipoCarteira);
                parameters.Add("@DESCRICAO", carteira.Descricao);
                parameters.Add("@FECHAMENTOFATURA", carteira.FechamentoFatura);
                parameters.Add("@VENCIMENTOFATURA", carteira.VencimentoFatura);
                parameters.Add("@CHKEXIBIRHOME", carteira.ChkExibirHome);
                parameters.Add("@CHKNAOSOMARPATRIMONIO", carteira.ChkNaoSomarPatrimonio);

                if (carteira.BancoDTO.code != null)
                {
                    query = query.Replace(":BANCO", ", BANCO");
                    query = query.Replace(":@BANCO", ", @BANCO");
                    parameters.Add("@BANCO", carteira.BancoDTO.Codigo);
                }
                else
                {
                    query = query.Replace(":BANCO", "");
                    query = query.Replace(":@BANCO", "");
                }


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

        public async Task<int> ObterUltimaCarteiraPorUsuario(int usuario)
        {
            try
            {
                string query = @$"  SELECT TOP 1 CODIGO FROM CARTEIRA
                                    WHERE USUARIO = {usuario}
                                    ORDER BY CODIGO DESC";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<int>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CarteiraDTO> ObterPorCodigo(int carteira, int usuario)
        {
            try
            {
                string query = @$"  SELECT
                                    TOP 1
                                    *
                                    FROM CARTEIRA C
                                    LEFT JOIN BANCO B ON C.BANCO = B.CODIGO
                                    WHERE C.CODIGO = {carteira} AND C.USUARIO = {usuario}
                                    ";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<CarteiraDTO, BancoDTO, CarteiraDTO>(
                    query,
                    transaction: _dbConnector.dbTransaction,
                    splitOn: "Codigo",
                    map: (carteira, banco) =>
                    {
                        carteira.BancoDTO = banco;
                        return carteira;
                    })).FirstOrDefault();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Alterar(CarteiraDTO carteiraDTO)
        {
            try
            {
                var parameters = new DynamicParameters();
                var setParams = "";

                string query = @"   UPDATE CARTEIRA
	                                    SET DATULTALT = @DATULTALT
                                        {PARAMS}
                                    WHERE CODIGO = @CODIGO AND USUARIO = @USUARIO";

                parameters.Add("@DATULTALT", DateTime.Now);

                if (carteiraDTO.TipoCarteira != 0)
                {
                    parameters.Add("@TIPOCARTEIRA", carteiraDTO.TipoCarteira);
                    setParams += ",TIPOCARTEIRA = @TIPOCARTEIRA";
                }

                if (!string.IsNullOrEmpty(carteiraDTO.Descricao))
                {
                    setParams += ",DESCRICAO = @DESCRICAO";
                    parameters.Add("@DESCRICAO", carteiraDTO.Descricao);
                }

                if (carteiraDTO.FechamentoFatura != null)
                {
                    setParams += ",FECHAMENTOFATURA = @FECHAMENTOFATURA";
                    parameters.Add("@FECHAMENTOFATURA", carteiraDTO.FechamentoFatura);
                }

                if (carteiraDTO.VencimentoFatura != null)
                {
                    setParams += ",VENCIMENTOFATURA = @VENCIMENTOFATURA";
                    parameters.Add("@VENCIMENTOFATURA", carteiraDTO.VencimentoFatura);
                }

                setParams += ",CHKEXIBIRHOME = @CHKEXIBIRHOME";
                parameters.Add("@CHKEXIBIRHOME", carteiraDTO.ChkExibirHome);
                setParams += ",CHKNAOSOMARPATRIMONIO = @CHKNAOSOMARPATRIMONIO";
                parameters.Add("@CHKNAOSOMARPATRIMONIO", carteiraDTO.ChkNaoSomarPatrimonio);

                parameters.Add("@USUARIO", carteiraDTO.Usuario);
                parameters.Add("@CODIGO", carteiraDTO.Codigo);

                if (string.IsNullOrEmpty(setParams))
                    query = query.Replace("{PARAMS}", "");
                else
                    query = query.Replace("{PARAMS}", setParams);

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
