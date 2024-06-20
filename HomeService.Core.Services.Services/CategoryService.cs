using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class CategoryService : ICategoryService
{
    #region Fields
    private readonly ICategoryRepository _categoryRepository;
    #endregion

    #region Ctors
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateCategory(CategoryDto model, CancellationToken cancellationToken)
       => await _categoryRepository.CreateCategory(model, cancellationToken);

    public async Task<int> CountCategories(CancellationToken cancellationToken)
        => await _categoryRepository.CountCategories(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _categoryRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _categoryRepository.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteCategory(model, cancellationToken);
    }

    public async Task DeleteCategoryById(int id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteCategoryById(id, cancellationToken);
    }

    public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        => await _categoryRepository.GetAllCategories(cancellationToken);

    public async Task<CategoryDto> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        => await _categoryRepository.GetCategoryById(categoryId, cancellationToken);

    public async Task UpdateCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        await _categoryRepository.UpdateCategory(model, cancellationToken);
    }

    #endregion
}
