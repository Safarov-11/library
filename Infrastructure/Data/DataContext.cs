using System.Data;
using Npgsql;

namespace Infrastructure.Data;

public class DataContext
{
    private const string connectionString = "Host = localhost; Database = library_db;User id = postgres; password = sr000080864";
    public Task<NpgsqlConnection> GetDbConnectionAsync(){
        return Task.FromResult(new NpgsqlConnection(connectionString));
    }
}
