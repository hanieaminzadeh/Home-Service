using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.ServiceManagement;

[Authorize(Roles = "Admin")]
public class UpdateModel : PageModel
{

    private readonly IServiceAppService _serviceAppService;
    private readonly ICategoryAppService _categoryAppService;

    public UpdateModel(IServiceAppService serviceAppService, ICategoryAppService categoryAppService)
    {
        _serviceAppService = serviceAppService;
        _categoryAppService = categoryAppService;
    }

    [BindProperty]
    public List<ServiceDto> Services { get; set; }

    [BindProperty]
    public List<CategoryDto> Categories { get; set; }

    [BindProperty]
    public ServiceDto Service { get; set; }

    [BindProperty]
    public int Id { get; set; }

    public async Task OnGet(int id, CancellationToken cancellationToken)
    {
        Categories = await _categoryAppService.GetAllCategories(cancellationToken);
        Service = await _serviceAppService.GetServiceById(id, cancellationToken);
        Id = id;
    }

    //public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    //{
    //    await _serviceAppService.UpdateService(Service, cancellationToken);
    //    return RedirectToPage("Index");
    //}
    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            await _serviceAppService.UpdateService(Service, cancellationToken);
        }
        return RedirectToPage("Index");
    }
}
