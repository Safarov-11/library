using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IBorrowingService
{
    Task<string> CreateBorrowingAsync(Borrowings borrowing);
    Task<List<Borrowings>> GetAllBorrowingsAsync();
    Task<List<Borrowings>> GetMemberBorrowingsAsync(int memberId);
    Task<string> UpdateBorrowingAsync(Borrowings borrowing);
}
