namespace HomeService.Core.DTOs;

public class CreateBidDto
{
    public int RequestId { get; set; }
    public int ExpertId { get; set; }
    public DateTime DateOfRegisteration { get; set; } = DateTime.Now;
    public int ProposedPrice { get; set; }
}
