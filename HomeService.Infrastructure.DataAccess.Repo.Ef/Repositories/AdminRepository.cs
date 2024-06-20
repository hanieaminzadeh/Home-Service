using HomeService.Core.Contracts.AdminContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class AdminRepository : IAdminRepository
{

    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<AdminRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public AdminRepository(AppDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task CreateAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        var newAdmin = new Admin()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            ProfileImgUrl = model.ProfileImgUrl,
            PhoneNumber = model.PhoneNumber,
            CreateAt = DateTime.Now,
        };
        try
        {
            await _context.Admins.AddAsync(newAdmin);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Admin Added Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("admin not created Maybe it has already been added Or there is another error {exception}", ex);
        }
    }

    public async Task<int> CountAdmins(CancellationToken cancellationToken)
        => await _context.Admins.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
    => await _context.Admins.AnyAsync(a => a.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var admin = await FindAdmin(id, cancellationToken);
        admin.Active = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var admin = await FindAdmin(id, cancellationToken);

        admin.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.FirstName == model.FirstName ||
                                 a.LastName == model.LastName ||
                                 a.PhoneNumber == model.PhoneNumber, cancellationToken);

        if (admin == null) return;

        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAdminById(int id, CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (admin == null) return;

        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<AdminDto>> GetAllAdmins(CancellationToken cancellationToken)
    {
        var result = await _context.Admins
            .Select(model => new AdminDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileImgUrl = model.ProfileImgUrl,
                PhoneNumber = model.PhoneNumber,
                CreateAt = DateTime.Now,
            }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Admin>? GetAdminById(int id, CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return admin ?? new Admin();
    }

    public async Task UpdateAdmin(AdminDto model, CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Id == model.Id, cancellationToken);

        if (admin == null) return;

        try
        {
            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            admin.ProfileImgUrl = model.ProfileImgUrl;
            admin.PhoneNumber = model.PhoneNumber;
            model.CreateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the admin Or there is another error {exception}", ex);
        }
    }
    #endregion

    #region PrivateMethods
    private async Task<Admin> FindAdmin(int id, CancellationToken cancellationToken)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (admin != null)
        {
            return admin;
        }

        throw new Exception($"admin with id {id} not found");
    }

    #endregion

}
