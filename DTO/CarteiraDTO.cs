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
        public int? FechamentoFatura { get; set; }
        public int? VencimentoFatura { get; set; }
        public bool ChkExibirHome { get; set; }
        public bool ChkNaoSomarPatrimonio { get; set; }
        public decimal Valor { get; set; }
        public int Banco { get; set; }
        public BancoDTO BancoDTO { get; set; }
    }
}
