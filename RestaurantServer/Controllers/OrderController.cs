using BusinessLayer.Abstract;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Mvc;
using RestaurantServer.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder", Name = "CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrder createOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var order = await _orderService.AddOrder(createOrder);
                return CreatedAtRoute("GetOrderById", new { id = order.Id }, order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "RemoveOrder")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            try
            {
                await _orderService.RemoveOrder(id);
                return Ok($"Order with ID {id} removed successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetOrders", Name = "GetOrders")]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _orderService.GetOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOrdersByUser/{userId}", Name = "GetOrdersByUser")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUser(userId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("UpdateOrder", Name = "UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto updateOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedOrder = await _orderService.UpdateOrderById(updateOrderDto);
                return Ok($"Order with ID {updateOrderDto.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
