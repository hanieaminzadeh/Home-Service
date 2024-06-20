using HomeService.Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace HomeService.Core.Entities;

public class User
{
    public int Id { get; set; }
    public int? CityId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ShebaNumber { get; set; }
    public string? CardNumber { get; set; }
    public string? Address { get; set; }
    public int? Score { get; set; }
    public IFormFile? ProfileImgUrl { get; set; }
    public List<string>? Role { get; set; }
    public List<ServiceDto>? Services { get; set; }
    public List<City>? Cities { get; set; }
}
