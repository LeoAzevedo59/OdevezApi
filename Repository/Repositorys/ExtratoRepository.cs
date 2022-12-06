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
    public class ExtratoRepository : IExtratoRepository
    {
        private readonly IDbConnector _dbConnector;

        public ExtratoRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task<List<ExtratoDTO>> ObterExtratoResumido(int usuario)
        {
            try
            {
                var retorno = new List<ExtratoDTO>();

                string query = @$"  SELECT 
                                    TOP 2
                                    E.CODIGO,
                                    E.DATACRIACAO,
                                    E.VALOR,
                                    E.DESCRICAO,
                                    --CATEGORIA (CT)
                                    CT.CODIGO,
                                    CT.DESCRICAO,
                                    -- CARTEIRA (C)
                                    C.CODIGO,
                                    C.USUARIO,
                                    C.DESCRICAO,
                                    -- MOVIMENTACAO (M)
                                    M.CODIGO,
                                    M.DESCRICAO
                                    FROM EXTRATO E
                                    INNER JOIN CARTEIRA C ON C.CODIGO = E.CARTEIRA
                                    INNER JOIN MOVIMENTACAO M ON M.CODIGO = E.MOVIMENTACAO
                                    INNER JOIN CATEGORIA CT ON CT.CODIGO = E.CATEGORIA
                                    WHERE C.USUARIO = {usuario}
                                    ORDER BY E.CODIGO DESC";

                retorno = (await _dbConnector.dbConnection.QueryAsync<ExtratoDTO, CategoriaDTO, CarteiraDTO, MovimentacaoDTO, ExtratoDTO>(
                    sql: query,
                    splitOn: "Codigo",
                    map: (extrato, categoria, carteira, movimentacao) =>
                    {
                        extrato.Categoria = categoria;
                        extrato.Carteira = carteira;
                        extrato.Movimentacao = movimentacao;
                        return extrato;
                    },
                    transaction: _dbConnector.dbTransaction
                    )).ToList();
                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
