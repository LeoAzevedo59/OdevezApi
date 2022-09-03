using System;

namespace Odevez.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
        public string Adress { get; set; }
    }
}
