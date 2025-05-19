using DoMain.DTOs;
using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("api/[controller]")]
public class MemberController(IMemberService MemberServ)
{
    [HttpGet]
    public async Task<List<Members>> GetAllMembersAsync()
    {
        return await MemberServ.GetAllMembersAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Members> GetMemberByIdAsync(int id)
    {
        return await MemberServ.GetMemberByIdAsync(id);
    }

    [HttpPost]
    public async Task<string> CreateMemberAsync(Members member)
    {
        return await MemberServ.CreateMemberAsync(member);
    }

    [HttpPut]
    public async Task<string> UpdateMemberAsync(Members member)
    {
        return await MemberServ.UpdateMemberAsync(member);
    }

    [HttpDelete]
    public async Task<string> DeleteMemberAsync(int id)
    {
        return await MemberServ.DeleteMemberAsync(id);
    }

    [HttpGet("Most Active Member")]
    public async Task<MostActiveMember> GetMostPopuarBookAsync()
    {
        return await MemberServ.GetMostActiveMemberAsync();
    }
    
    [HttpGet("Coun of Members with borrowings")]
    public async Task<int> GetMembersWithBorrowingCountAsync()
    {
        return await MemberServ.GetMembersWithBorrowingCountAsync();
    }

    [HttpGet("First member with fine")]
    public async Task<FirstMemberWithFine> GetFirstMemberWithFine()
    {
        return await MemberServ.GetFirstMemberWithFine();
    }

    [HttpGet("5 most Active Member")]
    public async Task<List<MostActiveMember>> GetTop5ostActiveMembersAsync()
    {
        return await MemberServ.GetTop5ostActiveMembersAsync();
    }

    [HttpGet("Members who payed fine")]
    public async Task<List<Members>> GetMembersWhoPayFineAsync()
    {
        return await MemberServ.GetMembersWhoPayFineAsync();
    }
}
