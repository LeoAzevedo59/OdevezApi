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

        public async Task<UsuarioDTO> ObterPasswordHash(string celular)
        {
            var parameters = new DynamicParameters();
            string query = "SELECT SENHAHASH, ID FROM USUARIO WHERE 1=1";

            parameters.Add("@CELULAR", celular);
            query += " AND CELULAR = @CELULAR";

            var passwordHash = (await _dbConnector.dbConnection.QueryAsync<UsuarioDTO>(query, param: parameters, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
            return passwordHash;
        }
    }
}
