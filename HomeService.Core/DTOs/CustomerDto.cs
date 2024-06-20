using HomeService.Core.Entities;

namespace HomeService.Core.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? BirthDate { get; set; }
    public string? ProfileImgUrl { get; set; }
    public List<Request>? Requests { get; set; }
    public string? Address { get; set; }
    public City? City { get; set; }
    public string? CardNumber { get; set; }
    public string? Description { get; set; }
    public DateTime CreatAt { get; set; } = DateTime.Now;
    public bool Active { get; set; } = false;
}
