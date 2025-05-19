using Dapper;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BorrowingService(DataContext context) : IBorrowingService
{
    public async Task<int> AllBorrowingsCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select count(borrowings.id) from borrowings";

            var res = await connection.ExecuteScalarAsync(cmd);
            return Convert.ToInt32(res);

        }
    }

    public async Task<decimal> GetAvgFineAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select avg(fine) from borrowings";

            var res = await connection.ExecuteScalarAsync(cmd);
            return Convert.ToInt32(res);

        }
    }


    public async Task<string> CreateBorrowingAsync(Borrowings borrowing)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd1 = @"select * from books where id = @id";
            var res1 = await connection.QueryFirstOrDefaultAsync<Books>(cmd1, new { id = borrowing.BookId });
            if (res1 == null)
            {
                return "Book with this Id is not exist!";
            }

            var cmd2 = @"select * from members where id = @id";
            var res2 = await connection.QueryFirstOrDefaultAsync<Books>(cmd2, new { id = borrowing.MemberId });
            if (res2 == null)
            {
                return "Member with this Id is not exist!";
            }

            if (res1.AvailableCopies == 0)
            {
                return "There isn't avaible copies of this book";
            }

            if (borrowing.BorrowDate >= borrowing.DueDate)
            {
                return "Borrowing due date is earlier";
            }

            var cmd = @"insert into borrowings(bookId, memberId, borrowDate, dueDate)
                        values(@BookId, @memberId, @borrowDate, @dueDate)";

            var result = await connection.ExecuteAsync(cmd, borrowing);

            var updateBookCommand = @"update books set availableCopies = availableCopies - 1 where id = @id";
            await connection.ExecuteAsync(updateBookCommand, new { Id = borrowing.BookId });

            return result > 0 ? "Sucsessfully inserted" : "Failed";

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
            var result = await connection.QueryAsync<Borrowings>(cmd, new { memberId = memberId });
            return result.ToList();
        }
    }

    public async Task<string> ReturnBookAsync(int borrowingId)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from borrowings where id = @id";
            var borrowing = await connection.QueryFirstOrDefaultAsync<Borrowings>(cmd, new { id = borrowingId });
            if (borrowing == null)
            {
                return "borrowing not found!";
            }
            borrowing.ReturnDate = DateTime.Now;
            if (borrowing.ReturnDate > borrowing.DueDate)
            {
                var days = borrowing.ReturnDate.Value.Day - borrowing.DueDate.Day;
                borrowing.Fine = days * 10;
            }

            var updateBorrowingCommand = "update borrowings set returnDate = @returnDate, fine = @fine where id = @id";
            var result = await connection.ExecuteAsync(updateBorrowingCommand, borrowing);
            if (result == 0)
            {
                return "Borrowing not updated";
            }

            var updateBookCommand = @"update books set availableCopies = availableCopies + 1 where id = @id";
            await connection.ExecuteAsync(updateBookCommand, new { id = borrowing.BookId });

            return "Borrowing updated";
        }
    }

    public async Task<List<NotReturnedBooks>> GetNotReturnedBooksAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select bk.title, bk.genre, b.returnDate from books bk
join borrowings b on bk.id = b.bookId
group by bk.title, bk.genre, b.returnDate 
having b.returnDate is null";
            var result = await connection.QueryAsync<NotReturnedBooks>(cmd);
            return result.ToList();
        }

    }

    public async Task<decimal> GetSumOfFinesAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select sum(fine) from borrowings";
            var result = await connection.ExecuteScalarAsync(cmd);
            return Convert.ToInt32(result);
        }
    }

    public async Task<int> GetCountOfFinesAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select count(bookid) from borrowings
                        where returnDate>DueDate";
            var result = await connection.ExecuteScalarAsync(cmd);
            return Convert.ToInt32(result);
        }
    }
    

}
