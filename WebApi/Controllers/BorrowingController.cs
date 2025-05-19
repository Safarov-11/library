using DoMain.DTOs;
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


    [HttpGet]
    public async Task<List<Borrowings>> GetAllBorrowingsAsync()
    {
        return await brServ.GetAllBorrowingsAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<List<Borrowings>> GetMemberBorrowingsByIdAsync(int id)
    {
        return await brServ.GetMemberBorrowingsAsync(id);
    }

    [HttpPost]
    public async Task<string> CreateBorrowingAsync(Borrowings borrowing)
    {
        return await brServ.CreateBorrowingAsync(borrowing);
    }

    [HttpPut]
    public async Task<string> ReturnBookAsync(int borrowingId)
    {
        return await brServ.ReturnBookAsync(borrowingId);
    }

    [HttpGet("All borrowings count")]
    public async Task<int> AllBorrowingsCountAsync()
    {
        return await brServ.AllBorrowingsCountAsync();
    }
    [HttpGet("Avg fine")]
    public async Task<decimal> GetAvgFineAsync()
    {
        return await brServ.GetAvgFineAsync();
    }

    [HttpGet("Not Returned Books")]
    public async Task<List<NotReturnedBooks>> NotReturnedBooksAsync()
    {
        return await brServ.GetNotReturnedBooksAsync();
    }
    
    [HttpGet("Sum off all fines")]
    public async Task<decimal> GetSumOfFinesAsync()
    {
        return await brServ.GetSumOfFinesAsync();
    }
    
    [HttpGet("count off all fines")]
    public async Task<int> GetCountOfFinesAsync()
    {
        return await brServ.GetCountOfFinesAsync();
    }
}
