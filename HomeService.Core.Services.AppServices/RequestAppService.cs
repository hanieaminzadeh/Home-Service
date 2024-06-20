using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class RequestAppService : IRequestAppService
{

    #region Fields
    private readonly IRequestService _requestService;
    #endregion

    #region Ctors
    public RequestAppService(IRequestService requestService)
    {
        _requestService = requestService;
    }
    #endregion

    #region Implementations

    public async Task<int> CreateRequest(RequestDto model, CancellationToken cancellationToken)
    {
        var requestId = await _requestService.CreateRequest(model, cancellationToken);

        var statusModel = new ChangeStatusDto
        {
            Id = requestId,
            Status = Enums.RequestStatus.Registered,
        };

        await _requestService.ChangeRequestStatus(statusModel, cancellationToken);

        return requestId;
    }

    public async Task<int> CountRequests(CancellationToken cancellationToken)
        => await _requestService.CountRequests(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _requestService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _requestService.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _requestService.DeActive(id, cancellationToken);
    }

    public async Task DeleteRequestById(int id, CancellationToken cancellationToken)
    {
        await _requestService.DeleteRequestById(id, cancellationToken);
    }

    public async Task<List<RequestDto>> GetAllRequests(CancellationToken cancellationToken)
        => await _requestService.GetAllRequests(cancellationToken);

    public async Task<RequestDto> GetRequestById(int requestId, CancellationToken cancellationToken)
        => await _requestService.GetRequestById(requestId, cancellationToken);

    public async Task UpdateRequest(RequestDto model, CancellationToken cancellationToken)
    {
        await _requestService.UpdateRequest(model, cancellationToken);
    }

    public async Task<ChangeStatusDto> ChangeRequestStatus(ChangeStatusDto newStatus, CancellationToken cancellationToken)
        => await _requestService.ChangeRequestStatus(newStatus, cancellationToken);
	
	public async Task<List<Request>> GetRequestByCustomerId(int? customerId, CancellationToken cancellationToken)
        => await _requestService.GetRequestByCustomerId(customerId, cancellationToken);

	public async Task<List<RequestDto>> GetExpertRequest(int? expertId, CancellationToken cancellationToken)
		=> await _requestService.GetExpertRequest(expertId, cancellationToken);

	#endregion
}