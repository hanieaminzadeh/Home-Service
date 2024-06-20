namespace HomeService.Core.Entities;

public class City
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public List<Customer>? Customers { get; set; }
    public List<Expert>? Experts { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public bool Active { get; set; } = false;
}


