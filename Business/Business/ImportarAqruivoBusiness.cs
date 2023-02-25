using Odevez.Business.Business.Interfaces;
using Odevez.DTO;
using Odevez.Repository.Repositorys.Interfaces;
using Odevez.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Odevez.Business.Business
{
    public class ImportarAqruivoBusiness : IImportarArquivoBusiness
    {
        private readonly IImportarArquivoRepository _importarArquivoRepository;
        public ImportarAqruivoBusiness(IImportarArquivoRepository importarArquivoRepository)
        {
            _importarArquivoRepository = importarArquivoRepository;
        }

        public async Task<bool> IncluirOFX(ImportFileDTO file)
        {
            try
            {
                var extract = new ExtractDTO();
                var OFXs = new List<string>();
                string[] start = new string[2];

                extract.TransactionsCredit = new List<TransactionDTO>();
                extract.TransactionsDebit = new List<TransactionDTO>();
                extract.TransactionsOther = new List<TransactionDTO>();

                var rows = file.Content.Split("\n");

                foreach (var row in rows)
                {
                    string r = row.Replace("\n", "");
                    r = r.Replace("\r", "");
                    OFXs.Add(r);
                }

                foreach (var OFX in OFXs)
                {
                    #region Descrição da organização

                    if (OFX.Contains("<ORG>"))
                    {
                        start = OFX.Split("<ORG>");
                        extract.OrganizacaoDesc = start[1].Split("</ORG>")[0].ToString();
                    }

                    #endregion

                    #region Tipo Moeda

                    if (OFX.Contains("<CURDEF>"))
                    {
                        start = OFX.Split("<CURDEF>");
                        extract.TipoMoeda = start[1].Split("</CURDEF>")[0].ToString();
                    }

                    #endregion

                    #region Código do banco

                    if (OFX.Contains("<BANKID>"))
                    {
                        start = OFX.Split("<BANKID>");
                        extract.IdBanco = Convert.ToInt64(start[1].Split("</BANKID>")[0]);
                        break;
                    }

                    #endregion
                }

                for (int i = 0; i < OFXs.Count(); i++)
                {
                    if (OFXs[i].Equals("<STMTTRN>"))
                    {
                        var transaction = new TransactionDTO();

                        #region Tipo Transação

                        if (OFXs[i + 1].Contains("<TRNTYPE>"))
                        {
                            start = OFXs[i + 1].Split("<TRNTYPE>");
                            transaction.Tipo = start[1].Split("</TRNTYPE>")[0].ToString();
                        }

                        #endregion

                        #region Data Transação

                        if (OFXs[i + 2].Contains("<DTPOSTED>"))
                        {
                            start = OFXs[i + 2].Split("<DTPOSTED>");
                            string data = start[1].Split("</DTPOSTED>")[0].ToString();
                            transaction.DataTransacao = DataUtils.RetornaDataStringToDate(data.Substring(0, 8));
                        }

                        #endregion

                        #region Valor transação

                        if (OFXs[i + 3].Contains("<TRNAMT>"))
                        {
                            start = OFXs[i + 3].Split("<TRNAMT>");
                            string valorString = start[1].Split("</TRNAMT>")[0];

                            decimal valor = Convert.ToDecimal(valorString, CultureInfo.InvariantCulture);

                            transaction.Valor = valor;
                        }

                        #endregion

                        #region Descrição da Transação

                        if (OFXs[i + 5].Contains("<MEMO>"))
                        {
                            start = OFXs[i + 5].Split("<MEMO>");
                            transaction.Descricao = start[1].Split("</MEMO>")[0].ToString();
                        }
                        else if (OFXs[i + 7].Contains("<MEMO>"))
                        {
                            start = OFXs[i + 7].Split("<MEMO>");
                            transaction.Descricao = start[1].Split("</MEMO>")[0].ToString();
                        }

                        #endregion

                        if (transaction.Tipo == "CREDIT")
                            extract.TransactionsCredit.Add(transaction);

                        if (transaction.Tipo == "DEBIT")
                            extract.TransactionsDebit.Add(transaction);

                        if (transaction.Tipo != "DEBIT" && transaction.Tipo != "CREDIT")
                            extract.TransactionsOther.Add(transaction);
                    }
                }

                decimal valorTransacaoCredito = extract.TransactionsCredit.Sum(x => x.Valor);
                decimal valorTransacaoDebito = extract.TransactionsDebit.Sum(x => x.Valor);
                decimal valorTransacaoOutros = extract.TransactionsOther.Sum(x => x.Valor);

                extract.Carteira = file.Carteira;

                return await _importarArquivoRepository.IncluirOFX(extract);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
