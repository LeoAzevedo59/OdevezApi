using System;

namespace Odevez.Repository.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
