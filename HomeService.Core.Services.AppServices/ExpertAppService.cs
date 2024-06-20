using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class ExpertAppService : IExpertAppService
{
    #region Fields
    private readonly IExpertService _expertService;
    #endregion

    #region Ctors
    public ExpertAppService(IExpertService expertService)
    {
        _expertService = expertService;
    }
    #endregion

    #region Implementations
    public async Task CreateExpert(ExpertDto model, CancellationToken cancellationToken)
       => await _expertService.CreateExpert(model, cancellationToken);

    public async Task<int> CountExperts(CancellationToken cancellationToken)
        => await _expertService.CountExperts(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _expertService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _expertService.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _expertService.DeActive(id, cancellationToken);
    }

    public async Task DeleteExpert(ExpertDto model, CancellationToken cancellationToken)
    {
        await _expertService.DeleteExpert(model, cancellationToken);
    }

    public async Task DeleteExpertById(int id, CancellationToken cancellationToken)
    {
        await _expertService.DeleteExpertById(id, cancellationToken);
    }

    public async Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken)
        => await _expertService.GetAllExperts(cancellationToken);

    public async Task<Expert>? GetExpertById(int id, CancellationToken cancellationToken)
        => await _expertService.GetExpertById(id, cancellationToken);

    public async Task UpdateExpert(ExpertProfileDto model, CancellationToken cancellationToken)
    {
        await _expertService.UpdateExpert(model, cancellationToken);
    }

    public async Task<ExpertProfileDto>? GetById(int? id, CancellationToken cancellationToken)
        => await _expertService.GetById(id, cancellationToken);

    public async Task<int?> GetExpertIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken)
        => await _expertService.GetExpertIdByApplicationUserId(applicationUserId, cancellationToken);
    #endregion
}
