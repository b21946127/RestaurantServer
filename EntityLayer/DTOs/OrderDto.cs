using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderTime { get; set; }

        public string Username { get; set; }
        public List<OrderItemDto> Items { get; set; }


    }
}
