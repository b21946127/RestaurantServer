using EntityLayer.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(CreateOrderDto createOrder);
        Task RemoveOrder(int orderId);
        Task<List<OrderDto>> GetOrdersByUser(int userId);
        Task<List<OrderDto>> GetOrders();
    }
}
