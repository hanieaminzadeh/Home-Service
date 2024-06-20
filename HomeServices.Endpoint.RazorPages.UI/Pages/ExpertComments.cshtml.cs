using System.Reflection;
using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeServices.Endpoint.RazorPages.UI.Pages
{
    public class ExpertCommentsModel : PageModel
    {
        private readonly ICommentAppService _commentAppService;


        [BindProperty]
        public List<CommentDto> comments { get; set; }

        public ExpertCommentsModel(ICommentAppService commentAppService)
        {
            _commentAppService = commentAppService;
        }

        public async Task OnGet(int id, CancellationToken cancellationToken)
        {
            comments = await _commentAppService.GetAllCommentsByExpertId(id, cancellationToken);
        }
    }
}
