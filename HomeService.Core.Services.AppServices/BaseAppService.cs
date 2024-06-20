using HomeService.Core.Contracts.BaseService;
using Microsoft.AspNetCore.Http;

namespace HomeService.Core.Services.AppServices;

public class BaseAppService : IBaseAppService
{

    private readonly IBaseService _baseService;

    public BaseAppService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<string> UploadImage(IFormFile image)
    {
        return await _baseService.UploadImage(image);
    }
}
