using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Order
    {
        [Key]
        public int Id { get;  set; }

        public Customer Customer { get; set; }

        [Required]
        public DateTime OrderTime { get; set; } 

        public ICollection<OrderItem> Items { get; set; }

    }
}
