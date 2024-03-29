using System.Text;
using BusinessLayer.Errors;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;

namespace BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly BookHubDbContext _context;
    private readonly IUserStore<User> _userStore;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly IUserEmailStore<User> _emailStore;
    private readonly IMemoryCache _memoryCache;


    public UserService(
        BookHubDbContext context,
        IUserStore<User> userStore,
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager, IMemoryCache memoryCache)
    {
        _context = context;
        _userStore = userStore;
        _userManager = userManager;
        _roleManager = roleManager;
        _memoryCache = memoryCache;
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }

        _emailStore = (IUserEmailStore<User>)_userStore;
    }


    public async Task<IEnumerable<UserDetail>> GetUsersAsync()
    {
        var users = await _context.Users
            .Include(u => u.Orders)
            .Include(u => u.Books)
            .ToListAsync();
        return users.Select(EntityMapper.MapUserToUserDetail);
    }
    
    public async Task<IEnumerable<UserDetail>> GetSearchUsersAsync(string? query)
    {
        var users = _context.Users
            .Include(u => u.Orders)
            .Include(u => u.Books)
            .AsQueryable();

        if (query != null)
        {
            users = users.Where(user => user.Name.ToLower().Contains(query.ToLower()));
        }

        var result = await users.ToListAsync();
        return result.Select(EntityMapper.MapUserToUserDetail);
    }


    public async Task<Result<UserDetail, (Error err, string message)>> GetUserByIdAsync(int id)
    {
        var key = $"BookById_{id}";
        if (_memoryCache.TryGetValue(key, out UserDetail? cached) && cached is not null)
        {
            return cached;
        }

        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return ErrorMessages.UserNotFound(id);
        }

        var mapped = EntityMapper.MapUserToUserDetail(user);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(1));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }

    public async Task<Result<UserDetail, (Error err, string message)>> CreateUserAsync(UserCreate userCreate)
    {
        User user;
        try
        {
            user = Activator.CreateInstance<User>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. ");
        }

        ICollection<Book> books = new List<Book>();
        if (!userCreate.Books.IsNullOrEmpty())
        {
            var bookIds = userCreate.Books.Select(a => a.Id).ToHashSet();
            var bookNames = userCreate.Books.Select(a => a.Name).ToHashSet();
            books = await _context.Books.Where(a => bookNames.Contains(a.Name) || bookIds.Contains(a.Id)).ToListAsync();
            if (books.Count != userCreate.Books.Count)
            {
                return ErrorMessages.BookNotFound();
            }
        }

        user.Name = userCreate.Name;
        user.Books = books;
        await _userStore.SetUserNameAsync(user, userCreate.UserName, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, userCreate.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, userCreate.Password);
        if (!result.Succeeded)
        {
            var errors = new StringBuilder();
            foreach (var err in result.Errors)
            {
                errors.Append($"{err.Code} - {err.Description}\n");
            }

            return ErrorMessages.UserAlreadyExists(userCreate.UserName);
        }

        if (await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return EntityMapper.MapUserToUserDetail(user);
    }

    public async Task<Result<UserDetail, (Error err, string message)>> UpdateUserAsync(int id, UserUpdate userUpdate)
    {
        var user = await _context.Users
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (user == null)
        {
            return ErrorMessages.UserNotFound(id);
        }

        user.Name = userUpdate.Name;
        if (userUpdate.Books is { Count: > 0 })
        {
            var bookNames = userUpdate.Books.Select(a => a.Name).ToHashSet();
            var bookIds = userUpdate.Books.Select(a => a.Id).ToHashSet();

            var books = await _context.Books
                .Where(a => bookNames.Contains(a.Name) || bookIds.Contains(a.Id))
                .ToListAsync();

            if (books.Count != userUpdate.Books.Count)
            {
                return ErrorMessages.BookNotFound();
            }

            user.Books.Clear();
            user.Books.AddRange(books);
        }

        await _userManager.SetUserNameAsync(user, userUpdate.UserName);
        await _userManager.SetEmailAsync(user, userUpdate.Email);
        await _userManager.ChangePasswordAsync(user, userUpdate.OldPassword, userUpdate.NewPassword);
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync();
        return EntityMapper.MapUserToUserDetail(user);
    }

    public async Task<Result<bool, (Error err, string message)>> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return ErrorMessages.UserNotFound(id);
        }

        await _userManager.DeleteAsync(user);
        return true;
    }

    public async Task<Result<(string, User user), (Error err, string message)>> GetUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return ErrorMessages.UserNotFound(id);
        }

        return (await _userManager.GeneratePasswordResetTokenAsync(user), user);
    }

    public async Task<Result<bool, (Error err, string message)>> AddBookToWishlist(int id, int bookId)
    {
        var user = await _context.Users
            .Include(b => b.Books)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return ErrorMessages.UserNotFound(id);
        }

        var toBeAddedBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);

        if (toBeAddedBook == null)
        {
            return ErrorMessages.BookNotFound(bookId);
        }

        if (user.Books.Any(b => b.Id == bookId))
        {
            return false;
        }

        user.Books.Add(toBeAddedBook);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Result<IEnumerable<BookDetail>, (Error err, string message)>> GetBooksInWishlist(int id)
    {
        var user = await _context
            .Users
            .Include(b => b.Books)
            .ThenInclude(pg => pg.PrimaryGenre)
            .Include(b => b.Books)
            .ThenInclude(g => g.Genres)
            .Include(b => b.Books)
            .ThenInclude(b => b.Publisher)
            .Include(b => b.Books)
            .ThenInclude(b => b.Authors)
            .FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return ErrorMessages.UserNotFound(id);
        }

        return user.Books.Select(EntityMapper.MapBookToBookDetail).ToList();
    }

    public async Task<Result<bool, (Error err, string message)>> DeleteBookFromWishlist(int userId, int bookId)
    {
        var user = await _context
            .Users
            .Include(b => b.Books)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            return ErrorMessages.UserNotFound(userId);
        }

        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
        {
            return ErrorMessages.BookNotFound(bookId);
        }

        user.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
    }
}