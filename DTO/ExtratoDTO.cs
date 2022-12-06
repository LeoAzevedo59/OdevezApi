using System;

namespace Odevez.DTO
{
    public class ExtratoDTO
    {
        public int Codigo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DatUltAlt { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public CategoriaDTO Categoria { get; set; }
        public MovimentacaoDTO Movimentacao { get; set; }
        public CarteiraDTO Carteira { get; set; }
    }
}
