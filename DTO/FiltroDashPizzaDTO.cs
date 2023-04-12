using Odevez.Utils.Enum;
using System;

namespace Odevez.DTO
{
    public class FiltroDashPizzaDTO
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public MovimentacaoEnum Movimentacao { get; set; }
        public int Usuario { get; set; }
        public string DataInicioStrg { get; set; }
        public string DataFimStrg { get; set; }
    }
}
