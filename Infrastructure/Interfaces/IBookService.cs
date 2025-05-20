using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IBookService
{
    Task<Response<List<Books>>> GetAllBooksAsync();
    Task<Response<Books>> GetBookByIdAsync(int id);
    Task<Response<string>> CreateBookAsync(Books book);
    Task<Response<string>> UpdateBookAsync(Books book);
    Task<Response<string>> DeleteBookAsync(int id);
    Task<Response<MostPopularBook>> GetMostPopularBookAsync();
    Task<Response<List<Books>>> NotAvailableBooksAsync();
    Task<Response<int>> GetUnpopularBooksCountAsync();
    Task<Response<MostPopularGenre>> GetMostPopularGenreAsync();
    Task<Response<List<MostPopularBook>>> GetMostPopularBooksAsync();
}
