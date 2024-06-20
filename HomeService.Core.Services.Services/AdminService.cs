using HomeService.Core.Contracts.AdminContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class AdminService : IAdminService
{
    #region Fields
    private readonly IAdminRepository _adminRepository;
    #endregion

    #region Ctors
    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateAdmin(AdminDto model, CancellationToken cancellationToken)
       => await _adminRepository.CreateAdmin(model, cancellationToken);

    public async Task<int> CountAdmins(CancellationToken cancellationToken)
        => await _adminRepository.CountAdmins(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _adminRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _adminRepository.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _adminRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        await _adminRepository.DeleteAdmin(model, cancellationToken);
    }

    public async Task DeleteAdminById(int id, CancellationToken cancellationToken)
    {
        await _adminRepository.DeleteAdminById(id, cancellationToken);
    }

    public async Task<List<AdminDto>> GetAllAdmins(CancellationToken cancellationToken)
        => await _adminRepository.GetAllAdmins(cancellationToken);

    public async Task<Admin>? GetAdminById(int id, CancellationToken cancellationToken)
        => await _adminRepository.GetAdminById(id, cancellationToken);

    public async Task UpdateAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        await _adminRepository.UpdateAdmin(model, cancellationToken);
    }

    #endregion
}