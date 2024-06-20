namespace HomeService.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public List<Service>? Services { get; set; }
    public bool IsDeleted { get; set; } 
    public string? ImgUrl { get; set; }
    public bool Active { get; set; } = false;
}
