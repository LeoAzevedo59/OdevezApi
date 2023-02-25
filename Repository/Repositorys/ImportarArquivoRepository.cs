using Dapper;
using Odevez.DTO;
using Odevez.Repository.DataConnector;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Utils.Enum;
using System;
using System.Threading.Tasks;

namespace Odevez.Repository.Repositorys
{
    public class ImportarArquivoRepository : IImportarArquivoRepository
    {
        private readonly IDbConnector _dbConnector;

        public ImportarArquivoRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<bool> IncluirOFX(ExtractDTO extract)
        {

            try
            {
                var parameters = new DynamicParameters();
                string query = @"INSERT EXTRATO(DATULTALT, DATACRIACAO, VALOR, CARTEIRA, CATEGORIA, MOVIMENTACAO, DESCRICAO, STATUS)
                                        VALUES (@DATULTALT, @DATACRIACAO, @VALOR, @CARTEIRA, @CATEGORIA, @MOVIMENTACAO, @DESCRICAO, @STATUS)";

                foreach (var credit in extract.TransactionsCredit)
                {
                    parameters.Add("@DATULTALT", DateTime.Now.Date);
                    parameters.Add("@CARTEIRA", extract.Carteira);
                    parameters.Add("@DATACRIACAO", credit.DataTransacao);
                    parameters.Add("@VALOR", credit.Valor);
                    parameters.Add("@STATUS", 1);
                    if (!string.IsNullOrEmpty(credit.Descricao) && credit.Descricao.ToLower().Contains("transf"))
                        parameters.Add("@CATEGORIA", (int)CategoriaEnum.TRANSFERENCIA);
                    else
                        parameters.Add("@CATEGORIA", (int)CategoriaEnum.BONUS);
                    parameters.Add("@MOVIMENTACAO", MovimentacaoEnum.Entrada);
                    parameters.Add("@DESCRICAO", credit.Descricao);

                    var retorno = await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);

                    if (retorno <= 0)
                        new Exception();

                    parameters = new DynamicParameters();
                }

                foreach (var debit in extract.TransactionsDebit)
                {
                    parameters.Add("@DATULTALT", DateTime.Now.Date);
                    parameters.Add("@CARTEIRA", extract.Carteira);
                    parameters.Add("@DATACRIACAO", debit.DataTransacao);
                    parameters.Add("@VALOR", debit.Valor);
                    parameters.Add("@STATUS", 1);
                    parameters.Add("@CATEGORIA", 8);
                    parameters.Add("@MOVIMENTACAO", MovimentacaoEnum.Saida);
                    parameters.Add("@DESCRICAO", debit.Descricao);

                    var retorno = await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);

                    if (retorno <= 0)
                        new Exception();

                    parameters = new DynamicParameters();
                }

                foreach (var other in extract.TransactionsOther)
                {
                    parameters.Add("@DATULTALT", DateTime.Now.Date);
                    parameters.Add("@CARTEIRA", extract.Carteira);
                    parameters.Add("@DATACRIACAO", other.DataTransacao);
                    parameters.Add("@VALOR", other.Valor * -1);
                    parameters.Add("@STATUS", 2);
                    parameters.Add("@CATEGORIA", (int)CategoriaEnum.OUTROS);
                    parameters.Add("@MOVIMENTACAO", MovimentacaoEnum.Saida);
                    parameters.Add("@DESCRICAO", other.Descricao);

                    var retorno = await _dbConnector.dbConnection.ExecuteAsync(query, param: parameters, transaction: _dbConnector.dbTransaction);

                    if (retorno <= 0)
                        new Exception();

                    parameters = new DynamicParameters();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
