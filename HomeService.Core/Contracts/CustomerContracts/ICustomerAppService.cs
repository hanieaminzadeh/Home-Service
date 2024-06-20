using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Contracts.CustomerContracts;

public interface ICustomerAppService
{
    Task CreateCustomer(CustomerDto model, CancellationToken cancellationToken);
    Task<int> CountCustomers(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task DeleteCustomer(CustomerDto model, CancellationToken cancellationToken);
    Task DeleteCustomerById(int id, CancellationToken cancellationToken);
    Task<List<CustomerDto>> GetAllCustomers(CancellationToken cancellationToken);
    Task<CustomerProfileDto>? GetById(int? id, CancellationToken cancellationToken);
	Task<Customer>? GetCustomerById(int id, CancellationToken cancellationToken);
    Task UpdateCustomer(CustomerProfileDto model, CancellationToken cancellationToken);
    Task<int?> GetCustomerIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken);
}
