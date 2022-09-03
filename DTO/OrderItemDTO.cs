namespace Odevez.DTO
{
    public class OrderItemDTO
    {
        public OrderDTO Order { get; set; }
        public ProductDTO Product { get; set; }
        public decimal SellValue { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmout { get; set; }
    }
}
