using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

[Authorize(Roles = "Customer,Expert")]
public class MyRequestsModel : PageModel
{
	private readonly IRequestAppService _requestAppService;
	private readonly ICustomerAppService _customerAppService;

	public MyRequestsModel(IRequestAppService requestAppService,
		ICustomerAppService customerAppService)
	{
		_requestAppService = requestAppService;
		_customerAppService = customerAppService;
	}

	[BindProperty]
	public List<Request> Orders { get; set; }

	public async Task OnGet(CancellationToken cancellationToken)
    {
		var applicationUserId = int.Parse(User.Claims.First().Value);

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
		//var userId = int.Parse(User.Claims.First(x=>x.Type == "userCustomerId").Value);
		Orders = await _requestAppService.GetRequestByCustomerId(userId, cancellationToken);
    }
}
