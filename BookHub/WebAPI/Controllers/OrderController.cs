using BusinessLayer.Models;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrders(int? userId, string? username,
            DateTime? startDate, DateTime? endDate, decimal? totalPrice, int? bookId, string? bookName, OrderStatus? orderStatus)
        {
            try
            {
                return Ok(await _orderService.GetOrdersAsync(userId, username, startDate, endDate, totalPrice, bookId, bookName, orderStatus));
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                return order.Match<ActionResult<OrderDetail>>(
                    o => Ok(o),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> CreateOrder(OrderCreate orderCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                var order = await _orderService.CreateOrderAsync(orderCreate);
                return order.Match<ActionResult<OrderDetail>>(
                    o => Ok(o),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetail>> UpdateOrder(int id, OrderUpdate orderUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                var order = await _orderService.UpdateOrderAsync(id, orderUpdate);
                return order.Match<ActionResult<OrderDetail>>(
                    o => Ok(o),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetail>> DeleteOrder(int id)
        {
            try
            {
                var order= await _orderService.DeleteOrderAsync(id);
                return order.Match<ActionResult<OrderDetail>>(
                    o => Ok(o),
                    e => NotFound(e)
                );
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        } 
        
        private ActionResult HandleOrderException(Exception e)
        {
            return Problem("Unknown problem occured");
        }
    } 
}
