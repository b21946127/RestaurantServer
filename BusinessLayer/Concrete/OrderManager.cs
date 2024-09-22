using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using RestaurantServer.DTOs;
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
        private readonly IUserDal _userDal;
        private readonly IOrderItemDal _orderItemDal;

        public OrderManager(IOrderDal orderDal, IMenuItemDal menuItemDal, IUserDal userDal, IOrderItemDal orderItemDal)
        {
            _orderDal = orderDal;
            _menuItemDal = menuItemDal;
            _userDal = userDal;
            _orderItemDal = orderItemDal;
        }

        public async Task<OrderDto> AddOrder(CreateOrder createOrder)
        {
            var user = await _userDal.GetAsync(x => x.Id == createOrder.UserId);
            if (user == null)
            {
                throw new Exception("User not found."); 
            }

            var order = new Order
            {
                OrderTime = DateTime.Now,
                User = user,
                Items = new List<OrderItem>()
            };

            foreach (var item in createOrder.Items)
            {
                var menuItem = await _menuItemDal.GetAsync(x => x.Id == item.MenuItemId);
                if (menuItem != null)
                {
                    var orderItem = new OrderItem
                    {
                        MenuItem = menuItem,
                        Quantity = item.Quantity
                    };

                    order.Items.Add(orderItem);
                    await _orderItemDal.InsertAsync(orderItem);
                }
            }

            await _orderDal.InsertAsync(order); 

            var orderDto = new OrderDto
            {
                OrderTime = order.OrderTime.ToString(),
                Username = user.Name,
                Items = order.Items.Select(item => new OrderItemDto
                {
                    MenuItem = new MenuItemDto
                    {
                        Id = item.MenuItem.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Portion = item.MenuItem.Portion
                    },
                    Quantity = item.Quantity
                }).ToList()
            };

            return orderDto;
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
            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new OrderDto
                {
                    Id = order.Id,
                    OrderTime = order.OrderTime.ToString(),
                    Username = order.User.Name,
                    Items = order.Items.Select(item => new OrderItemDto
                    {
                        MenuItem = new MenuItemDto
                        {
                            Id = item.MenuItem.Id,
                            Name = item.MenuItem.Name,
                            Price = item.MenuItem.Price,
                            Portion = item.MenuItem.Portion
                        },
                        Quantity = item.Quantity
                    }).ToList()
                };

                orderDtos.Add(orderDto);
            }

            return orderDtos;
        }

        public async Task<List<OrderDto>> GetOrdersByUser(int userId)
        {
            var user = await _userDal.GetAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            List<Order> orders = await _orderDal.ListAsync(o => o.User.Id == userId);
            if (orders.Count == 0)
            {
                throw new Exception("Order not found.");
            }

            var orderDtos = new List<OrderDto>();
            foreach (var order in orders) {
                var orderDto = new OrderDto
                {
                    Id = order.Id,
                    OrderTime = order.OrderTime.ToString(),
                    Username = order.User.Name,
                    Items = order.Items.Select(item => new OrderItemDto
                    {
                        MenuItem = new MenuItemDto
                        {
                            Id = item.MenuItem.Id,
                            Name = item.MenuItem.Name,
                            Price = item.MenuItem.Price,
                            Portion = item.MenuItem.Portion
                        },
                        Quantity = item.Quantity
                    }).ToList()
                };

                orderDtos.Add(orderDto);
            }
            return orderDtos;
        }

        public async Task<OrderDto> UpdateOrderById(UpdateOrderDto updateOrderDto)
        {
            var order = await _orderDal.GetAsync(o => o.Id == updateOrderDto.Id);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            var user = await _userDal.GetAsync(u => u.Id == updateOrderDto.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            order.User = user;
            order.Items = new List<OrderItem>();

            foreach (var item in updateOrderDto.Items)
            {
                var menuItem = await _menuItemDal.GetAsync(x => x.Id == item.MenuItem.Id);
                if (menuItem != null)
                {
                    var orderItem = new OrderItem
                    {
                        MenuItem = menuItem,
                        Quantity = item.Quantity
                    };

                    order.Items.Add(orderItem);
                    await _orderItemDal.InsertAsync(orderItem);
                }
            }

            await _orderDal.UpdateAsync(order);

            var orderDto = new OrderDto
            {
                OrderTime = order.OrderTime.ToString(),
                Username = user.Name,
                Items = order.Items.Select(item => new OrderItemDto
                {
                    MenuItem = new MenuItemDto
                    {
                        Id = item.MenuItem.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Portion = item.MenuItem.Portion
                    },
                    Quantity = item.Quantity
                }).ToList()
            };

            return orderDto;

        }
    }
}
