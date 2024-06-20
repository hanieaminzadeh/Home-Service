using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.Contracts.ServiceContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

[Authorize(Roles = "Customer")]
public class RequestModel : PageModel
{
	private readonly IRequestAppService _requestAppService;
	private readonly IServiceAppService _serviceAppService;
	private readonly ICustomerAppService _customerAppService;

	public RequestModel(IRequestAppService requestAppService,
		IServiceAppService serviceAppService,
		ICustomerAppService customerAppService)
	{
		_requestAppService = requestAppService;
		_serviceAppService = serviceAppService;
		_customerAppService = customerAppService;
	}

	[BindProperty]
	public RequestDto Order { get; set; }

	[BindProperty]
	public ServiceDto Service { get; set; }


	public async Task OnGet(int serviceId, CancellationToken cancellationToken)
	{
		Service = await _serviceAppService.GetService(serviceId, cancellationToken);
	}

	public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
	{
		//var userId = int.Parse(User.Claims.First(x => x.Type == "userCustomerId").Value);
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

		Order.CustomerId = userId;
		await _requestAppService.CreateRequest(Order, cancellationToken);
		return LocalRedirect("~/MyRequests");
	}
}
