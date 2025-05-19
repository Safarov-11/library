using DoMain.DTOs;
using DoMain.Entities;

namespace Infrastructure.Interfaces;

public interface IMemberService
{
    Task<List<Members>> GetAllMembersAsync();
    Task<Members> GetMemberByIdAsync(int id);
    Task<string> CreateMemberAsync(Members member);
    Task<string> UpdateMemberAsync(Members member);
    Task<string> DeleteMemberAsync(int id);
    Task<MostActiveMember> GetMostActiveMemberAsync();
    Task<int> GetMembersWithBorrowingCountAsync();
    Task<FirstMemberWithFine> GetFirstMemberWithFine();
    Task<List<MostActiveMember>> GetTop5ostActiveMembersAsync();
    Task<List<Members>> GetMembersWhoPayFineAsync();
}
