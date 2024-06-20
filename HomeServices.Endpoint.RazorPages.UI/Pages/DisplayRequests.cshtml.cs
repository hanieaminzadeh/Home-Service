using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

public class DisplayRequestsModel : PageModel
{
	private readonly IRequestAppService _requestAppService;
    private readonly IExpertAppService _expertAppService;

    public DisplayRequestsModel(IRequestAppService requestAppService,
        IExpertAppService expertAppService)
    {
		_requestAppService = requestAppService;
        _expertAppService = expertAppService;
    }

	[BindProperty]
	public ExpertProfileDto Expert { get; set; }

	[BindProperty]
	public List<RequestDto> Orders { get; set; }

	public async Task OnGet(CancellationToken cancellationToken)
	{
        //var expertId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userExpertId").Value);
        var applicationUserId = int.Parse(User.Claims.First().Value);
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

        Orders = await _requestAppService.GetExpertRequest(userId, cancellationToken);
	}
}
