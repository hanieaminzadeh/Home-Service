using Microsoft.AspNetCore.Http;

namespace HomeService.Core.Contracts.BaseService;

public interface IBaseService
{
    Task<string> UploadImage(IFormFile image);
}
