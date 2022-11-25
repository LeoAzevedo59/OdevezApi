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
    }
}
