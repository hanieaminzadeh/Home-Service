using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class CategoryRepository : ICategoryRepository
{

    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<CategoryRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public CategoryRepository(AppDbContext context, ILogger<CategoryRepository> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task CreateCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        var newCategory = new Category()
        {
            Id = model.Id,
            Name = model.Name,
            CreateAt = DateTime.Now,
            Services = model.Services,
            ImgUrl = model.ImgUrl,
        };
        try
        {
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Category Added Succesfully");
        }
        catch (Exception ex)
        {
            _logger.LogError("Category not created Maybe it has already been added Or there is another error {exception}", ex);
        }
    }

    public async Task<int> CountCategories(CancellationToken cancellationToken)
        => await _context.Categories.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
    => await _context.Categories.AnyAsync(c => c.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var category = await FindCategory(id, cancellationToken);
        category.IsDeleted = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var category = await FindCategory(id, cancellationToken);

        category.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == model.Name, cancellationToken);

        if (category == null) return;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCategoryById(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category == null) return;

        //_context.Categories.Remove(category);
        category.IsDeleted = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CategoryDto>> GetAllCategories(CancellationToken cancellationToken)
    {
        var result = await _context.Categories
            .Select(model => new CategoryDto
            {
                Id = model.Id,
                Name = model.Name,
                CreateAt = DateTime.Now,
                Services = model.Services,
                IsDeleted = model.IsDeleted,
                ImgUrl = model.ImgUrl,
                Active = model.Active,
            }).ToListAsync(cancellationToken);

        _memoryCache.Set("result", result);
        return result;
    }

    //public async Task<Category>? GetCategoryById(int id, CancellationToken cancellationToken)
    //{
    //    var category = await _context.Categories
    //        .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    //    return category ?? new Category();
    //}
    public async Task<CategoryDto> GetCategoryById(int categoryId, CancellationToken cancellationToken)
    {
        var category = _memoryCache.Get<CategoryDto>("categoryDto");
        if (category is null)
        {
            category = await _context.Categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }).FirstOrDefaultAsync(a => a.Id == categoryId, cancellationToken);

            if (category != null)
            {
                _memoryCache.Set("categoryDto", category, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(120)
                });
                _logger.LogInformation("categoryDto returned from database, and cached in memory successfully.");
                return category;
            }
            else
            {
                _logger.LogError("We expected the categoryDto to return from the database, but it returned null.");
                throw new Exception("Something wents wrong!, please try again.");
            }
        }
        _logger.LogInformation("categoryDto returned from InMemoryCache.");
        return category;
    }

    public async Task UpdateCategory(CategoryDto model, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == model.Id, cancellationToken);

        if (category == null) return;

        try
        {
            category.Id = model.Id;
            category.Name = model.Name;
            category.Services = model.Services;
            category.ImgUrl = model.ImgUrl;
            model.CreateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the category Or there is another error {exception}", ex);
        }
    }

    #endregion

    #region PrivateMethods
    private async Task<Category> FindCategory(int id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (category != null)
        {
            return category;
        }

        throw new Exception($"category with id {id} not found");
    }
     
    public int Sum(int numA,int numB)
    {
        return numA+numB;
    }

    #endregion
}
