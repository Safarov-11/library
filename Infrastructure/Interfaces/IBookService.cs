using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Task<List<Books>> GetAllBooksAsync();
    Task<Books> GetBookByIdAsync(int id);
    Task<string> CreateBookAsync(Books book);
    Task<string> UpdateBookAsync(Books book);
    Task<string> DeleteBookAsync(int id);

}
