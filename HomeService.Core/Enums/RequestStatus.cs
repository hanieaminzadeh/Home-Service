using System.ComponentModel.DataAnnotations;

namespace HomeService.Core.Enums;

public enum RequestStatus
{
    [Display(Name = "سفارش شما ثبت شد")]
    Registered = 1,
    [Display(Name = "در انتظار تماس متخصص")]
    CheckingAndWaitingExpert = 2,
    [Display(Name = "سفارش شما توسط متخصص تایید شد")]
    RegisteredByExpert = 3,
    [Display(Name = "انجام شد")]
    Done = 4,
    [Display(Name = "رد شد")]
    Rejected = 5,
}