using Dapper;
using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class MemberService(DataContext context) : IMemberService
{
    public async Task<Response<string>> CreateMemberAsync(Members member)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"insert into members(fullName, phone, email, membershipDate)
                        values(@fullName, @phone, @email, @membershipDate)";

            var result = await connection.ExecuteAsync(cmd, member);
            return result == null
            ? new Response<string>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<string>(null,"Success");
        }
    }

    public async Task<Response<string>> DeleteMemberAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"Delete from members where id = @id";

            var result = await connection.ExecuteAsync(cmd, new { id = id });
            return result == null
            ? new Response<string>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<string>(null,"Success");
        }

    }


    public async Task<Response<List<Members>>> GetAllMembersAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from members";

            var result = await connection.QueryAsync<Members>(cmd);
            return new Response<List<Members>>(result.ToList(), "Success");
        }
    }


    public async Task<Response<Members>> GetMemberByIdAsync(int id)
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"select * from members where id = @id";
            var result = await connection.QueryFirstOrDefaultAsync<Members>(cmd, new { id = id });
            return result == null
            ? new Response<Members>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<Members>(null,"Success");
        }
    }


    public async Task<Response<string>> UpdateMemberAsync(Members member)
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
            return result == null
            ? new Response<string>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<string>(null,"Success");
        }
    }

    public async Task<Response<MostActiveMember>> GetMostActiveMemberAsync()
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
            return result == null
            ? new Response<MostActiveMember>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<MostActiveMember>(null,"Success");
        }
    }

    public async Task<Response<int>> GetMembersWithBorrowingCountAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select count(*) from members m 
join borrowings b on b.memberId = m.id
where b.id > 0";

            var res = await connection.ExecuteScalarAsync(cmd);
            return res == null
            ? new Response<int>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<int>(default,"Success");
        }
    }

    public async Task<Response<FirstMemberWithFine>> GetFirstMemberWithFine()
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
            return res == null
            ? new Response<FirstMemberWithFine>("Some thing went wrong", System.Net.HttpStatusCode.InternalServerError)
            : new Response<FirstMemberWithFine>(null,"Success");
        }
    }
    public async Task<Response<List<MostActiveMember>>> GetTop5ostActiveMembersAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select m.fullname, m.email, count(b.id) as borrowingsCount
from borrowings b 
join members m on m.id = b.memberId
group by m.fullname, m.email 
order by borrowingsCount desc
limit 5";

            var result = await connection.QueryAsync<MostActiveMember>(cmd);
            return new Response<List<MostActiveMember>>(result.ToList(), "Success");
        }
    }

    public async Task<Response<List<Members>>> GetMembersWhoPayFineAsync()
    {
        using (var connection = await context.GetDbConnectionAsync())
        {
            var cmd = @"
            select m.id, m.fullname, m.email from borrowings b
join members m on m.id = b.memberId
where returnDate>DueDate and fine = 0";

            var result = await connection.QueryAsync<Members>(cmd);
            return new Response<List<Members>>(result.ToList(), "Success");
        }
    }
}
