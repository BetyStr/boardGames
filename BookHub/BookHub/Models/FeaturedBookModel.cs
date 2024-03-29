using BusinessLayer.Models;

namespace BookHub.Models;

public class FeaturedBookModel
{
    public required IEnumerable<BookDetail> Book;
    public required string ImageUrl { get; set; }
}