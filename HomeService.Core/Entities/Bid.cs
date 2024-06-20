namespace HomeService.Core.Entities;

public class Bid
{
    public int Id { get; set; }
    public int? RequestId { get; set; }
    public Request? Request { get; set; }
    public int? ExpertId { get; set; }
    public Expert? Expert { get; set; }
    public DateTime DateOfRegisteration { get; set; } = DateTime.Now;
    public DateTime? DateOfWork { get; set; }
    public int? ProposedPrice { get; set; }
    public bool? IsWinner { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public bool Active { get; set; } = false;
}


