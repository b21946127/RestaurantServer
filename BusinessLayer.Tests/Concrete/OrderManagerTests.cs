using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using EntityLayer.DTOs.OrderDtos;
using BusinessLayer.Concrete;
using EntityLayer.DTOs.OrderItemDtos;
using System.Linq.Expressions;
using EntityLayer.DTOs.CustomerDtos;

namespace BusinessLayer.Tests.Concrete
{
    [TestClass]
    public class OrderManagerTests
    {
        private Mock<IOrderDal> _orderDalMock;
        private Mock<IMenuItemDal> _menuItemDalMock;
        private Mock<ICustomerDal> _customerDalMock;
        private Mock<IOrderItemDal> _orderItemDalMock;
        private Mock<IMapper> _mapperMock;
        private OrderManager _orderManager;

        [TestInitialize]
        public void SetUp()
        {
            _orderDalMock = new Mock<IOrderDal>();
            _menuItemDalMock = new Mock<IMenuItemDal>();
            _customerDalMock = new Mock<ICustomerDal>();
            _orderItemDalMock = new Mock<IOrderItemDal>();
            _mapperMock = new Mock<IMapper>();

            _orderManager = new OrderManager(
                _orderDalMock.Object,
                _menuItemDalMock.Object,
                _customerDalMock.Object,
                _orderItemDalMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task AddOrder_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var createOrder = new CreateOrderDto { CustomerId = 1 };

            _customerDalMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Customer)null);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _orderManager.AddOrder(createOrder));
            Assert.AreEqual("User not found.", ex.Message);
        }

        [TestMethod]
        public async Task AddOrder_ShouldReturnOrderDto_WhenOrderIsSuccessfullyAdded()
        {
            // Arrange
            var createOrder = new CreateOrderDto
            {
                CustomerId = 1,
                Items = new List<CreateOrderItemDto>
        {
            new CreateOrderItemDto { MenuItemId = 1, Quantity = 2 }
        }
            };

            var customer = new Customer { Id = 1 };
            var menuItem = new MenuItem { Id = 1, Name = "Pizza" };
            var order = new Order { Id = 1, Customer = customer, OrderTime = DateTime.UtcNow };

            _customerDalMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync(customer);
            _menuItemDalMock.Setup(m => m.GetAsync(It.IsAny<Expression<Func<MenuItem, bool>>>()))
                .ReturnsAsync(menuItem);
            _orderDalMock.Setup(o => o.InsertAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _orderItemDalMock.Setup(o => o.InsertAsync(It.IsAny<OrderItem>())).Returns(Task.CompletedTask);
            _orderDalMock.Setup(o => o.UpdateAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _mapperMock.Setup(m => m.Map<OrderDto>(It.IsAny<Order>())).Returns(new OrderDto { Id = 1, Customer = new CustomerDto { Id = 1 } });

            // Act
            var result = await _orderManager.AddOrder(createOrder);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Customer.Id);
        }

        [TestMethod]
        public async Task RemoveOrder_ShouldThrowException_WhenOrderNotFound()
        {
            // Arrange
            var orderId = 1;

            _orderDalMock.Setup(o => o.GetAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync((Order)null);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _orderManager.RemoveOrder(orderId));
            Assert.AreEqual("Order not found.", ex.Message);
        }

        [TestMethod]
        public async Task RemoveOrder_ShouldSuccessfullyRemoveOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = 1;
            var order = new Order { Id = orderId };

            _orderDalMock.Setup(o => o.GetAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(order);
            _orderDalMock.Setup(o => o.DeleteAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            // Act
            await _orderManager.RemoveOrder(orderId);

            // Assert
            _orderDalMock.Verify(o => o.DeleteAsync(order), Times.Once);
        }

        [TestMethod]
        public async Task GetOrders_ShouldReturnListOfOrderDtos()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, OrderTime = DateTime.UtcNow, Customer = new Customer() },
                new Order { Id = 2, OrderTime = DateTime.UtcNow, Customer = new Customer() }
            };
            var orderDtos = new List<OrderDto>
            {
                new OrderDto { Id = 1 },
                new OrderDto { Id = 2 }
            };

            _orderDalMock.Setup(o => o.ListAsync()).ReturnsAsync(orders);
            _mapperMock.Setup(m => m.Map<List<OrderDto>>(It.IsAny<List<Order>>())).Returns(orderDtos);

            // Act
            var result = await _orderManager.GetOrders();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public async Task GetOrdersByUser_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = 1;
            _customerDalMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync((Customer)null);

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _orderManager.GetOrdersByUser(userId));
            Assert.AreEqual("User not found.", ex.Message);
        }

        [TestMethod]
        public async Task GetOrdersByUser_ShouldThrowException_WhenNoOrdersFound()
        {
            // Arrange
            var userId = 1;
            var customer = new Customer { Id = userId };

            _customerDalMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync(customer);
            _orderDalMock.Setup(o => o.ListAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(new List<Order>());

            // Act & Assert
            var ex = await Assert.ThrowsExceptionAsync<Exception>(() => _orderManager.GetOrdersByUser(userId));
            Assert.AreEqual("Order not found.", ex.Message);
        }

        [TestMethod]
        public async Task GetOrdersByUser_ShouldReturnListOfOrderDtos_WhenOrdersExist()
        {
            // Arrange
            var userId = 1;
            var customer = new Customer { Id = userId };
            var orders = new List<Order>
            {
                new Order { Id = 1, Customer = customer },
                new Order { Id = 2, Customer = customer }
            };
            var orderDtos = new List<OrderDto>
            {
                new OrderDto { Id = 1 },
                new OrderDto { Id = 2 }
            };

            _customerDalMock.Setup(c => c.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                .ReturnsAsync(customer);
            _orderDalMock.Setup(o => o.ListAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                .ReturnsAsync(orders);
            _mapperMock.Setup(m => m.Map<List<OrderDto>>(It.IsAny<List<Order>>())).Returns(orderDtos);

            // Act
            var result = await _orderManager.GetOrdersByUser(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}
