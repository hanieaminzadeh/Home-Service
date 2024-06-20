using HomeService.Core.Entities;

namespace HomeService.Core.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public List<Service>? Services { get; set; }
    public string? ImgUrl { get; set; }
    public bool Active { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
}
