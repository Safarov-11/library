using Dapper;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class MemberService : IMemberService
{
    private readonly DataContext context = new DataContext();



    public async Task<string> CreateMemberAsync(Members member)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into members(fullName, phone, email, membershipDate)
                        values(@fullName, @phone, @email, @membershipDate)";

            var result = await connection.ExecuteAsync(cmd, member);
            return result > 0 ? "sucssesfully inserted to table" : "Failed!";
        }
    }

    public async Task<string> DeleteMemberAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"Delete from members where id = @id";

            var result = await connection.ExecuteAsync(cmd, new { id = id });
            return result > 0 ? "Sucssesfully deleted from table" : "Failed!";
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
            var result = await connection.QueryFirstOrDefaultAsync<Members>(cmd, new { id = id });
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
            email = @email
            where id = @id
            ";

            var result = await connection.ExecuteAsync(cmd, member);
            return result > 0 ? "Sucssesfully Updated table" : "Failed!";
        }
    }

    public async Task<MostActiveMember> GetMostActiveMemberAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select m.fullname, m.email, count(b.id) as borrowingsCount
from borrowings b 
join members m on m.id = b.memberId
group by m.fullname, m.email 
order by borrowingsCount desc
limit 1";

            var result = await connection.QuerySingleOrDefaultAsync<MostActiveMember>(cmd);
            return result;
        }
    }

    public async Task<int> GetMembersWithBorrowingCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select count(*) from members m 
join borrowings b on b.memberId = m.id
where b.id > 0";

            var res = await connection.ExecuteScalarAsync(cmd);
            return Convert.ToInt32(res);
        }
    }

    public async Task<FirstMemberWithFine> GetFirstMemberWithFine()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select m.fullname, m.email, b.duedate, b.returndate, b.fine
from borrowings b
join members m on m.id = b.memberId
where b.returnDate > b.duedate
order by b.returnDate
limit 1";

            var res = await connection.QueryFirstOrDefaultAsync<FirstMemberWithFine>(cmd);
            return res;
        }
    }


}
