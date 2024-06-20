using HomeService.Core.Contracts.ApplicationUser;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Account.Pages;

public class LoginModel : PageModel
{

    private readonly IAccountAppService _applicationUserAppService;

    public LoginModel(IAccountAppService applicationUserAppService)
    {
        _applicationUserAppService = applicationUserAppService;
    }

    [BindProperty]
    public ApplicationUserLoginDto Input { get; set; }

    public async Task OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken, string returnUrl = null)
    {

        returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid) return Page();

        var succeededLogin = await _applicationUserAppService.Login(Input, cancellationToken);

        if (succeededLogin)
            return LocalRedirect(returnUrl);

        else
        {
            ModelState.AddModelError(string.Empty, "نام کاربری یا کلمه عبور اشتباه است");
            return Page();
        }
    }
}
