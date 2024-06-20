using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

[Authorize(Roles = "Customer,Expert")]
public class CommentModel : PageModel
{

    private readonly ICommentAppService _commentAppService;
    private readonly IExpertAppService _expertAppService;
    private readonly ICustomerAppService _customerAppService;

    public CommentModel(ICommentAppService commentAppService,
        IExpertAppService expertAppService,
        ICustomerAppService customerAppService)
    {
        _commentAppService = commentAppService;
        _expertAppService = expertAppService;
        _customerAppService = customerAppService;
    }

    [BindProperty]
    public CreateCommentDto Comment { get; set; }

    [BindProperty]
    public Expert Expert { get; set; }

    public async Task OnGet(int id, CancellationToken cancellationToken)
    {
        Expert = await _expertAppService.GetExpertById(id, cancellationToken);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
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
                userId = await _customerAppService.GetCustomerIdByApplicationUserId(applicationUserId, cancellationToken);
            }
            Comment.CustomerId = userId.Value;
        }
        await _commentAppService.CreateCommentDto(Comment, cancellationToken);
        return LocalRedirect("~/Index");
    }
}
