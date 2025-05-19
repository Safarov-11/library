using Dapper;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public async Task<string> CreateBookAsync(Books book)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into books(title,genre,publicationYear,totalCopies,AvailableCopies)
                        values(@title,@genre,@publicationYear,@totalCopies,@AvailableCopies)";

            var result = await connection.ExecuteAsync(cmd, book);
            return result > 0 ? "sucssesfully inserted to table" : "Failed!";
        }
    }

    public async Task<string> DeleteBookAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"Delete from books where id = @id";

            var result = await connection.ExecuteAsync(cmd, new { id = id });
            return result > 0 ? "Sucssesfully deleted from table" : "Failed!";
        }

    }

    public async Task<List<Books>> GetAllBooksAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from books";

            var result = await connection.QueryAsync<Books>(cmd);
            return result.ToList();
        }
    }

    public async Task<Books> GetBookByIdAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from books where id = @id";
            var result = await connection.QueryFirstOrDefaultAsync<Books>(cmd, new { id = id });
            return result;
        }
    }

    public async Task<string> UpdateBookAsync(Books book)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            Update books 
            set title = @title, 
            genre = @genre, publicationYear = @publicationYear, 
            totalCopies = @totalCopies, AvailableCopies = @AvailableCopies
            where id = @id
            ";

            var result = await connection.ExecuteAsync(cmd, book);
            return result > 0 ? "Sucssesfully Updated table" : "Failed!";
        }
    }

    public async Task<MostPopularBook> GetMostPopularBookAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select bk.title, bk.genre, bk.totalcopies, count(b.id) as borrowingsCount
from borrowings b
join books bk on bk.id = b.bookId
group by bk.title, bk.genre, bk.totalcopies
order by borrowingsCount desc
limit 1";

            var result = await connection.QuerySingleOrDefaultAsync<MostPopularBook>(cmd);
            return result;
        }
    }

    public async Task<List<Books>> NotAvailableBooksAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from books where availableCopies = 0";

            var res = await connection.QueryAsync<Books>(cmd);
            return res.ToList();
        }
    }

    public async Task<int> GetUnpopularBooksCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            SELECT COUNT(*) 
FROM books bk
JOIN borrowings b ON bk.id = b.bookId
WHERE b.id IS NULL";

            var res = await connection.ExecuteScalarAsync(cmd);
            return Convert.ToInt32(res);
        }
    }

    public async Task<MostPopularGenre> GetMostPopularGenreAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select  genre, count(id)  from books 
group by  genre
order by count desc
limit 1
";

            var res = await connection.QuerySingleOrDefaultAsync<MostPopularGenre>(cmd);
            return res;
        }

    }

    public async Task<List<MostPopularBook>> GetMostPopularBooksAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select bk.title, bk.genre, bk.totalcopies, count(b.id) as borrowingsCount
from borrowings b
join books bk on bk.id = b.bookId
group by bk.title, bk.genre, bk.totalcopies
having count(b.id)>5";

            var result = await connection.QueryAsync<MostPopularBook>(cmd);
            return result.ToList();
        }
    }
    
}
