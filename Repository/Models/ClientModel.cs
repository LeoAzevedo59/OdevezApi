namespace Odevez.Repository.Models
{
    public class ClientModel : EntityBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Adress { get; set; }
    }
}
