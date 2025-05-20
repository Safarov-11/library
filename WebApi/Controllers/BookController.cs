using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bookServ)
{
    [HttpGet]
    public async Task<Response<List<Books>>> GetAllBooksAsync()
    {
        return await bookServ.GetAllBooksAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Response<Books>> GetBookByIdAsync(int id)
    {
        return await bookServ.GetBookByIdAsync(id);
    }

    [HttpPost]
    public async Task<Response<string>> CreateBookAsync(Books book)
    {
        return await bookServ.CreateBookAsync(book);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateBookAsync(Books book)
    {
        return await bookServ.UpdateBookAsync(book);
    }

    [HttpDelete]
    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        return await bookServ.DeleteBookAsync(id);
    }

    [HttpGet("Most Popular Book")]
    public async Task<Response<MostPopularBook>> GetMostPopuarBookAsync()
    {
        return await bookServ.GetMostPopularBookAsync();
    }

    [HttpGet("Not Available books")]
    public async Task<Response<List<Books>>> NotAvailableBooksAsync()
    {
        return await bookServ.NotAvailableBooksAsync();
    }
    
    [HttpGet("Get unpopular Books count")]
    public async Task<Response<int>> GetUnpopularBooksAsync()
    {
        return await bookServ.GetUnpopularBooksCountAsync();
    }

    [HttpGet("Get most popular genre")]
    public async Task<Response<MostPopularGenre>> GetMostPopularGenreAsync()
    {
        return await bookServ.GetMostPopularGenreAsync();
    }

    [HttpGet("Popular Books")]
    public async Task<Response<List<MostPopularBook>>> GetMostPopularBooksAsync()
    {
        return await bookServ.GetMostPopularBooksAsync();
    }
}
