using Dapper;
using Odevez.DTO;
using Odevez.Repository.DataConnector;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys.Interfaces
{
    public class AutenticarRepository : IAutenticarRepository
    {
        private readonly IDbConnector _dbConnector;

        public AutenticarRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<ClientDTO> ObterPasswordHash(long phoneNumber)
        {
            var parameters = new DynamicParameters();
            string query = "SELECT PASSWORDHASH, ID FROM CLIENT WHERE 1=1";

            parameters.Add("@PHONE", phoneNumber);
            query += " AND PHONENUMBER = @PHONE";

            var passwordHash = (await _dbConnector.dbConnection.QueryAsync<ClientDTO>(query, param: parameters, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
            return passwordHash;
        }
    }
}
