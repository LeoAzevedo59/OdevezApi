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

        public async Task<bool> IncluirTransacaoCarteira(ExtratoDTO extrato)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"INSERT EXTRATO(DATULTALT, DATACRIACAO, VALOR, CARTEIRA, CATEGORIA, MOVIMENTACAO)
                                        VALUES (@DATULTALT, @DATACRIACAO, @VALOR, @CARTEIRA, @CATEGORIA, @MOVIMENTACAO)";

                parameters.Add("@DATULTALT", extrato.DatUltAlt);
                parameters.Add("@DATACRIACAO", extrato.DataMovimentacao);
                parameters.Add("@VALOR", extrato.Valor);
                parameters.Add("@CARTEIRA", extrato.Carteira.Codigo);
                parameters.Add("@CATEGORIA", extrato.Categoria.Codigo);
                parameters.Add("@MOVIMENTACAO", extrato.Movimentacao.Codigo);

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
    }
}
