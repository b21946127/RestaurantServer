using EntityLayer.DTOs.CustomerDtos;
using EntityLayer.DTOs.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderTime { get; set; }
        public CustomerDto Customer { get; set; }
        public List<OrderItemDto> Items { get; set; }

    }
}
