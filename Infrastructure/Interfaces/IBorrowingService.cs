using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IBorrowingService
{
    Task<Response<string>> CreateBorrowingAsync(Borrowings borrowing);
    Task<Response<List<Borrowings>>> GetAllBorrowingsAsync();
    Task<Response<List<Borrowings>>> GetMemberBorrowingsAsync(int memberId);
    Task<Response<string>> ReturnBookAsync(int borrowingId);
    Task<Response<int>> AllBorrowingsCountAsync();
    Task<Response<decimal>> GetAvgFineAsync();
    Task<Response<List<NotReturnedBooks>>> GetNotReturnedBooksAsync();
    Task<Response<decimal>> GetSumOfFinesAsync();
    Task<Response<int>> GetCountOfFinesAsync();
}
