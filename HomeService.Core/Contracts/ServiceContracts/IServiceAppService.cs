using HomeService.Core.DTOs;

namespace HomeService.Core.Contracts.ServiceContracts;

public interface IServiceAppService
{
    Task CreateService(ServiceDto model, CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteService(ServiceDto model, CancellationToken cancellationToken);
    Task DeleteServiceById(int id, CancellationToken cancellationToken);
    Task<List<ServiceDto>> GetAllServices(CancellationToken cancellationToken);
    Task<ServiceDto> GetServiceById(int serviceId, CancellationToken cancellationToken);
    Task UpdateService(ServiceDto model, CancellationToken cancellationToken);
    Task<List<ServiceDto>> GetServicesByCategoryId(int categoryId, CancellationToken cancellationToken);
    Task<ServiceDto> GetService(int serviceId, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);

}
