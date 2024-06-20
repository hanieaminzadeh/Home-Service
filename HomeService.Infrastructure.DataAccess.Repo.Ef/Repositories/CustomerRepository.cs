using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class CustomerRepository : ICustomerRepository
{
    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<CustomerRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public CustomerRepository(AppDbContext context, ILogger<CustomerRepository> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task CreateCustomer(CustomerDto model, CancellationToken cancellationToken)
    {
        var newCustomer = new Customer()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            ProfileImgUrl = model.ProfileImgUrl,
            PhoneNumber = model.PhoneNumber,
            Requests = model.Requests,
            City = model.City,
            Address = model.Address,
            CardNumber = model.CardNumber,
            CreatAt = DateTime.Now,
        };
        try
        {
            await _context.Customers.AddAsync(newCustomer);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("customer not created Maybe it has already been added Or there is another error {exception}", ex);
        }
    }

    public async Task<int> CountCustomers(CancellationToken cancellationToken)
        => await _context.Customers.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
    => await _context.Customers.AnyAsync(c => c.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var customer = await FindCustomer(id, cancellationToken);
        customer.Active = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var customer = await FindCustomer(id, cancellationToken);

        customer.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCustomer(CustomerDto model, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(e => e.FirstName == model.FirstName ||
                                 e.LastName == model.LastName ||
                                 e.PhoneNumber == model.PhoneNumber, cancellationToken);

        if (customer == null) return;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCustomerById(int id, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (customer == null) return;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken)
    {
        var result = await _context.Customers
            .Select(model => new CustomerDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                ProfileImgUrl = model.ProfileImgUrl,
                PhoneNumber = model.PhoneNumber,
                Requests = model.Requests,
                City = model.City,
                Address = model.Address,
                CardNumber = model.CardNumber,
                CreatAt = DateTime.Now,
            }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Customer>? GetCustomerById(int id, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        return customer ?? new Customer();
    }

	public async Task<CustomerProfileDto>? GetById(int? id, CancellationToken cancellationToken)
	{
		var customer = await _context.Customers
            //.Where(x=> x.ApplicationUserId == id)
			.Select(model => new CustomerProfileDto
			{
				Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileImgUrl = model.ProfileImgUrl,
                Address = model.Address,
                City = model.City,
                Description = model.Description,
        PhoneNumber = model.PhoneNumber,
			}).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

		return customer ?? new CustomerProfileDto();
	}

    public async Task<int?> GetCustomerIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken)
    {
        var customerId = await _context.Customers
        .Where(c => c.ApplicationUserId == applicationUserId)
        .Select(c => c.Id)
        .FirstOrDefaultAsync(cancellationToken);

        return customerId;
    }

    public async Task UpdateCustomer(CustomerProfileDto model, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

        if (customer == null) return;

        try
        {
            customer.Id = model.Id;
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.ProfileImgUrl = model.ProfileImgUrl;
            customer.PhoneNumber = model.PhoneNumber;
            customer.Address = model.Address;
            customer.Description = model.Description;
            customer.CreatAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the customer Or there is another error {exception}", ex);
        }
    }
    #endregion

    #region PrivateMethods
    private async Task<Customer> FindCustomer(int id, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (customer != null)
        {
            return customer;
        }

        throw new Exception($"customer with id {id} not found");
    }

    #endregion
}
