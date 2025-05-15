using Dapper;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class BookService : IBookService
{
    private readonly DataContext context =  new DataContext();
    public async Task<string> CreateBookAsync(Books book)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into books(title,genre,publicationDate,totalCopies,AviableCopies)
                        values(@title,@genre,@publicationDate,@totalCopies,@AviableCopies)";
            
            var result = await connection.ExecuteAsync(cmd,book);
            return result>0 ? "sucssesfully inserted to table" : "Failed!";
        }
    }

    public async Task<string> DeleteBookAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"Delete from tables where id = @id";
            
            var result = await connection.ExecuteAsync(cmd, new {id = id});
            return result>0 ? "Sucssesfully deleted from table" : "Failed!";
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
            var result = await connection.QueryFirstOrDefaultAsync<Books>(cmd);
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
            genre = @genre, publicationDate = @publicationDate, 
            totalCopies = @totalCopies, AviableCopies = @AvaibleCopies
            where id = @id
            ";
            
            var result = await connection.ExecuteAsync(cmd,book);
            return result>0 ? "Sucssesfully Updated table" : "Failed!";
        }
    }

}
