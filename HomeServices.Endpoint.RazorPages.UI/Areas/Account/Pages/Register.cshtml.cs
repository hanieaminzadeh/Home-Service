using HomeService.Core.Contracts.ApplicationUser;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HomeServices.Endpoint.RazorPages.UI.Areas.Account.Pages;

public class RegisterModel : PageModel
{
    private readonly IAccountAppService _accountAppServices;

    public RegisterModel(IAccountAppService accountAppServices)
    {
        _accountAppServices = accountAppServices;
    }

    [BindProperty]
    public ApplicationUserDto Input { get; set; }

    public async Task OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken, string returnUrl = null)
    {
        if (Input.Role == "Customer" && Input.Role == "Expert")
        {
            ModelState.AddModelError(string.Empty, "همزمان نمیتوانید هم مشتری باشید هم کارشناس");
            return Page();
        }

        returnUrl ??= Url.Content("~/");

        var result = await _accountAppServices.Register(Input, cancellationToken);
        if (result.Count == 0)
        {
            return LocalRedirect(returnUrl);
        }

        foreach (var error in result)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return Page();

    }
}
