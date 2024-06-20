using HomeService.Core.Contracts.CityContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class CityService : ICityService
{

    #region Fields
    private readonly ICityRepository _cityRepository;
    #endregion

    #region Ctors
    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateCity(CityDto model, CancellationToken cancellationToken)
         => await _cityRepository.CreateCity(model, cancellationToken);

    public async Task<int> CountCities(CancellationToken cancellationToken)
        => await _cityRepository.CountCities(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _cityRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _cityRepository.IsActive(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _cityRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteCity(CityDto model, CancellationToken cancellationToken)
    {
        await _cityRepository.DeleteCity(model, cancellationToken);
    }

    public async Task DeleteCityById(int id, CancellationToken cancellationToken)
    {
        await _cityRepository.DeleteCityById(id, cancellationToken);
    }

    public async Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken)
        => await _cityRepository.GetAllCities(cancellationToken);

    public async Task<City>? GetCityById(int id, CancellationToken cancellationToken)
        => await _cityRepository.GetCityById(id, cancellationToken);

    public async Task UpdateCity(CityDto model, CancellationToken cancellationToken)
    {
        await _cityRepository.UpdateCity(model, cancellationToken);
    }
    #endregion
}
