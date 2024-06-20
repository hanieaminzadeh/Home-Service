using HomeService.Core.Contracts.BidContracts;
using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages;

[Authorize(Roles = "Customer")]
public class BidModel : PageModel
{
    private readonly IBidAppService _bidAppService;
    private readonly IRequestAppService _requestAppService;

    public BidModel(IBidAppService bidAppService, IRequestAppService requestAppService)
    {
		_bidAppService = bidAppService;
	}

    [BindProperty]
    public ChangeStatusDto NewStatus { get; set; }


    public List<BidDto> Bids { get; set; }

    public async Task OnGet(int orderId, CancellationToken cancellationToken)
    {
        Bids = await _bidAppService.GetBidsByOrderId(orderId, cancellationToken);
    }

    //public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
    //{
    //    await _requestAppService.ChangeRequestStatus(NewStatus, cancellationToken);
    //    return RedirectToPage("Index");
    //} 

    public async Task<IActionResult> OnGetActive(int id, CancellationToken cancellationToken)
	{
		await _bidAppService.Active(id, cancellationToken);
		return RedirectToPage("Index");
	}

	public async Task<IActionResult> OnGetDeActive(int id, CancellationToken cancellationToken)
	{
		await _bidAppService.DeActive(id, cancellationToken);
		return RedirectToPage("Index");
	}

    public async Task<IActionResult> OnPostDone(int bidId, CancellationToken cancellationToken)
    {
        await _bidAppService.DoneRequest(bidId, cancellationToken);
        return RedirectToPage("Index");
    }
}
