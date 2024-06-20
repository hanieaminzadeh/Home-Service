namespace HomeService.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string? CommentText { get; set; }
    public DateTime CreatAt { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int ExpertId { get; set; }
    public Expert Expert { get; set; }
    public int? Score { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool Active { get; set; } = false;
}
