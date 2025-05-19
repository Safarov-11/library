using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController
{
    private IBookService bookServ = new BookService();
    [HttpGet]
    public async Task<List<Books>> GetAllBooksAsync()
    {
        return await bookServ.GetAllBooksAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Books> GetBookByIdAsync(int id)
    {
        return await bookServ.GetBookByIdAsync(id);
    }

    [HttpPost]
    public async Task<string> CreateBookAsync(Books book)
    {
        return await bookServ.CreateBookAsync(book);
    }

    [HttpPut]
    public async Task<string> UpdateBookAsync(Books book)
    {
        return await bookServ.UpdateBookAsync(book);
    }

    [HttpDelete]
    public async Task<string> DeleteBookAsync(int id)
    {
        return await bookServ.DeleteBookAsync(id);
    }

    [HttpGet("Most Popular Book")]
    public async Task<MostPopularBook> GetMostPopuarBookAsync()
    {
        return await bookServ.GetMostPopularBookAsync();
    }

    [HttpGet("Not Available books")]
    public async Task<List<Books>> NotAvailableBooksAsync()
    {
        return await bookServ.NotAvailableBooksAsync();
    }
    
    [HttpGet("Get unpopular Books count")]
    public async Task<int> GetUnpopularBooksAsync()
    {
        return await bookServ.GetUnpopularBooksCountAsync();
    }

    [HttpGet("Get most popular genre")]
    public async Task<MostPopularGenre> GetMostPopularGenreAsync()
    {
        return await bookServ.GetMostPopularGenreAsync();
    }

    [HttpGet("Popular Books")]
    public async Task<List<MostPopularBook>> GetMostPopularBooksAsync()
    {
        return await bookServ.GetMostPopularBooksAsync();
    }
}
