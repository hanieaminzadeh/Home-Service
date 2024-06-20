using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using HomeService.Infrastructure.DataAccess.Repo.Ef.Cache.InMemoryCache;
using HomeServices.Endpoint.RazorPages.UI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.CategoryManagement;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IInMemoryCacheService _inMemoryCacheService;

    public List<CategoryDto> Categories { get; set; }

    public IndexModel(ICategoryAppService categoryAppService, IInMemoryCacheService inMemoryCacheService)
    {
        _categoryAppService = categoryAppService;
        _inMemoryCacheService = inMemoryCacheService;
    }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        //var cacheData = _inMemoryCacheService.Get<List<CategoryDto>>(CacheKey.Categories);
        //if (cacheData == null)
        //{
        Categories = await _categoryAppService.GetAllCategories(cancellationToken);
        //    _inMemoryCacheService.SetSliding(CacheKey.Categories, Categories, 10);
        //}
        //else
        //{
        //    Categories = cacheData;
        //}
    }

    public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
    {
        await _categoryAppService.DeleteCategoryById(id, cancellationToken);
        return RedirectToAction("OnGet");
    }

    public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
    {
        await _categoryAppService.Active(id, cancellationToken);
        return RedirectToAction("OnGet");
    }

    public async Task<IActionResult> OnPostDeActive(int id, CancellationToken cancellationToken)
    {
        await _categoryAppService.DeActive(id, cancellationToken);
        return RedirectToAction("OnGet");
    }
}
