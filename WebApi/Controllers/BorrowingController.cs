using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BorrowingController
{
    private IBorrowingService brServ = new BorrowingService();

    [HttpPost]
    public async Task<string> CreateBorrowingAsync(Borrowings borrowing){
        return await brServ.CreateBorrowingAsync(borrowing);
    }

    [HttpGet]
    public Task<List<Borrowings>> GetAllBorrowingsAsync(){
        return brServ.GetAllBorrowingsAsync();
    }

    [HttpGet("{id:int}")]
    public Task<List<Borrowings>> GetMemberBorrowingsByIdAsync(int id){
        return brServ.GetMemberBorrowingsAsync(id);
    }
}
