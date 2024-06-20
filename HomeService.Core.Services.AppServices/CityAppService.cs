using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.Contracts.CityContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class CityAppService : ICityAppService
{

    #region Fields
    private readonly ICityService _cityService;
    #endregion

    #region Ctors
    public CityAppService(ICityService cityService)
    {
        _cityService = cityService;
    }
    #endregion

    #region Implementations

    public async Task CreateCity(CityDto model, CancellationToken cancellationToken)
         => await _cityService.CreateCity(model, cancellationToken);

    public async Task<int> CountCities(CancellationToken cancellationToken)
        => await _cityService.CountCities(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _cityService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _cityService.IsActive(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _cityService.DeActive(id, cancellationToken);
    }

    public async Task DeleteCity(CityDto model, CancellationToken cancellationToken)
    {
        await _cityService.DeleteCity(model, cancellationToken);
    }

    public async Task DeleteCityById(int id, CancellationToken cancellationToken)
    {
        await _cityService.DeleteCityById(id, cancellationToken);
    }

    public async Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken)
        => await _cityService.GetAllCities(cancellationToken);

    public async Task<City>? GetCityById(int id, CancellationToken cancellationToken)
        => await _cityService.GetCityById(id, cancellationToken);

    public async Task UpdateCity(CityDto model, CancellationToken cancellationToken)
    {
        await _cityService.UpdateCity(model, cancellationToken);
    }
    #endregion
}
