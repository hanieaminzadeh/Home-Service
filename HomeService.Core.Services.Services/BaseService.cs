using HomeService.Core.Contracts.BaseService;
using Microsoft.AspNetCore.Http;

namespace HomeService.Core.Services.Services;

public class BaseService : IBaseService
{
    public async Task<string> UploadImage(IFormFile image)
    {
        if (image != null && image.Length > 0)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return "/uploads/" + uniqueFileName;
        }
        return null;
    }
}
