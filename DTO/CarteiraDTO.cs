using Odevez.Utils.Enum;
using System;

namespace Odevez.DTO
{
    public class CarteiraDTO
    {
        public int Codigo { get; set; }
        public DateTime DatUltAlt { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Usuario { get; set; }
        public TipoCarteiraEnum TipoCarteira { get; set; }
        public string Descricao { get; set; }
        public DateTime? FechamentoFatura { get; set; }
        public DateTime? VencimentoFatura { get; set; }
    }
}
