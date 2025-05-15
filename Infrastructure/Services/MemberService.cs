using Dapper;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class MemberService : IMemberService
{
    private readonly DataContext context =  new DataContext();



    public async Task<string> CreateMemberAsync(Members member)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into members(fullName, phone, email, membershipDate)
                        values(@fullName, @phone, @email, @membershipDate)";
            
            var result = await connection.ExecuteAsync(cmd,member);
            return result>0 ? "sucssesfully inserted to table" : "Failed!";
        }
    }

    public async Task<string> DeleteMemberAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"Delete from tables where id = @id";
            
            var result = await connection.ExecuteAsync(cmd, new {id = id});
            return result>0 ? "Sucssesfully deleted from table" : "Failed!";
        }
        
    }


    public async Task<List<Members>> GetAllMembersAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from members";     

            var result = await connection.QueryAsync<Members>(cmd);
            return result.ToList();
        }
    }


    public async Task<Members> GetMemberByIdAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from members where id = @id";
            var result = await connection.QueryFirstOrDefaultAsync<Members>(cmd);
            return result;
        }
    }


    public async Task<string> UpdateMemberAsync(Members member)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            Update members 
            set fullName = @fullName, phone = @phone,
            email = @email, membershipDate = @membershipDate
            where id = @id
            ";
            
            var result = await connection.ExecuteAsync(cmd,member);
            return result>0 ? "Sucssesfully Updated table" : "Failed!";
        }
    }
}
