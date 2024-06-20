using HomeService.Core.Contracts.CityContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class CityRepository : ICityRepository
{

    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<CityRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public CityRepository(AppDbContext context, ILogger<CityRepository> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task CreateCity(CityDto model, CancellationToken cancellationToken)
    {
        var newCity = new City()
        {
            Name = model.Name,
            Customers = model.Customers,
            Experts = model.Experts,
            CreateAt = DateTime.Now,
        };
        try
        {
            await _context.Cities.AddAsync(newCity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"City not created Maybe it has already been added Or there is another error {{exception}}", ex);
        }
    }

    public async Task<int> CountCities(CancellationToken cancellationToken)
        => await _context.Cities.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
    => await _context.Cities.AnyAsync(c => c.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var city = await FindCity(id, cancellationToken);
        city.Active = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var city = await FindCity(id, cancellationToken);

        city.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCity(CityDto model, CancellationToken cancellationToken)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(e => e.Name == model.Name, cancellationToken);

        if (city == null) return;

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCityById(int id, CancellationToken cancellationToken)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (city == null) return;

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken)
    {
        var result = await _context.Cities
            .Select(model => new CityDto
            {
                Name = model.Name,
                CreateAt = DateTime.Now,
            }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<City>? GetCityById(int id, CancellationToken cancellationToken)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return city ?? new City();
    }

    public async Task UpdateCity(CityDto model, CancellationToken cancellationToken)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.Id == model.Id, cancellationToken);

        if (city == null) return;

        try
        {
            city.Name = model.Name;
            model.CreateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the category Or there is another error {exception}", ex);
        }
    }
    #endregion

    #region PrivateMethods
    private async Task<City> FindCity(int id, CancellationToken cancellationToken)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (city != null)
        {
            return city;
        }

        throw new Exception($"City with id {id} not found");
    }

    #endregion
}
