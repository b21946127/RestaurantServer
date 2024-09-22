using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.OrderItemDtos
{
    public class CreateOrderItemDto
    {

        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
