using HomeService.Core.Entities;

namespace HomeService.Core.DTOs;

public class CreateCommentDto
{
    public string? CommentText { get; set; }
    public int ExpertId { get; set; }
    public int CustomerId { get; set; }
    public int? Score { get; set; }
}
