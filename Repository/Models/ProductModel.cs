namespace Odevez.Repository.Models
{
    public class ProductModel : EntityBase
    {
        public string Description { get; set; }
        public decimal SellValue { get; set; }
        public int Stock { get; set; }
    }
}
