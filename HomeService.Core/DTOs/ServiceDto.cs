using HomeService.Core.Entities;

namespace HomeService.Core.DTOs;

public class ServiceDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? Description { get; set; }
    public int? Price { get; set; }
    public DateTime CreatAt { get; set; } = DateTime.Now;
    public List<Expert>? Experts { get; set; }
    public List<Request>? Requests { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? ImgUrl { get; set; }
    public bool Active { get; set; } = false;
}
