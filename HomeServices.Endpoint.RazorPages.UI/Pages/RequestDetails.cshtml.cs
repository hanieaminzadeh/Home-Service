using HomeService.Core.Contracts.BidContracts;
using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

public class RequestDetailsModel : PageModel
{
    private readonly IRequestAppService _requestAppService;
    private readonly IBidAppService _bidAppService;
    private readonly IExpertAppService _expertAppService;

    public RequestDetailsModel(IRequestAppService requestAppService,
        IBidAppService bidAppService,
        IExpertAppService expertAppService)
    {
        _requestAppService = requestAppService;
        _bidAppService = bidAppService;
        _expertAppService = expertAppService;
    }

    public RequestDto Order { get; set; }

    [BindProperty]
    public int ExpertId { get; set; }

    public async Task OnGet(int orderId, CancellationToken cancellationToken)
    {
        Order = await _requestAppService.GetRequestById(orderId, cancellationToken);

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
        ExpertId = userId.Value;
    }

    public async Task<IActionResult> OnGetCreateBid(int id, CancellationToken cancellationToken)
    {
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

        var bidModel = new CreateBidDto()
        {
            RequestId = id,
            ExpertId = userId.Value,
        };
        await _bidAppService.CreateBidByRequestId(bidModel, cancellationToken);
        return LocalRedirect("~/DisplayRequests");
    }
}
