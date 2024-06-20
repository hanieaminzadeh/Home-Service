namespace HomeService.Core.Entities;

public class Admin 
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public string? ProfileImgUrl { get; set; }
    public bool Active { get; set; } = false;
	public int ApplicationUserId { get; set; }
	public ApplicationUser ApplicationUser { get; set; }
}
