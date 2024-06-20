namespace HomeService.Core.Entities;

public class Customer
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BirthDate { get; set; }
    public string? ProfileImgUrl { get; set; }
    public List<Request>? Requests { get; set; }
    public string? Address { get; set; }
    public int? CityId { get; set; }
    public City? City { get; set; }
    public string? CardNumber { get; set; }
    public DateTime CreatAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public bool Active { get; set; } = false;
    public string? Description { get; set; }
    public List<Comment>? Comments { get; set; }
	public int ApplicationUserId { get; set; }
	public ApplicationUser ApplicationUser { get; set; }
}
