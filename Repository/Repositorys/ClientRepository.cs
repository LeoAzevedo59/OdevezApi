using Dapper;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnector _dbConnector;
        public ClientRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public Task CreatAsync(ClientModel client)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(int clientId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientModel> GetByIdAsync(int clientId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<ClientModel>> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            string query = "SELECT TOP 10 * FROM CLIENT WHERE 1=1";

            if (!string.IsNullOrEmpty(name))
                query += "AND NAME LIKE '% @NAME %'";

            if (clientId >= 0)
                query += "AND CLIENTID = @CLIENTID";

            var client = await _dbConnector.dbConnection.QueryAsync<ClientModel>(query, new { Id = clientId, Name = name}, transaction: _dbConnector.dbTransaction);
            return client.ToList();
        }

        public Task UpdateAsync(ClientModel client)
        {
            throw new System.NotImplementedException();
        }
    }
}
