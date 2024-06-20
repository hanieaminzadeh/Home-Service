using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Contracts.CityContracts;

public interface ICityService
{
    Task CreateCity(CityDto model, CancellationToken cancellationToken);
    Task<int> CountCities(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteCity(CityDto model, CancellationToken cancellationToken);
    Task DeleteCityById(int id, CancellationToken cancellationToken);
    Task<List<CityDto>> GetAllCities(CancellationToken cancellationToken);
    Task<City>? GetCityById(int id, CancellationToken cancellationToken);
    Task UpdateCity(CityDto model, CancellationToken cancellationToken);
}
