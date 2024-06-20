using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class ServiceRepository : IServiceRepository
{

    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<ServiceRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public ServiceRepository(AppDbContext context, ILogger<ServiceRepository> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task CreateService(ServiceDto model, CancellationToken cancellationToken)
    {
        var newService = new Service()
        {
            Id = model.Id,
            Title = model.Title,
            CategoryId = model.CategoryId,
            Category = model.Category,
            Description = model.Description,
            Price = model.Price,
            ImgUrl = model.ImgUrl,
            Experts = model.Experts,
            Requests = model.Requests,
            CreatAt = DateTime.Now,
        };
        try
        {
            await _context.Services.AddAsync(newService);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Service Added Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Service not created Maybe it has already been added Or there is another error {exception}", ex);
        }
    }

    public async Task<int> CountServices(CancellationToken cancellationToken)
        => await _context.Services.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _context.Services.AnyAsync(s => s.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var service = await FindService(id, cancellationToken);
        service.IsDeleted = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var service = await FindService(id, cancellationToken);

        service.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteService(ServiceDto model, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Title == model.Title, cancellationToken);

        if (service == null) return;

        _context.Services.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteServiceById(int id, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (service == null) return;

        _context.Services.Remove(service);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ServiceDto>> GetAllServices(CancellationToken cancellationToken)
    {
        var result = await _context.Services
            .Select(model => new ServiceDto
            {
                Id = model.Id,
                Title = model.Title,
                Category = model.Category,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = model.Price,
                ImgUrl = model.ImgUrl,
                Requests = model.Requests,
                Experts = model.Experts,
                CreatAt = DateTime.Now,
                IsDeleted = model.IsDeleted,
            }).ToListAsync(cancellationToken);

        return result;
    }

    //public async Task<Service>? GetServiceById(int id, CancellationToken cancellationToken)
    //{
    //    var service = await _context.Services
    //        .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    //    return service ?? new Service();
    //}

    public async Task<List<ServiceDto>> GetServicesByCategoryId(int categoryId, CancellationToken cancellationToken)
    {
        var services = await _context.Services
            .Where(s => s.CategoryId == categoryId)
            .Select(s => new ServiceDto()
            {
                Id = s.Id,
                CategoryId = categoryId,
                Title = s.Title,
                Description = s.Description,
                IsDeleted = s.IsDeleted,
                ImgUrl = s.ImgUrl,
            }).ToListAsync(cancellationToken);
        return services;
    }
    public async Task<ServiceDto> GetServiceById(int serviceId, CancellationToken cancellationToken)
    {
        var service = _memoryCache.Get<ServiceDto>("serviceDto");
        if (service is null)
        {
            service = await _context.Services
            .Select(c => new ServiceDto
            {
                Id = c.Id,
                Title = c.Title,
            }).FirstOrDefaultAsync(a => a.Id == serviceId, cancellationToken);

            if (service != null)
            {
                _memoryCache.Set("ServiceDto", service, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(120)
                });
                _logger.LogInformation("ServiceDto returned from database, and cached in memory successfully.");
                return service;
            }
            else
            {
                _logger.LogError("We expected the ServiceDto to return from the database, but it returned null.");
                throw new Exception("Something wents wrong!, please try again.");
            }
        }
        _logger.LogInformation("ServiceDto returned from InMemoryCache.");
        return service;
    }
    public async Task UpdateService(ServiceDto model, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == model.Id, cancellationToken);

        if (service == null) return;

        try
        {
            service.Id = model.Id;
            service.Title = model.Title;
            service.Category = model.Category;
            service.Description = model.Description;
            service.Price = model.Price;
            service.ImgUrl = model.ImgUrl;
            service.Requests = model.Requests;
            service.Experts = model.Experts;
            service.CreatAt = DateTime.Now;
            service.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the service Or there is another error {exception}", ex);
        }
    }

    public async Task<ServiceDto> GetService(int serviceId, CancellationToken cancellationToken)
    {
        var record = await _context.Services
            .Select(s => new ServiceDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                ImgUrl = s.ImgUrl,
                Price = s.Price,
            }).FirstOrDefaultAsync(a => a.Id == serviceId, cancellationToken);
        return record;
    }

    #endregion

    #region PrivateMethods
    private async Task<Service> FindService(int id, CancellationToken cancellationToken)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        if (service != null)
        {
            return service;
        }

        throw new Exception($"Service with id {id} not found");
    }

    #endregion
}
