using Dapper;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BorrowingService : IBorrowingService
{
    private readonly DataContext context = new DataContext();
    public async Task<string> CreateBorrowingAsync(Borrowings borrowing)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from books where id = bookId";
            var res1 = await connection.QueryFirstOrDefaultAsync<Books>(cmd1, new { id = borrowing.BookId });
            if (res1 == null)
            {
                return "Book with this Id is not exist!";
            }
            
            var cmd2 = @"select * from members where id = memberIdId";
            var res2 = await connection.QueryFirstOrDefaultAsync<Books>(cmd2, new { id = borrowing.MemberId });
            if (res2 == null)
            {
                return "Member with this Id is not exist!";
            }

            if (res1.AvaibleCopies == 0)
            {
                return "There isn't avaible copies of this book";
            }

            var newAvaibleCopies = res1.AvaibleCopies - 1;
            res1.AvaibleCopies = newAvaibleCopies;

            var cmd = @"insert into borrowings(bookId, memberId, borrowDate, dueDate, returnDate, fine)
                        values(@bookId, @memberId, @borrowDate, @dueDate, @returnDate, @fine)";

            var result = await connection.ExecuteAsync(cmd,borrowing);
            return result>0 ? "Sucsessfully inserted" : "Failed";
 
        }
    }

    public async Task<List<Borrowings>> GetAllBorrowingsAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from borrowings";
            var result = await connection.QueryAsync<Borrowings>(cmd);
            return result.ToList();
        }
        
    }

    public async Task<List<Borrowings>> GetMemberBorrowingsAsync(int memberId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from borrowings 
                        where memberId = @memberId";
            var result = await connection.QueryAsync<Borrowings>(cmd, new {memberId = memberId});
            return result.ToList();
        }
    }

    public Task<string> UpdateBorrowingAsync(Borrowings borrowing)
    {
        throw new NotImplementedException();
    }

}
