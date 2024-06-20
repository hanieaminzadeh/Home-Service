using HomeService.Core.Enums;

namespace HomeService.Core.DTOs;

public class ChangeStatusDto
{
    public int Id { get; set; }
    public RequestStatus Status { get; set; }
}
