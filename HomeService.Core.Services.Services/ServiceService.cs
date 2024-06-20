using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;

namespace HomeService.Core.Services.Services;

public class ServiceService : IServiceService
{

    #region Fields
    private readonly IServiceRepository _serviceRepository;
    #endregion

    #region Ctors
    public ServiceService(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateService(ServiceDto model, CancellationToken cancellationToken)
        => await _serviceRepository.CreateService(model, cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _serviceRepository.IsActive(id, cancellationToken);

    public async Task DeActive(int id, CancellationToken cancellationToken)
        => await _serviceRepository.DeActive(id, cancellationToken);

    public async Task DeleteService(ServiceDto model, CancellationToken cancellationToken)
    {
        await _serviceRepository.DeleteService(model, cancellationToken);
    }

    public async Task DeleteServiceById(int id, CancellationToken cancellationToken)
    {
        await _serviceRepository.DeleteServiceById(id, cancellationToken);
    }

    public async Task<List<ServiceDto>> GetAllServices(CancellationToken cancellationToken)
        => await _serviceRepository.GetAllServices(cancellationToken);

    public async Task<ServiceDto> GetServiceById(int serviceId, CancellationToken cancellationToken)
        => await _serviceRepository.GetServiceById(serviceId, cancellationToken);

    public async Task UpdateService(ServiceDto model, CancellationToken cancellationToken)
    {
        await _serviceRepository.UpdateService(model, cancellationToken);
    }

    public async Task<List<ServiceDto>> GetServicesByCategoryId(int categoryId, CancellationToken cancellationToken)
        => await _serviceRepository.GetServicesByCategoryId(categoryId, cancellationToken);

    public async Task<ServiceDto> GetService(int serviceId, CancellationToken cancellationToken)
        => await _serviceRepository.GetService(serviceId, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    => await _serviceRepository.Active(id, cancellationToken);
    #endregion
}
