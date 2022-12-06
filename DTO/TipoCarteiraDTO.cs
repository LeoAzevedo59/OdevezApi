using System;

namespace Odevez.DTO
{
    public class TipoCarteiraDTO
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int Usuario { get; set; }
        public int TipoCarteira { get; set; }
        public decimal Valor { get; set; }
        public DateTime DatUltAlt { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? FechamentoFatura { get; set; }
        public DateTime? VencimentoFatura { get; set; }
    }
}
