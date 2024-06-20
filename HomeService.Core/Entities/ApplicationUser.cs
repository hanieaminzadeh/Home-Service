using Microsoft.AspNetCore.Identity;

namespace HomeService.Core.Entities;

public class ApplicationUser : IdentityUser<int>
{
    public Admin? Admin { get; set; }
    public Customer? Customer { get; set; }
    public Expert? Expert { get; set; }
    public string? FullName { get; set; }
    public bool IsAccept { get; set; } = false;
}
