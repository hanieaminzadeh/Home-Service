using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Admin.Pages.ServiceManagement;

[Authorize(Roles = "Admin")]
public class CreateModel : PageModel
{
    private readonly IServiceAppService _serviceAppService;
    private readonly ICategoryAppService _categoryAppService;

    [BindProperty]
    public ServiceDto Service { get; set; }

    [BindProperty]
    public List<ServiceDto> Services { get; set; }

    [BindProperty]
    public List<CategoryDto> Categories { get; set; }

    public CreateModel(IServiceAppService serviceAppServices, ICategoryAppService categoryAppService)
    {
        _serviceAppService = serviceAppServices;
        _categoryAppService = categoryAppService;
    }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        Services = await _serviceAppService.GetAllServices(cancellationToken);
        Categories = await _categoryAppService.GetAllCategories(cancellationToken);
    }

    public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    {
        await _serviceAppService.CreateService(Service, cancellationToken);
        return RedirectToPage("Index");
    }
}