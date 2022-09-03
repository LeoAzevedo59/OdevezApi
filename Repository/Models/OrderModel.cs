using System.Collections.Generic;

namespace Odevez.Repository.Models
{
    public class OrderModel : EntityBase
    {
        public ClientModel Cliente { get; set; }
        public UserModel User { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
}
