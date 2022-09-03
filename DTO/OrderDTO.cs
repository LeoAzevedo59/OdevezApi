using System.Collections.Generic;

namespace Odevez.DTO
{
    public class OrderDTO
    {
        public ClientDTO Cliente { get; set; }
        public UserDTO User { get; set; }
        public List<OrderItemDTO> Items { get; set; }
    }
}
