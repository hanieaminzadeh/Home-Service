using HomeService.Core.Contracts.AdminContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class AdminAppService : IAdminAppService
{
    #region Fields
    private readonly IAdminService _adminService;
    #endregion

    #region Ctors
    public AdminAppService(IAdminService adminService)
    {
        _adminService = adminService;
    }
    #endregion

    #region Implementations
    public async Task CreateAdmin(AdminDto model, CancellationToken cancellationToken)
   => await _adminService.CreateAdmin(model, cancellationToken);

    public async Task<int> CountAdmins(CancellationToken cancellationToken)
        => await _adminService.CountAdmins(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _adminService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _adminService.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _adminService.DeActive(id, cancellationToken);
    }

    public async Task DeleteAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        await _adminService.DeleteAdmin(model, cancellationToken);
    }

    public async Task DeleteAdminById(int id, CancellationToken cancellationToken)
    {
        await _adminService.DeleteAdminById(id, cancellationToken);
    }

    public async Task<List<AdminDto>> GetAllAdmins(CancellationToken cancellationToken)
        => await _adminService.GetAllAdmins(cancellationToken);

    public async Task<Admin>? GetAdminById(int id, CancellationToken cancellationToken)
        => await _adminService.GetAdminById(id, cancellationToken);

    public async Task UpdateAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        await _adminService.UpdateAdmin(model, cancellationToken);
    }
    #endregion
}