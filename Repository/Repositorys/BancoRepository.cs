using Dapper;
using Odevez.DTO;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys
{
    public class BancoRepository : IBancoRepository
    {
        private readonly IDbConnector _dbConnector;

        public BancoRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<int> Incluir(BancoDTO banco)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"   INSERT BANCO(DATULTALT, DATACRIACAO, NAME, CODE, ISPB, USUARIO)
                                          VALUES(@DATULTALT, @DATACRIACAO, @NAME, @CODE, @ISPB, @USUARIO)";

                parameters.Add("@DATULTALT", DateTime.Now.Date, DbType.DateTime);
                parameters.Add("@DATACRIACAO", DateTime.Now.Date, DbType.DateTime);
                parameters.Add("@USUARIO", banco.Usuario, DbType.Int32);
                parameters.Add("@NAME", banco.name, DbType.String);
                parameters.Add("@ISPB", Convert.ToInt64(banco.ispb), DbType.Int64);
                parameters.Add("@CODE", banco.code, DbType.Int32);

                var retorno = await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ObterPorIspb(string ispb)
        {
            try
            {
                string query = @$"  SELECT CODIGO FROM BANCO
                                    WHERE ISPB = '{ispb}'";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<int>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
