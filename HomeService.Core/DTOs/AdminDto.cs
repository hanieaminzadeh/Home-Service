namespace HomeService.Core.DTOs;

public class AdminDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public string? ProfileImgUrl { get; set; }
    public bool Active { get; set; } = false;
}
