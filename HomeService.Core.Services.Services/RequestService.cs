using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class RequestService : IRequestService
{

    #region Fields
    private readonly IRequestRepository _requestrRepository;
    private readonly IRequestDapperRepository _dapperRepository;
    #endregion

    #region Ctors
    public RequestService(IRequestRepository requestRepository, IRequestDapperRepository dapperRepository)
    {
        _requestrRepository = requestRepository;
        _dapperRepository = dapperRepository;
    }
    #endregion

    #region Implementations
    public async Task<int> CreateRequest(RequestDto model, CancellationToken cancellationToken)
        => await _requestrRepository.CreateRequest(model, cancellationToken);

    public async Task<int> CountRequests(CancellationToken cancellationToken)
        => await _requestrRepository.CountRequests(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _requestrRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _requestrRepository.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _requestrRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteRequestById(int id, CancellationToken cancellationToken)
    {
        await _requestrRepository.DeleteRequestById(id, cancellationToken);
    }

    public async Task<List<RequestDto>> GetAllRequests(CancellationToken cancellationToken)
        => await _dapperRepository.GetRequests(cancellationToken);

    public async Task<RequestDto> GetRequestById(int requestId, CancellationToken cancellationToken)
        => await _requestrRepository.GetRequestById(requestId, cancellationToken);

    public async Task UpdateRequest(RequestDto model, CancellationToken cancellationToken)
    {
        await _requestrRepository.UpdateRequest(model, cancellationToken);
    }

    public async Task<ChangeStatusDto> ChangeRequestStatus(ChangeStatusDto newStatus, CancellationToken cancellationToken)
        => await _requestrRepository.ChangeRequestStatus(newStatus, cancellationToken);

    public async Task<List<Request>> GetRequestByCustomerId(int? customerId, CancellationToken cancellationToken)
        => await _requestrRepository.GetRequestByCustomerId(customerId, cancellationToken);

	public async Task<List<RequestDto>> GetExpertRequest(int? expertId, CancellationToken cancellationToken)
	=> await _requestrRepository.GetExpertRequest(expertId, cancellationToken);


	#endregion
}