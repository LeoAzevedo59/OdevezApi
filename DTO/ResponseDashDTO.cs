using System.Collections.Generic;

namespace Odevez.DTO
{
    public class ResponseDashDTO
    {
        public decimal ValorTotal { get; set; }
        public List<DashPizzaDTO> Dados { get; set; }
    }
}
