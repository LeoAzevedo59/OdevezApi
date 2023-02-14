using Odevez.Utils.Enum;

namespace Odevez.DTO
{
    public class DashPizzaDTO
    {
        public int Codigo { get; set; }
        public string Categoria { get; set; }
        public decimal Valor { get; set; }
        public string Cor { get; set; }
        public MovimentacaoEnum Movimentacao { get; set; }
    }
}
