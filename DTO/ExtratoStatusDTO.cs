using Odevez.Utils.Enum;

namespace Odevez.DTO
{
    public class ExtratoStatusDTO
    {
        public int Codigo { get; set; }
        public int Carteira { get; set; }
        public decimal Valor { get; set; }
        public ExtratoStatusEnum StatusOld { get; set; }
    }
}
