using HomeService.Core.Contracts.ApplicationUser;
using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace HomeService.Core.Services.AppServices;

public class AccountAppService : IAccountAppService
{
	#region Fileds

	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly ICustomerService _customerService;
	private readonly IExpertService _expertService;

    #endregion


    #region Ctor
    public AccountAppService(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        ICustomerService customerService,
        IExpertService expertService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _customerService = customerService;
        _expertService = expertService;
    }

    #endregion


    #region Implementations

    public async Task<bool> Login(ApplicationUserLoginDto model, CancellationToken cancellationToken)
	{
		var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, lockoutOnFailure: false);

		return result.Succeeded;
	}

	public async Task<List<IdentityError>> Register(ApplicationUserDto model, CancellationToken cancellationToken)
	{
		var role = string.Empty;

		var user = new ApplicationUser();

		if (model.Role == "Expert")
		{
			role = "Expert";

            user = new ApplicationUser
			{
				Email = model.Email,
				UserName = model.Email,
				Expert = new Expert()
				{
					FirstName = model.FirstName,
					LastName = model.LastName ,
				}
			};
		}

		if (model.Role == "Customer")
		{
			role = "Customer";

            user = new ApplicationUser
			{

				Email = model.Email,
				UserName = model.Email,
				Customer = new Customer()
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
				}
			};
		}
		var result = await _userManager.CreateAsync(user, model.Password);

        if (model.Role == "Customer")
        {
            var userCustomerId = user.Customer!.Id;
            await _userManager.AddClaimAsync(user, new Claim("userCustomerId", userCustomerId.ToString()));
        }

        if (model.Role == "Expert")
        {
            var userExpertId = user.Expert!.Id;
            await _userManager.AddClaimAsync(user, new Claim("userExpertId", userExpertId.ToString()));
        }

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, role);

        return (List<IdentityError>)result.Errors;
	}


	public async Task<CustomerProfileDto> GetCustomer(int? id, int applicationUserId, CancellationToken cancellationToken)
	{
		var result = await _userManager.Users
			.Select(model => new CustomerProfileDto
			{
				Id = model.Id,
				UserName = model.UserName,
				Email = model.Email,
			}).FirstOrDefaultAsync(x => x.Id == applicationUserId, cancellationToken);

		var result2 = await _customerService.GetById(id, cancellationToken);

		var profileDto = new CustomerProfileDto();

		profileDto.Id = result2.Id;
		profileDto.FirstName = result2.FirstName;
		profileDto.LastName = result2.LastName;
		profileDto.ProfileImgUrl = result2.ProfileImgUrl;
		profileDto.Address = result2.Address;
		profileDto.City = result2.City;
		profileDto.PhoneNumber = result2.PhoneNumber;
		profileDto.Email = result.Email;
		profileDto.UserName = result.UserName;
		profileDto.Description = result2.Description;

		return profileDto;
	}

    public async Task<ExpertProfileDto> GetExpert(int? id, int applicationUserId, CancellationToken cancellationToken)
    {
        var result = await _userManager.Users
            .Select(model => new ExpertProfileDto
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
            }).FirstOrDefaultAsync(x => x.Id == applicationUserId, cancellationToken);

        var result2 = await _expertService.GetById(id, cancellationToken);

        var profileDto = new ExpertProfileDto();

        profileDto.Id = result2.Id;
        profileDto.FirstName = result2.FirstName;
        profileDto.LastName = result2.LastName;
        profileDto.ProfileImgUrl = result2.ProfileImgUrl;
        profileDto.Address = result2.Address;
        profileDto.City = result2.City;
        profileDto.PhoneNumber = result2.PhoneNumber;
        profileDto.Email = result.Email;
        profileDto.UserName = result.UserName;
		profileDto.ServiceIds = result2.ServiceIds;
		profileDto.Description = result2.Description;

        return profileDto;
    }

    #endregion


    #region PrivateMethode
    private ApplicationUser CreateUser()
	{
		try
		{
			return Activator.CreateInstance<ApplicationUser>();
		}
		catch
		{
			throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
												$"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
												$"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
		}
	}

	public bool IsRole(string role)
	{
		throw new NotImplementedException();
	}
	#endregion
}
