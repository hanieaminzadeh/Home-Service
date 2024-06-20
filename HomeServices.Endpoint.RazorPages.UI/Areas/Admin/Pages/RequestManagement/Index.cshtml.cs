using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using HomeService.Infrastructure.DataAccess.Repo.Ef.Cache.InMemoryCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.RequestManagement;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IRequestAppService _requestAppService;
    private readonly IInMemoryCacheService _inMemoryCacheService;

    public List<RequestDto> Requests { get; set; }

    [BindProperty]
    public ChangeStatusDto NewStatus { get; set; }

    [BindProperty]
    public int RequestId { get; set; }

    public IndexModel(IRequestAppService requestAppService, IInMemoryCacheService inMemoryCacheService)
    {
        _requestAppService = requestAppService;
        _inMemoryCacheService = inMemoryCacheService;
    }
    public async Task OnGet(CancellationToken cancellationToken)
    {
        Requests = await _requestAppService.GetAllRequests(cancellationToken);
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        NewStatus.Id  = RequestId;
        await _requestAppService.ChangeRequestStatus(NewStatus, cancellationToken);
        return RedirectToPage("Index");
    }
    public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
    {
        await _requestAppService.DeleteRequestById(id, cancellationToken);
        return RedirectToAction("OnGet");
    }
}
