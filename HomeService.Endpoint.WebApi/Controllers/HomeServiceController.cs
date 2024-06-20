using HomeService.Core.Contracts.ApplicationUser;
using HomeService.Core.Contracts.CategoryContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HomeService.Endpoint.WebApi.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class HomeServiceController : ControllerBase
{
    private readonly ICategoryAppService _categoryAppService;
    private readonly IRequestAppService _requestAppService;
    private readonly IAccountAppService _accountAppService;

    public HomeServiceController(ICategoryAppService categoryAppService,
        IRequestAppService requestAppService,
        IAccountAppService accountAppService)
    {
        _categoryAppService = categoryAppService;
        _requestAppService = requestAppService;
        _accountAppService = accountAppService;
    }

    [HttpGet]
    [Route(nameof(GetCategories))]
    public async Task<List<CategoryDto>> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await _categoryAppService.GetAllCategories(cancellationToken);
        return categories;
    }


    [HttpGet]
    [Route(nameof(GetRequests))]
    public async Task<List<RequestDto>> GetRequests(CancellationToken cancellationToken)
    {
        var requests = await _requestAppService.GetAllRequests(cancellationToken);
        return requests;
    }


    [HttpPost]
    [Route(nameof(RegisterUser))]
    public async Task<string> RegisterUser(ApplicationUserDto input, CancellationToken cancellationToken)
    {
        var result = await _accountAppService.Register(input, cancellationToken);
        if (result.Count == 0)
        {
            return "ثبت نام با موفقیت انجام شد";
        }
        else
        {
            return "خطا در ثبت نام";
        }
    }
}
