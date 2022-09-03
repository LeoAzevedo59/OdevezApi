using Dapper;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System;
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

        public async Task<List<ClientModel>> ListByFilterAsync(int? clientId = 0, string name = null)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = "SELECT * FROM CLIENT WHERE 1=1";

                if (!string.IsNullOrEmpty(name))
                {
                    parameters.Add("@NOME", name);
                    query += $" AND NAME LIKE '%{name}%'";
                }

                if (clientId > 0)
                {
                    parameters.Add("@ID", clientId);
                    query += " AND ID = @ID";
                }

                var client = await _dbConnector.dbConnection.QueryAsync<ClientModel>(query, param: parameters, transaction: _dbConnector.dbTransaction);
                return client.ToList();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }
    }
}
