using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;

namespace HomeService.Core.Services.AppServices;

public class ServiceAppService : IServiceAppService
{

    #region Fields
    private readonly IServiceService _serviceService;
    #endregion

    #region Ctors
    public ServiceAppService(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    #endregion

    #region Implementations
    public async Task CreateService(ServiceDto model, CancellationToken cancellationToken)
        => await _serviceService.CreateService(model, cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _serviceService.IsActive(id, cancellationToken);

    public async Task DeActive(int id, CancellationToken cancellationToken)
        => await _serviceService.DeActive(id, cancellationToken);

    public async Task DeleteService(ServiceDto model, CancellationToken cancellationToken)
    {
        await _serviceService.DeleteService(model, cancellationToken);
    }

    public async Task DeleteServiceById(int id, CancellationToken cancellationToken)
    {
        await _serviceService.DeleteServiceById(id, cancellationToken);
    }

    public async Task<List<ServiceDto>> GetAllServices(CancellationToken cancellationToken)
        => await _serviceService.GetAllServices(cancellationToken);

    public async Task<ServiceDto> GetServiceById(int serviceId, CancellationToken cancellationToken)
        => await _serviceService.GetServiceById(serviceId, cancellationToken);

    public async Task UpdateService(ServiceDto model, CancellationToken cancellationToken)
    {
        await _serviceService.UpdateService(model, cancellationToken);
    }

    public async Task<List<ServiceDto>> GetServicesByCategoryId(int categoryId, CancellationToken cancellationToken)
        => await _serviceService.GetServicesByCategoryId(categoryId, cancellationToken);

    public async Task<ServiceDto> GetService(int serviceId, CancellationToken cancellationToken)
        => await _serviceService.GetService(serviceId, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    => await _serviceService.Active(id, cancellationToken);
    #endregion
}
