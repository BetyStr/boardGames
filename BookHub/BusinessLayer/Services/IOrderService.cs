using BusinessLayer.Errors;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDetail>> GetOrdersAsync(int? userId, string? username,
        DateTime? startDate, DateTime? endDate, decimal? totalPrice, int? bookId, string? bookName,
        OrderStatus? orderStatus);

    Task<Result<OrderDetail, (Error err, string message)>> GetOrderByIdAsync(int id);
    Task<Result<OrderDetail, (Error err, string message)>> CreateOrderAsync(OrderCreate orderCreate);
    Task<Result<OrderDetail, (Error err, string message)>> UpdateOrderAsync(int id, OrderUpdate orderUpdate);
    Task<Result<bool, (Error err, string message)>> DeleteOrderAsync(int id);
    Task<Result<bool, (Error err, string message)>> AppendBookToOrder(int userId, int bookId);
    Task<Result<OrderDetail, (Error err, string message)>> GetUnpaidOrder(int userId);

    Task<Result<bool, (Error err, string message)>> PayOrderAsync(int id);
    Task<Result<bool, (Error err, string message)>> RemoveBookFromOrder(int userId, int bookId);

}