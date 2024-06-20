using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class CustomerAppService : ICustomerAppService
{
    #region Fields
    private readonly ICustomerService _customerService;
    #endregion

    #region Ctors
    public CustomerAppService(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    #endregion

    #region Implementations
    public async Task CreateCustomer(CustomerDto model, CancellationToken cancellationToken)
         => await _customerService.CreateCustomer(model, cancellationToken);

    public async Task<int> CountCustomers(CancellationToken cancellationToken)
        => await _customerService.CountCustomers(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _customerService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _customerService.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _customerService.DeActive(id, cancellationToken);
    }

    public async Task DeleteCustomer(CustomerDto model, CancellationToken cancellationToken)
    {
        await _customerService.DeleteCustomer(model, cancellationToken);
    }

    public async Task DeleteCustomerById(int id, CancellationToken cancellationToken)
    {
        await _customerService.DeleteCustomerById(id, cancellationToken);
    }

    public async Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken)
        => await _customerService.GetAllCustomers(cancellationToken);

    public async Task<Customer>? GetCustomerById(int id, CancellationToken cancellationToken)
        => await _customerService.GetCustomerById(id, cancellationToken);

    public async Task<CustomerProfileDto>? GetById(int? id, CancellationToken cancellationToken)
        => await _customerService.GetById(id, cancellationToken);

	public async Task UpdateCustomer(CustomerProfileDto model, CancellationToken cancellationToken)
    {
        await _customerService.UpdateCustomer(model, cancellationToken);
    }

    public async Task<int?> GetCustomerIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken)
        => await _customerService.GetCustomerIdByApplicationUserId(applicationUserId, cancellationToken);
    #endregion
}
