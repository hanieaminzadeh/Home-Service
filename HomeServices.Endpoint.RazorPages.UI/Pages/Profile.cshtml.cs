using HomeService.Core.Contracts.ApplicationUser;
using HomeService.Core.Contracts.BaseService;
using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Core.Services.AppServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

[Authorize(Roles = "Customer,Expert")]
public class ProfileModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAccountAppService _accountAppServices;
    private readonly IBaseAppService _baseAppServices;
    private readonly ICustomerAppService _customerAppServices;
    private readonly IExpertAppService _expertAppService;
    private readonly IServiceAppService _serviceAppService;

    [BindProperty]
    public IFormFile Image { get; set; }


    [BindProperty]
    public CustomerProfileDto Customer { get; set; }

    [BindProperty]
    public ExpertProfileDto Expert { get; set; }


    public List<ServiceDto> Services { get; set; }

    public ProfileModel(UserManager<ApplicationUser> userManager,
        IAccountAppService accountAppServices,
        IBaseAppService baseAppServices,
        ICustomerAppService customerAppServices,
        IExpertAppService expertAppService,
        IServiceAppService serviceAppService)
    {
        _userManager = userManager;
        _accountAppServices = accountAppServices;
        _baseAppServices = baseAppServices;
        _customerAppServices = customerAppServices;
        _expertAppService = expertAppService;
        _serviceAppService = serviceAppService;
    }

    public async Task OnGet(CancellationToken cancellationToken)
    {
        var applicationUserId = int.Parse(User.Claims.First().Value);

        if (User.IsInRole("Customer"))
        {
            int? userId;

            var user = User.Claims.FirstOrDefault(c => c.Type == "userCustomerId");
            if (user != null)
            {
                userId = int.Parse(user.Value);
            }
            else
            {
                userId = await _customerAppServices.GetCustomerIdByApplicationUserId(applicationUserId, cancellationToken);
            }
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userCustomerId").Value);
            TempData["CustomerId"] = userId;
            Customer = await _accountAppServices.GetCustomer(userId, applicationUserId, cancellationToken);
        }
        if (User.IsInRole("Expert"))
        {
            Services = await _serviceAppService.GetAllServices(cancellationToken);
            int? userId;

            var user = User.Claims.FirstOrDefault(c => c.Type == "userExpertId");
            if (user != null)
            {
                userId = int.Parse(user.Value);
            }
            else
            {
                userId = await _expertAppService.GetExpertIdByApplicationUserId(applicationUserId, cancellationToken);
            }
            //int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userExpertId").Value);
            TempData["ExpertId"] = userId;
            Expert = await _accountAppServices.GetExpert(userId, applicationUserId, cancellationToken);
        }
    }

    public async Task<IActionResult> OnPostUpdateCustomer(IFormFile image, CancellationToken cancellationToken)
    {
        if (image != null)
        {
            var imageUrl = await _baseAppServices.UploadImage(image);
            Customer.ProfileImgUrl = imageUrl;
        }
        await _customerAppServices.UpdateCustomer(Customer, cancellationToken);
        return LocalRedirect("/Profile");
    }

    public async Task<IActionResult> OnPostUpdateExpert(IFormFile image, CancellationToken cancellationToken)
    {
        if (image != null)
        {
            var imageUrl = await _baseAppServices.UploadImage(image);
            Expert.ProfileImgUrl = imageUrl;
        }
        await _expertAppService.UpdateExpert(Expert, cancellationToken);
        return LocalRedirect("/Profile");
    }
}



















//public async Task<IActionResult> OnPostExpert(ExpertDto expertDto, IFormFile image, CancellationToken cancellationToken)
//{
//    var imageUrl = await _baseAppServices.UploadImage(image);
//    expertDto.ProfileImgUrl = imageUrl;
//    await _expertAppService.CreateExpert(expertDto, cancellationToken);
//    return LocalRedirect("/Profile");
//}

//public async Task<IActionResult> OnPostCustomer(CustomerDto customerDto, IFormFile image, CancellationToken cancellationToken)
//{
//    var imageUrl = await _baseAppServices.UploadImage(image);
//    customerDto.ProfileImgUrl = imageUrl;
//    await _customerAppServices.CreateCustomer(customerDto, cancellationToken);
//    return LocalRedirect("/Profile");
//}
