using HomeService.Core.Contracts.CustomerContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class CustomerService : ICustomerService
{
    #region Fields
    private readonly ICustomerRepository _customerRepository;
    #endregion

    #region Ctors
    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateCustomer(CustomerDto model, CancellationToken cancellationToken)
        => await _customerRepository.CreateCustomer(model, cancellationToken);

    public async Task<int> CountCustomers(CancellationToken cancellationToken)
        => await _customerRepository.CountCustomers(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _customerRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _customerRepository.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _customerRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteCustomer(CustomerDto model, CancellationToken cancellationToken)
    {
        await _customerRepository.DeleteCustomer(model, cancellationToken);
    }

    public async Task DeleteCustomerById(int id, CancellationToken cancellationToken)
    {
        await _customerRepository.DeleteCustomerById(id, cancellationToken);
    }

    public async Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken)
        => await _customerRepository.GetAllCustomers(cancellationToken);

    public async Task<Customer>? GetCustomerById(int id, CancellationToken cancellationToken)
        => await _customerRepository.GetCustomerById(id, cancellationToken);

    public async Task<CustomerProfileDto>? GetById(int? id, CancellationToken cancellationToken)
        => await _customerRepository.GetById(id, cancellationToken);

	public async Task UpdateCustomer(CustomerProfileDto model, CancellationToken cancellationToken)
    {
        await _customerRepository.UpdateCustomer(model, cancellationToken);
    }

    public async Task<int?> GetCustomerIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken)
        => await _customerRepository.GetCustomerIdByApplicationUserId(applicationUserId, cancellationToken);

    #endregion
}
