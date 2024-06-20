using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Contracts.RequestContracts;

public interface IRequestRepository
{
    Task<int> CreateRequest(RequestDto model, CancellationToken cancellationToken);
    Task<int> CountRequests(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteRequestById(int id, CancellationToken cancellationToken);
    Task<List<RequestDto>> GetAllRequests(CancellationToken cancellationToken);
    Task<RequestDto> GetRequestById(int requestId, CancellationToken cancellationToken);
    Task UpdateRequest(RequestDto model, CancellationToken cancellationToken);
    Task<ChangeStatusDto> ChangeRequestStatus(ChangeStatusDto status, CancellationToken cancellationToken);
    Task<List<Request>> GetRequestByCustomerId(int? customerId, CancellationToken cancellationToken);
    Task<List<RequestDto>> GetExpertRequest(int? expertId, CancellationToken cancellationToken);
    

}
