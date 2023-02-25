using System;

namespace Odevez.DTO
{
    public class TransactionDTO
    {
        public string Tipo { get; set; }
        public DateTime DataTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
}
