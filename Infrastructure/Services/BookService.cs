using System.Net;
using Dapper;
using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public async Task<Response<string>> CreateBookAsync(Books book)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into books(title,genre,publicationYear,totalCopies,AvailableCopies)
                        values(@title,@genre,@publicationYear,@totalCopies,@AvailableCopies)";

            var result = await connection.ExecuteAsync(cmd, book);
            return result == null
            ? new Response<string>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null,"Success");
        }
    }

    public async Task<Response<string>> DeleteBookAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"Delete from books where id = @id";

            var result = await connection.ExecuteAsync(cmd, new { id = id });
            return result == null
            ? new Response<string>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(default,"Success");
        }

    }

    public async Task<Response<List<Books>>> GetAllBooksAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from books";

            var result = await connection.QueryAsync<Books>(cmd);
            return new Response<List<Books>>(result.ToList(), "Success");
        }
    }

    public async Task<Response<Books>> GetBookByIdAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from books where id = @id";
            var result = await connection.QueryFirstOrDefaultAsync<Books>(cmd, new { id = id });
            return result == null
            ? new Response<Books>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<Books>(null,"Success");
        }
    }

    public async Task<Response<string>> UpdateBookAsync(Books book)
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
            return result == null
            ? new Response<string>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<string>(null,"Success");
        }
    }

    public async Task<Response<MostPopularBook>> GetMostPopularBookAsync()
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
            return result == null
            ? new Response<MostPopularBook>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<MostPopularBook>(null,"Success");
        }
    }

    public async Task<Response<List<Books>>> NotAvailableBooksAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from books where availableCopies = 0";

            var res = await connection.QueryAsync<Books>(cmd);
            return new Response<List<Books>>(res.ToList(), "Success");
        }
    }

    public async Task<Response<int>> GetUnpopularBooksCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            SELECT COUNT(*) 
FROM books bk
JOIN borrowings b ON bk.id = b.bookId
WHERE b.id IS NULL";

            var res = await connection.ExecuteScalarAsync(cmd);
            return res == null
            ? new Response<int>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<int>(default,"Success");
        }
    }

    public async Task<Response<MostPopularGenre>> GetMostPopularGenreAsync()
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
            return res == null
            ? new Response<MostPopularGenre>("Some thing went wrong", HttpStatusCode.InternalServerError)
            : new Response<MostPopularGenre>(null,"Success");
        }

    }

    public async Task<Response<List<MostPopularBook>>> GetMostPopularBooksAsync()
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
            return new Response<List<MostPopularBook>>(result.ToList(), "Success");
        }
    }
}
