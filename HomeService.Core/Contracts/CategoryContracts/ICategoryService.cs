using HomeService.Core.DTOs;

namespace HomeService.Core.Contracts.CategoryContracts;

public interface ICategoryService
{
    Task CreateCategory(CategoryDto model, CancellationToken cancellationToken);
    Task<int> CountCategories(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteCategory(CategoryDto model, CancellationToken cancellationToken);
    Task DeleteCategoryById(int id, CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken);
    Task<CategoryDto> GetCategoryById(int categoryId, CancellationToken cancellationToken);
    Task UpdateCategory(CategoryDto model, CancellationToken cancellationToken);
}
