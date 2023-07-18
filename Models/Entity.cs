using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyFlow.Models
{
    public class Entity
    {
        [Key]
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}