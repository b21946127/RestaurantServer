using EntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService
    {
        Task<OrderDto> AddOrder(CreateOrder createOrder);
        Task RemoveOrder(int orderId);
        Task<List<OrderDto>> GetOrdersByUser(int userId);
        Task<List<OrderDto>> GetOrders();
        Task<OrderDto> UpdateOrderById(UpdateOrderDto updateOrderDto);
    }
}
