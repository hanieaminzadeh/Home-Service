using Microsoft.AspNetCore.Http;

namespace HomeService.Core.Contracts.BaseService;

public interface IBaseAppService
{
    Task<string> UploadImage(IFormFile image);
}
