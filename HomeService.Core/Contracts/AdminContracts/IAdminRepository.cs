using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Contracts.AdminContracts;

public interface IAdminRepository
{
    Task CreateAdmin(AdminDto model, CancellationToken cancellationToken);
    Task<int> CountAdmins(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteAdmin(AdminDto model, CancellationToken cancellationToken);
    Task DeleteAdminById(int id, CancellationToken cancellationToken);
    Task<List<AdminDto>> GetAllAdmins(CancellationToken cancellationToken);
    Task<Admin>? GetAdminById(int id, CancellationToken cancellationToken);
    Task UpdateAdmin(AdminDto model, CancellationToken cancellationToken);
}
