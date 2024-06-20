using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class ExpertService : IExpertService
{
    #region Fields
    private readonly IExpertRepository _expertRepository;
    #endregion

    #region Ctors
    public ExpertService(IExpertRepository expertRepository)
    {
        _expertRepository = expertRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateExpert(ExpertDto model, CancellationToken cancellationToken)
       => await _expertRepository.CreateExpert(model, cancellationToken);

    public async Task<int> CountExperts(CancellationToken cancellationToken)
        => await _expertRepository.CountExperts(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _expertRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _expertRepository.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _expertRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteExpert(ExpertDto model, CancellationToken cancellationToken)
    {
        await _expertRepository.DeleteExpert(model, cancellationToken);
    }

    public async Task DeleteExpertById(int id, CancellationToken cancellationToken)
    {
        await _expertRepository.DeleteExpertById(id, cancellationToken);
    }

    public async Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken)
        => await _expertRepository.GetAllExperts(cancellationToken);

    public async Task<Expert>? GetExpertById(int id, CancellationToken cancellationToken)
        => await _expertRepository.GetExpertById(id, cancellationToken);

    public async Task UpdateExpert(ExpertProfileDto model, CancellationToken cancellationToken)
    {
        await _expertRepository.UpdateExpert(model, cancellationToken);
    }

    public async Task<ExpertProfileDto>? GetById(int? id, CancellationToken cancellationToken)
         => await _expertRepository.GetById(id, cancellationToken);

    public async Task<int?> GetExpertIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken)
        => await _expertRepository.GetExpertIdByApplicationUserId(applicationUserId, cancellationToken);

    #endregion
}
