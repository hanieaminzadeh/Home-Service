using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.DTOs;
using HomeService.Infrastructure.DataAccess.Repo.Ef.Cache.InMemoryCache;
using HomeServices.Endpoint.RazorPages.UI.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.CommentManagement;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly ICommentAppService _commentAppService;
    private readonly IInMemoryCacheService _inMemoryCacheService;

    public List<CommentDto> Comments { get; set; }

    public IndexModel(ICommentAppService commentAppService, IInMemoryCacheService inMemoryCacheService)
    {
        _commentAppService = commentAppService;
        _inMemoryCacheService = inMemoryCacheService;
    }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        //var cacheData = _inMemoryCacheService.Get<List<CommentDto>>(CacheKey.Comments);
        //if (cacheData == null)
        //{
            Comments = await _commentAppService.GetAllComments(cancellationToken);
        //    _inMemoryCacheService.SetSliding(CacheKey.Comments, Comments, 10);
        //}
        //else
        //{
        //    Comments = cacheData;
        //}
    }

    public async Task<IActionResult> OnGetDelete(int id, CancellationToken cancellationToken)
    {
        await _commentAppService.DeleteCommentById(id, cancellationToken);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnGetActive(int id, CancellationToken cancellationToken)
    {
        await _commentAppService.Active(id, cancellationToken);
        return RedirectToPage("Index");
    }

    public async Task<IActionResult> OnGetDeActive(int id, CancellationToken cancellationToken)
    {
        await _commentAppService.DeActive(id, cancellationToken);
        return RedirectToPage("Index");
    }
}
