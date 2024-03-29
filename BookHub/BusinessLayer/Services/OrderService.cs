using BusinessLayer.Errors;
using DataAccessLayer;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;


namespace BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly BookHubDbContext _context;
        private readonly IMemoryCache _memoryCache;


        public OrderService(BookHubDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrdersAsync(int? userId, string? username,
            DateTime? startDate, DateTime? endDate, decimal? totalPrice, int? bookId, string? bookName,
            OrderStatus? orderStatus)
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .AsQueryable();

            if (userId.HasValue)
            {
                orders = orders.Where(o => o.User.Id == userId.Value);
            }

            if (!string.IsNullOrEmpty(username))
            {
                orders = orders.Where(o => o.User.Name == username);
            }

            if (startDate.HasValue)
            {
                orders = orders.Where(o => o.Date >= startDate.Value);
            }


            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.Date <= endDate.Value);
            }

            if (orderStatus.HasValue)
            {
                orders = orders.Where(ps => ps.OrderStatus == orderStatus);
            }

            if (totalPrice.HasValue)
            {
                orders = orders.Where(o => o.TotalPrice == totalPrice.Value);
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName || b.Id == bookId);
            if (book != null)
            {
                orders = orders.Where(o => o.Books.Contains(book));
            }

            var filteredOrders = await orders.ToListAsync();
            return filteredOrders.Select(EntityMapper.MapOrderToOrderDetail);
        }

        public async Task<Result<OrderDetail, (Error err, string message)>> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ErrorMessages.OrderNotFound(id);
            }

            var mapped = EntityMapper.MapOrderToOrderDetail(order);
            return mapped;
        }

        public async Task<Result<OrderDetail, (Error err, string message)>> CreateOrderAsync(OrderCreate orderCreate)
        {
            if (orderCreate.Books.IsNullOrEmpty())
            {
                return ErrorMessages.BooksEmpty();
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Name == orderCreate.User.Name || u.Id == orderCreate.User.Id);
            if (user == null)
            {
                return ErrorMessages.UserNotFound(orderCreate.User.Id, orderCreate.User.Name);
            }

            var order = new Order
            {
                User = user,
                TotalPrice = orderCreate.TotalPrice
            };

            var bookNames = orderCreate.Books.Select(a => a.Name).ToHashSet();
            var bookIds = orderCreate.Books.Select(a => a.Id).ToHashSet();

            var books = await _context.Books
                .Where(b => bookNames.Contains(b.Name) || bookIds.Contains(b.Id))
                .ToListAsync();

            if (books.Count != orderCreate.Books.Count)
            {
                return ErrorMessages.BookNotFound();
            }

            order.Books.AddRange(books);
            order.OrderStatus = OrderStatus.Unpaid;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return EntityMapper.MapOrderToOrderDetail(order);
        }

        public async Task<Result<OrderDetail, (Error err, string message)>> UpdateOrderAsync(int id, OrderUpdate orderUpdate)
        {
            var order = await _context.Orders
                .Include(o => o.Books)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ErrorMessages.OrderNotFound(id);
            }

            order.TotalPrice = orderUpdate.TotalPrice;
            order.OrderStatus = orderUpdate.OrderStatus;

            if (orderUpdate.Books.Count != 0)
            {
                var bookNames = orderUpdate.Books.Select(b => b.Name).ToHashSet();
                var bookIds = orderUpdate.Books.Select(b => b.Id).ToHashSet();

                var books = await _context.Books
                    .Where(b => bookNames.Contains(b.Name) || bookIds.Contains(b.Id))
                    .ToListAsync();

                if (books.Count != orderUpdate.Books.Count)
                {
                    return ErrorMessages.BookNotFound();
                }

                order.Books.Clear();
                order.Books.AddRange(books);
            }

            await _context.SaveChangesAsync();
            return EntityMapper.MapOrderToOrderDetail(order);
        }

        public async Task<Result<bool, (Error err, string message)>> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Books)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ErrorMessages.OrderNotFound(id);
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<Result<OrderDetail, (Error err, string message)>> GetUnpaidOrder(int userId)
        {
            var res = await GetUnpaid(userId);
            if (res is null)
            {
                return ErrorMessages.UserNotFound(userId);
            }

            return EntityMapper.MapOrderToOrderDetail(res);
        }


        public async Task<Result<bool, (Error err, string message)>> PayOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ErrorMessages.OrderNotFound(id);
            }

            order.OrderStatus = OrderStatus.Paid;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Result<bool, (Error err, string message)>> AppendBookToOrder(int userId, int bookId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return ErrorMessages.UserNotFound(userId);
            }
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book == null)
            {
                return ErrorMessages.BookNotFound(bookId);
            }

            var order = await _context
                .Orders
                .Include(order => order.Books)
                .FirstOrDefaultAsync(o =>
                    o.UserId == userId && o.OrderStatus == OrderStatus.Unpaid);

            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    User = user,
                    TotalPrice = book.Price,
                    OrderStatus = OrderStatus.Unpaid,
                    Date = DateTime.Now,
                };
                _context.Orders.Add(order);
            }
            else
            {
                order.TotalPrice += book.Price;
            }

            var orderItem =
                await _context.BookOrders.FirstOrDefaultAsync(bo => bo.BookId == book.Id && bo.OrderId == order.Id);
            if (orderItem != null)
            {
                orderItem.Count += 1;
            }
            else
            {
                _context.BookOrders.Add(new BookOrder
                {
                    OrderId = order.Id,
                    Order = order,
                    BookId = book.Id,
                    Book = book,
                    Count = 1,
                    BuyUnitPrice = book.Price
                });
            }

            book.StockInStorage -= 1;
            _memoryCache.Remove($"BookById_{book.Id}");
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<Result<bool, (Error err, string message)>> RemoveBookFromOrder(int userId, int bookId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (user == null)
            {
                return ErrorMessages.UserNotFound(userId);
            }

            if (book == null)
            {
                return ErrorMessages.BookNotFound(bookId);
            }

            var order = await _context
                .Orders
                .Include(order => order.Books)
                .FirstOrDefaultAsync(o =>
                    o.UserId == userId && o.OrderStatus == OrderStatus.Unpaid);

            if (order == null)
            {
                return ErrorMessages.OrderNotFound(userId, OrderStatus.Unpaid);
            }
            order.TotalPrice -= book.Price;
            var orderItem =
                await _context.BookOrders.FirstOrDefaultAsync(bo => bo.BookId == book.Id && bo.OrderId == order.Id);
            if (orderItem == null)
            {
                return ErrorMessages.OrderItemNotFound(userId, bookId);
            }

            orderItem.Count -= 1;
            if (orderItem.Count <= 0)
            {
                order.BookOrders.Remove(orderItem);   
            }
            book.StockInStorage += 1;
            _memoryCache.Remove($"BookById_{book.Id}");
            await _context.SaveChangesAsync();
            return true;
        }
        
        private async Task<Order?> GetUnpaid(int userId)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .Include(o => o.BookOrders)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.Unpaid);
            return order;
        }

        private void FixPrices()
        {
            var orders = _context.Orders
                .Include(order => order.Books)
                .Include(order => order.BookOrders).ToList();
            
            foreach (var order in orders)
            {
                order.TotalPrice = order.BookOrders.Sum(bk => bk.Book.Price * bk.Count);
            }
            
            foreach (var order in orders)
            {
                foreach (var bookOrder in order.BookOrders)
                {
                    bookOrder.BuyUnitPrice = _context.Books.FirstOrDefault(b => b.Id == bookOrder.BookId)!.Price;
                }
            }

            _context.SaveChanges();
        }
    }
}