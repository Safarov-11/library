using DoMain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("api/[controller]")]
public class MemberController
{
    private IMemberService MemberServ = new MemberService();
    [HttpGet]
    public async Task<List<Members>> GetAllMembersAsync(){
        return await MemberServ.GetAllMembersAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<Members> GetMemberByIdAsync(int id){
        return await MemberServ.GetMemberByIdAsync(id);
    }

    [HttpPost]
    public async Task<string> CreateMemberAsync(Members member){
        return await MemberServ.CreateMemberAsync(member);
    }

    [HttpPut]
    public async Task<string> UpdateMemberAsync(Members member){
        return await MemberServ.UpdateMemberAsync(member);
    }

    [HttpDelete]
    public async Task<string> DeleteMemberAsync(int id){
        return await MemberServ.DeleteMemberAsync(id);
    }
}
