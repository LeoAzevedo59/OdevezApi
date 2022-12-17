using Dapper;
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

        public async Task<decimal> ObterValorCarteiraPorUsuario(int usuario)
        {
            try
            {
                string query = $"SELECT COALESCE(SUM(VALOR),0) FROM CARTEIRA WHERE USUARIO = '{usuario}'";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                                    C.USUARIO IS NULL OR C.USUARIO = {usuario}";

                retorno = (await _dbConnector.dbConnection.QueryAsync<CategoriaDTO>(query, transaction: _dbConnector.dbTransaction)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return retorno;
        }

        public async Task<bool> AlterarValorCarteira(int codigo, decimal valorCarteira)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"   UPDATE CARTEIRA
	                                    SET VALOR = @VALOR, DATULTALT = @DATULTALT
                                    WHERE CODIGO = @CODIGO";

                parameters.Add("@DATULTALT", DateTime.Now);
                parameters.Add("@CODIGO", codigo);
                parameters.Add("@VALOR", valorCarteira);


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

        public async Task<decimal> ObterValorCarteira(int codigo)
        {
            try
            {
                string query = $"SELECT COALESCE(SUM(VALOR),0) FROM CARTEIRA WHERE CODIGO = '{codigo}'";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<bool> IncluirTransacaoCarteira(TipoCarteiraDTO tipoCarteira)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"   INSERT CARTEIRA(DATULTALT, DATACRIACAO, USUARIO, TIPOCARTEIRA, DESCRICAO, FECHAMENTOFATURA, VENCIMENTOFATURA, VALOR)
                                    VALUES(@DATULTALT, @DATACRIACAO, @USUARIO, @TIPOCARTEIRA, @DESCRICAO, @FECHAMENTOFATURA, @VENCIMENTOFATURA, @VALOR)";

                parameters.Add("@DATULTALT", DateTime.Now.Date);
                parameters.Add("@DATACRIACAO", DateTime.Now.Date);
                parameters.Add("@USUARIO", tipoCarteira.Usuario);
                parameters.Add("@TIPOCARTEIRA", tipoCarteira.TipoCarteira);
                parameters.Add("@DESCRICAO", tipoCarteira.Descricao);
                parameters.Add("@FECHAMENTOFATURA", tipoCarteira.FechamentoFatura);
                parameters.Add("@VENCIMENTOFATURA", tipoCarteira.VencimentoFatura);
                parameters.Add("@VALOR", decimal.Zero);


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
                string query = @$"  SELECT * FROM CARTEIRA
                                    WHERE USUARIO = {usuario}";

                if (tipoCarteira != 12)
                    query += $" AND TIPOCARTEIRA = { (int)tipoCarteira }";

                retorno = (await _dbConnector.dbConnection.QueryAsync<CarteiraDTO>(query, transaction: _dbConnector.dbTransaction)).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> ObterValorCarteiraPorTipoCarteira(int usuario, int tipoCarteira)
        {
            try
            {
                string query = $"SELECT COALESCE(SUM(VALOR),0) FROM CARTEIRA WHERE USUARIO = '{usuario}'";

                if (tipoCarteira != 12)
                    query += $" AND TIPOCARTEIRA = { (int)tipoCarteira }";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<decimal>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
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
    }
}
