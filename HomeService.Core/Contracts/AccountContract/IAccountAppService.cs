using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HomeService.Core.Contracts.ApplicationUser;

public interface IAccountAppService
{
    Task<bool> Login(ApplicationUserLoginDto model, CancellationToken cancellationToken);
    Task<List<IdentityError>> Register(ApplicationUserDto model, CancellationToken cancellationToken);
    Task<CustomerProfileDto> GetCustomer(int? id, int applicationUserId, CancellationToken cancellationToken);
    bool IsRole(string role);
    Task<ExpertProfileDto> GetExpert(int? id, int applicationUserId, CancellationToken cancellationToken);
}
