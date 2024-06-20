using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

public class ListingServicesModel : PageModel
{
    private readonly IServiceAppService _serviceAppService;

    public List<ServiceDto> Services { get; set; }

    public ListingServicesModel(IServiceAppService serviceAppService)
    {
        _serviceAppService = serviceAppService;
    }
    public async Task OnGet(int categoryId, CancellationToken cancellationToken)
    {
        Services = await _serviceAppService.GetServicesByCategoryId(categoryId, cancellationToken);
    }
}
