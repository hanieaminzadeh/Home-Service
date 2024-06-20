using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.CategoryManagement;

[Authorize(Roles = "Admin")]
public class UpdateModel : PageModel
{
    private readonly ICategoryAppService _categoryAppService;

    public UpdateModel(ICategoryAppService categoryAppService)
    {
        _categoryAppService = categoryAppService;
    }

    [BindProperty]
    public CategoryDto Category { get; set; }

    [BindProperty]
    public int Id { get; set; }


    public async Task OnGet(int id, CancellationToken cancellationToken)
    {
        Category = await _categoryAppService.GetCategoryById(id, cancellationToken);
        Id = id;
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        await _categoryAppService.UpdateCategory(Category, cancellationToken);
        return RedirectToPage("Index");
    }
}
