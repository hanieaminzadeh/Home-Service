using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using HomeService.Infrastructure.DataAccess.Repo.Ef.Cache.InMemoryCache;
using HomeServices.Endpoint.RazorPages.UI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.ServiceManagement;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IServiceAppService _serviceAppService;
    private readonly IInMemoryCacheService _inMemoryCacheService;

    public List<ServiceDto> Services { get; set; }

    public IndexModel(IServiceAppService serviceAppService, IInMemoryCacheService inMemoryCacheService)
    {
        _serviceAppService = serviceAppService;
        _inMemoryCacheService = inMemoryCacheService;
    }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        //var cacheData = _inMemoryCacheService.Get<List<ServiceDto>>(CacheKey.Services);
        //if (cacheData == null)
        //{
        Services = await _serviceAppService.GetAllServices(cancellationToken);
        //    _inMemoryCacheService.SetSliding(CacheKey.Services, Services, 10);
        //}
    }

    public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
    {
        await _serviceAppService.DeleteServiceById(id, cancellationToken);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
    {
        await _serviceAppService.Active(id, cancellationToken);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnPostDeActive(int id, CancellationToken cancellationToken)
    {
        await _serviceAppService.DeActive(id, cancellationToken);
        return RedirectToPage("Index");
    }
}
