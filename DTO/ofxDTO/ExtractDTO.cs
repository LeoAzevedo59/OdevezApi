using System.Collections.Generic;

namespace Odevez.DTO
{
    public class ExtractDTO
    {
        public int Usuario { get; set; }
        public int Carteira { get; set; }
        public string OrganizacaoDesc { get; set; }
        public long IdBanco { get; set; }
        public string TipoMoeda { get; set; }
        public List<TransactionDTO> TransactionsCredit { get; set; }
        public List<TransactionDTO> TransactionsDebit { get; set; }
        public List<TransactionDTO> TransactionsOther { get; set; }

    }
}
