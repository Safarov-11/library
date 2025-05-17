using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IBorrowingService
{
    Task<string> CreateBorrowingAsync(Borrowings borrowing);
    Task<List<Borrowings>> GetAllBorrowingsAsync();
    Task<List<Borrowings>> GetMemberBorrowingsAsync(int memberId);
    Task<string> ReturnBookAsync(int borrowingId);
    Task<int> AllBorrowingsCountAsync();
    Task<decimal> GetAvgFineAsync();
    Task<List<NotReturnedBooks>> GetNotReturnedBooksAsync();
    
}
