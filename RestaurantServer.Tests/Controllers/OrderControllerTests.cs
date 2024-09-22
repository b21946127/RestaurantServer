using Microsoft.AspNetCore.Mvc;
using Moq;
using BusinessLayer.Abstract;
using EntityLayer.DTOs.OrderDtos;
using RestaurantServer.Controllers;
using EntityLayer.DTOs.CustomerDtos;

namespace RestaurantServer.Tests.Controllers
{
    [TestClass]
    public class OrderControllerTests
    {
        private Mock<IOrderService> _orderServiceMock;
        private OrderController _orderController;

        [TestInitialize]
        public void SetUp()
        {
            _orderServiceMock = new Mock<IOrderService>();
            _orderController = new OrderController(_orderServiceMock.Object);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnOkResult_WhenOrderIsCreated()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto { CustomerId = 1 };
            _orderServiceMock.Setup(o => o.AddOrder(createOrderDto))
                .ReturnsAsync(new OrderDto { Id = 1, Customer = new CustomerDto { Id = 1 } });

            // Act
            var result = await _orderController.CreateOrder(createOrderDto) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(OrderDto));
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _orderController.ModelState.AddModelError("Error", "Model state is invalid.");

            // Act
            var result = await _orderController.CreateOrder(new CreateOrderDto()) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public async Task CreateOrder_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var createOrderDto = new CreateOrderDto { CustomerId = 1 };
            _orderServiceMock.Setup(o => o.AddOrder(createOrderDto))
                .ThrowsAsync(new Exception("Error creating order"));

            // Act
            var result = await _orderController.CreateOrder(createOrderDto) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Error creating order", result.Value);
        }

        [TestMethod]
        public async Task RemoveOrder_ShouldReturnOkResult_WhenOrderIsRemoved()
        {
            // Arrange
            var orderId = 1;
            _orderServiceMock.Setup(o => o.RemoveOrder(orderId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _orderController.RemoveOrder(orderId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual($"Order with ID {orderId} removed successfully.", result.Value);
        }

        [TestMethod]
        public async Task RemoveOrder_ShouldReturnNotFound_WhenOrderNotFound()
        {
            // Arrange
            var orderId = 1;
            _orderServiceMock.Setup(o => o.RemoveOrder(orderId))
                .ThrowsAsync(new Exception("Order not found"));

            // Act
            var result = await _orderController.RemoveOrder(orderId) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Order not found", result.Value);
        }

        [TestMethod]
        public async Task GetOrders_ShouldReturnOkResult_WhenOrdersAreRetrieved()
        {
            // Arrange
            var orders = new List<OrderDto>
            {
                new OrderDto { Id = 1 },
                new OrderDto { Id = 2 }
            };
            _orderServiceMock.Setup(o => o.GetOrders())
                .ReturnsAsync(orders);

            // Act
            var result = await _orderController.GetOrders() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, ((List<OrderDto>)result.Value).Count);
        }

        [TestMethod]
        public async Task GetOrdersByUser_ShouldReturnOkResult_WhenOrdersAreFound()
        {
            // Arrange
            var userId = 1;
            var orders = new List<OrderDto>
            {
                new OrderDto { Id = 1 },
                new OrderDto { Id = 2 }
            };
            _orderServiceMock.Setup(o => o.GetOrdersByUser(userId))
                .ReturnsAsync(orders);

            // Act
            var result = await _orderController.GetOrdersByUser(userId) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(2, ((List<OrderDto>)result.Value).Count);
        }

        [TestMethod]
        public async Task GetOrdersByUser_ShouldReturnNotFound_WhenNoOrdersFound()
        {
            // Arrange
            var userId = 1;
            _orderServiceMock.Setup(o => o.GetOrdersByUser(userId))
                .ThrowsAsync(new Exception("Order not found"));

            // Act
            var result = await _orderController.GetOrdersByUser(userId) as NotFoundObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.AreEqual("Order not found", result.Value);
        }
    }
}
