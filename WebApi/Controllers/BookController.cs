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
    public async Task<List<Books>> GetAllBooksAsync(){
        return await bookServ.GetAllBooksAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Books> GetBookByIdAsync(int id){
        return await bookServ.GetBookByIdAsync(id);
    }

    [HttpPost]
    public async Task<string> CreateBookAsync(Books book){
        return await bookServ.CreateBookAsync(book);
    }

    [HttpPut]
    public async Task<string> UpdateBookAsync(Books book){
        return await bookServ.UpdateBookAsync(book);
    }

    [HttpDelete]
    public async Task<string> DeleteBookAsync(int id){
        return await bookServ.DeleteBookAsync(id);
    }
}
