using HomeService.Core.Enums;

namespace HomeService.Core.Entities;

public class Request
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public DateTime DateOfRegisteration { get; set; } = DateTime.Now;
    public DateTime? DateOfImplemention { get; set; }
    public RequestStatus? Status { get; set; }
    public string? Description { get; set; }
    public int? ServiceId { get; set; }
    public Service? Service { get; set; }
    public List<Bid>? Bids { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool Active { get; set; } = false;
}
