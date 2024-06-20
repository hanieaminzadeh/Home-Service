using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.DTOs;
using HomeServices.Endpoint.RazorPages.UI.Infrastructure;
using HomeServices.Endpoint.RazorPages.UI.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;
    private readonly ICategoryAppService _categoryAppService;


    public IndexModel(ILogger<IndexModel> logger, IMemoryCache memoryCache, IDistributedCache distributedCache, ICategoryAppService categoryAppService)
    {
        _logger = logger;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _categoryAppService = categoryAppService;
    }
    public List<CategoryDto> categories { get; set; }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        //categories = await _categoryAppService.GetAllCategories(cancellationToken);
    }
}