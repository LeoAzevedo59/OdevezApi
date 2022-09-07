using Dapper;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Models;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Collections.Generic;
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> InserirClient(ClientModel clientModel)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"INSERT CLIENT (CREATEDAT, NAME, EMAIL, PHONENUMBER, ADRESS, PASSWORDHASH)
                                        VALUES (@DATE, @NAME, @EMAIL, @PHONE, @ADRESS, @PASSWORD)";

                parameters.Add("@DATE", DateTime.Now.Date);
                parameters.Add("@NAME", clientModel.Name);
                parameters.Add("@EMAIL", clientModel.Email);
                parameters.Add("@PHONE", clientModel.PhoneNumber);
                parameters.Add("@ADRESS", clientModel.Adress);
                parameters.Add("@PASSWORD", clientModel.PasswordHash);

                var client = await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);
                if (client > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerifyPhoneNumber(long phoneNumber)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = "SELECT NAME FROM CLIENT WHERE 1=1";

                parameters.Add("@PHONE", phoneNumber);
                query += " AND PHONENUMBER = @PHONE";

                var nameUser = (await _dbConnector.dbConnection.QueryAsync<ClientModel>(query, param: parameters, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                if (nameUser != null)
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
