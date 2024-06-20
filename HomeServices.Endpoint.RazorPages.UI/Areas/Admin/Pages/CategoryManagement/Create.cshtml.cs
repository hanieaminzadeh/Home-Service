using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.CategoryManagement;

[Authorize(Roles ="Admin")]
public class CreateModel : PageModel
{

    private readonly ICategoryAppService _categoryAppService;

    [BindProperty]
    public CategoryDto Category { get; set; }

    public CreateModel(ICategoryAppService categoryAppServices)
    {
        _categoryAppService = categoryAppServices;
    }

    public async Task OnGet()
    {
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        await _categoryAppService.CreateCategory(Category, cancellationToken);
        return RedirectToPage("Index");
    }
}
