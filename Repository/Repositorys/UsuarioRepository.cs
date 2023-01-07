using Dapper;
using Odevez.DTO;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnector _dbConnector;

        public UsuarioRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<bool> InserirUsuario(UsuarioDTO usuario)
        {
            try
            {
                var parameters = new DynamicParameters();
                string query = @"INSERT USUARIO (ID, DATULTALT, NOME, SOBRENOME, APELIDO, EMAIL, SENHAHASH, CELULAR, CPF)
		                                 VALUES (@ID, @DATULTALT, @NOME, @SOBRENOME, @APELIDO, @EMAIL, @SENHAHASH, @CELULAR, @CPF)";

                parameters.Add("@ID", usuario.Id);
                parameters.Add("@DATULTALT", usuario.DatUltAlt);
                parameters.Add("@NOME", usuario.Nome);
                parameters.Add("@SOBRENOME", usuario.Sobrenome);
                parameters.Add("@APELIDO", usuario.Apelido);
                parameters.Add("@EMAIL", usuario.Email);
                parameters.Add("@SENHAHASH", usuario.SenhaHash);
                parameters.Add("@CELULAR", usuario.Celular);
                parameters.Add("@CPF", usuario.Cpf);

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

        public async Task<UsuarioDTO> ObterUsuarioPorCelular(string celular)
        {
            try
            {
                string query = $"SELECT * FROM USUARIO WHERE CELULAR = '{celular}'";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<UsuarioDTO>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ObterUsuarioPorId(long id)
        {
            try
            {
                string query = $"SELECT ID FROM USUARIO WHERE ID = {id}";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<long>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                if (retorno > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> VerificarCPF(string email)
        {
            try
            {
                string query = $"SELECT CODIGO FROM USUARIO WHERE EMAIL = '{email}'";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<long>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                if (retorno > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        public async Task<string> InserirApelido(UsuarioDTO usuario)
        {
            try
            {
                string query = $@"UPDATE USUARIO
	                                SET APELIDO = '{usuario.Apelido}'
                                 WHERE CODIGO = {usuario.Codigo}";

                var retorno = await _dbConnector.dbConnection.ExecuteAsync(query, transaction: _dbConnector.dbTransaction);
                if (retorno > 0)
                    return usuario.Apelido;

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ObterNomePorCodigo(int usuario)
        {
            try
            {
                string query = @$"  SELECT NOME FROM USUARIO
                                    WHERE CODIGO = {usuario}";

                var retorno = (await _dbConnector.dbConnection.QueryAsync<string>(query, transaction: _dbConnector.dbTransaction)).FirstOrDefault();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
