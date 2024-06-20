using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class CategoryAppService : ICategoryAppService
{

    #region Fields
    private readonly ICategoryService _categoryService;
    #endregion

    #region Ctors
    public CategoryAppService(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    #endregion

    #region Implementations
    public async Task CreateCategory(CategoryDto model, CancellationToken cancellationToken)
       => await _categoryService.CreateCategory(model, cancellationToken);

    public async Task<int> CountCategories(CancellationToken cancellationToken)
        => await _categoryService.CountCategories(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _categoryService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _categoryService.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _categoryService.DeActive(id, cancellationToken);
    }

    public async Task DeleteCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteCategory(model, cancellationToken);
    }

    public async Task DeleteCategoryById(int id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteCategoryById(id, cancellationToken);
    }

    public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
        => await _categoryService.GetAllCategories(cancellationToken);

    public async Task<CategoryDto> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        => await _categoryService.GetCategoryById(categoryId, cancellationToken);

    public async Task UpdateCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        await _categoryService.UpdateCategory(model, cancellationToken);
    }

    #endregion
}
