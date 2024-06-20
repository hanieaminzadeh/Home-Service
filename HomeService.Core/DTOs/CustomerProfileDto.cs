using HomeService.Core.Entities;

namespace HomeService.Core.DTOs;

public class CustomerProfileDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileImgUrl { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set;}
    public string? Description { get; set; }
    public int CityId { get; set; }
    public City? City { get; set; }
    public List<Request> Requests { get; set; }
}
