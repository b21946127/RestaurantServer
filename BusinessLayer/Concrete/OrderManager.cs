using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using EntityLayer.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IMenuItemDal _menuItemDal;
        private readonly ICustomerDal _customerDal;
        private readonly IOrderItemDal _orderItemDal;
        private readonly IMapper _mapper;

        public OrderManager(IOrderDal orderDal, IMenuItemDal menuItemDal, ICustomerDal customerDal, IOrderItemDal orderItemDal, IMapper mapper)
        {
            _orderDal = orderDal;
            _menuItemDal = menuItemDal;
            _customerDal = customerDal;
            _orderItemDal = orderItemDal;
            _mapper = mapper;
        }

        public async Task<OrderDto> AddOrder(CreateOrderDto createOrder)
        {
            var customer = await _customerDal.GetAsync(x => x.Id == createOrder.CustomerId);
            if (customer == null)
            {
                throw new Exception("User not found.");
            }
             
            Order order = new Order
            {
                Customer = customer,
                OrderTime = DateTime.UtcNow,
                Items = new List<OrderItem>()

            };

            await _orderDal.InsertAsync(order);

            foreach (var item in createOrder.Items)
            {
                var menuItem = await _menuItemDal.GetAsync(x => x.Id == item.MenuItemId);
                if (menuItem != null)
                {
                    var orderItem = new OrderItem
                    {
                        MenuItem = menuItem,
                        Quantity = item.Quantity,
                        Order = order
                    };

                    order.Items.Add(orderItem);
                    await _orderItemDal.InsertAsync(orderItem);
                }
            }

            await _orderDal.UpdateAsync(order);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task RemoveOrder(int orderId)
        {
            var order = await _orderDal.GetAsync(o => o.Id == orderId);
            if (order != null)
            {
                await _orderDal.DeleteAsync(order);
            }
            else
            {
                throw new Exception("Order not found."); 
            }
        }

        public async Task<List<OrderDto>> GetOrders()
        {
            var orders = await _orderDal.ListAsync();
            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return orderDtos;
        }

        public async Task<List<OrderDto>> GetOrdersByUser(int userId)
        {
            var user = await _customerDal.GetAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            List<Order> orders = await _orderDal.ListAsync(o => o.Customer.Id == userId);
            if (orders.Count == 0)
            {
                throw new Exception("Order not found.");
            }

            var orderDtos = _mapper.Map<List<OrderDto>>(orders);
            return orderDtos;
        }

    }
}
