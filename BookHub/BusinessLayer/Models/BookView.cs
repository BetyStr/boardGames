namespace BusinessLayer.Models;

public class BookView
{
    public IEnumerable<BookDetail> Books { get; init; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    
    
    public BookView(IEnumerable<BookDetail> books, int currentPage, int totalPages)
    {
        Books = books;
        CurrentPage = currentPage;
        TotalPages = totalPages;
    }
    
}