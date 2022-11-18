using Dapper;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;
using System;
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
    }
}
