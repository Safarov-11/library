using DoMain.ApiResponse;
using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<Response<List<Members>>> GetAllMembersAsync();
    Task<Response<Members>> GetMemberByIdAsync(int id);
    Task<Response<string>> CreateMemberAsync(Members member);
    Task<Response<string>> UpdateMemberAsync(Members member);
    Task<Response<string>> DeleteMemberAsync(int id);
    Task<Response<MostActiveMember>> GetMostActiveMemberAsync();
    Task<Response<int>> GetMembersWithBorrowingCountAsync();
    Task<Response<FirstMemberWithFine>> GetFirstMemberWithFine();
    Task<Response<List<MostActiveMember>>> GetTop5ostActiveMembersAsync();
    Task<Response<List<Members>>> GetMembersWhoPayFineAsync();
}
