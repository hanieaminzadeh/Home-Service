using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Contracts.ExpertContracts;

public interface IExpertRepository
{
    Task CreateExpert(ExpertDto model, CancellationToken cancellationToken);
    Task<int> CountExperts(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteExpert(ExpertDto model, CancellationToken cancellationToken);
    Task DeleteExpertById(int id, CancellationToken cancellationToken);
    Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken);
    Task<Expert>? GetExpertById(int id, CancellationToken cancellationToken);
    Task UpdateExpert(ExpertProfileDto model, CancellationToken cancellationToken);
    Task<ExpertProfileDto>? GetById(int? id, CancellationToken cancellationToken);
    Task<int?> GetExpertIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken);
}
