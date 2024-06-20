using HomeService.Core.Entities;

namespace HomeService.Core.DTOs;

public class CityDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Customer>? Customers { get; set; }
    public List<Expert>? Experts { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public bool? Active { get; set; } = false;
}
