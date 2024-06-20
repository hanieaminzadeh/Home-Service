using HomeService.Core.Contracts.RequestContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class RequestRepository : IRequestRepository
{

    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<RequestRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public RequestRepository(AppDbContext context, ILogger<RequestRepository> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task<int> CreateRequest(RequestDto model, CancellationToken cancellationToken)
    {
        var newRequest = new Request()
        {
            Customer = model.Customer,
            DateOfRegisteration = DateTime.Now,
            DateOfImplemention = model.DateOfImplemention,
            Status = model.Status,
            Description = model.Description,
            CustomerId = model.CustomerId,
            ServiceId = model.ServiceId,
        };
        await _context.Requests.AddAsync(newRequest);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Request Added Succesfully");
        return newRequest.Id;

    }

    public async Task<int> CountRequests(CancellationToken cancellationToken)
        => await _context.Requests.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _context.Requests.AnyAsync(r => r.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var request = await FindRequest(id, cancellationToken);
        request.Active = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var request = await FindRequest(id, cancellationToken);

        request.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRequestById(int id, CancellationToken cancellationToken)
    {
        var request = await _context.Requests
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (request == null) return;

        _context.Requests.Remove(request);

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is a problem deleting the Request {Exception}", ex);
        }
    }

    public async Task<List<RequestDto>> GetAllRequests(CancellationToken cancellationToken)
    {
        var result = await _context.Requests
            .Select(model => new RequestDto
            {
                Id = model.Id,
                Customer = model.Customer,
                CustomerId = model.CustomerId,
                DateOfRegisteration = DateTime.Now,
                DateOfImplemention = model.DateOfImplemention,
                Status = model.Status,
                Description = model.Description,
                ServiceId = model.ServiceId,
                Service = model.Service,
                Bids = model.Bids,
                IsDeleted = model.IsDeleted,
            }).ToListAsync(cancellationToken);

        return result;
    }

    //public async Task<Request>? GetRequestById(int id, CancellationToken cancellationToken)
    //{
    //    var request = await _context.Requests
    //        .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    //    return request ?? new Request();
    //}
    public async Task<RequestDto> GetRequestById(int requestId, CancellationToken cancellationToken)
    {

        var request = await _context.Requests
                .Include(r => r.Bids)
                .Include(r => r.Customer)
                .ThenInclude(r => r.ApplicationUser)
                .Include(r => r.Service)
            .Select(r => new RequestDto
            {
                Id = r.Id,
                Customer = r.Customer,
                Service = r.Service,
                ServiceName = r.Service.Title,
                Bids = r.Bids,
                Status = r.Status,
            }).FirstOrDefaultAsync(a => a.Id == requestId, cancellationToken);

        if (request != null)
        {
            _logger.LogInformation("requestDto returned from database, and cached in memory successfully.");
            return request;
        }
        else
        {
            _logger.LogError("We expected the requestDto to return from the database, but it returned null.");
            throw new Exception("Something wents wrong!, please try again.");
        }
        _logger.LogInformation("requestDto returned from InMemoryCache.");
        return request;
    }


    public async Task<List<Request>> GetRequestByCustomerId(int? customerId, CancellationToken cancellationToken)
    {
        var requests = await _context.Requests
            .Where(x => x.CustomerId == customerId)
            .Include(x => x.Service)
            .Include(x => x.Bids)
        //.ThenInclude(x => x.Except)
        .ToListAsync(cancellationToken);

        return requests;
    }

    public async Task UpdateRequest(RequestDto model, CancellationToken cancellationToken)
    {
        var request = await _context.Requests
            .FirstOrDefaultAsync(e => e.Id == model.Id, cancellationToken);

        if (request == null) return;

        try
        {
            request.Customer = model.Customer;
            request.DateOfImplemention = model.DateOfImplemention;
            request.DateOfImplemention = model.DateOfImplemention;
            request.Status = model.Status;
            request.Description = model.Description;
            request.Service = model.Service;
            request.Bids = model.Bids;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the request Or there is another error {Exception}", ex);
        }
    }

    public async Task<ChangeStatusDto> ChangeRequestStatus(ChangeStatusDto status, CancellationToken cancellationToken)
    {
        var request = await FindRequest(status.Id, cancellationToken);
        request.Status = status.Status;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            return status;
        }
        catch (Exception ex)
        {
            _logger.LogInformation("There is a problem change the Request {Exception}", ex);
            throw ex;
        }
    }

    public async Task<List<RequestDto>> GetExpertRequest(int? expertId, CancellationToken cancellationToken)
    {
        var expertServices = await GetExpertServices(expertId, cancellationToken);

        var requestForExpert = new List<RequestDto>();

        foreach (var expertSkillId in expertServices.ServiceIds)
        {
            var record = await _context.Requests
                .Where(r => r.ServiceId == expertSkillId)
                .Include(r => r.Service)

                .Select(r => new RequestDto()
                {
                    Id = r.Id,
                    ServiceName = r.Service.Title,
                    IsDeleted = r.IsDeleted,
                    Status = r.Status,
                }).ToListAsync(cancellationToken);
            requestForExpert.AddRange(record);
        }
        return requestForExpert;
    }

    #endregion

    #region PrivateMethods
    private async Task<Request> FindRequest(int id, CancellationToken cancellationToken)
    {
        var request = await _context.Requests
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        if (request != null)
        {
            return request;
        }

        throw new Exception($"Request with id {id} not found");
    }

    private async Task<ExpertServicesDto> GetExpertServices(int? expertId, CancellationToken cancellationToken)
    {
        var expertServices = await _context.Experts
            .Select(e => new ExpertServicesDto
            {
                Id = e.Id,
                ServiceIds = e.Services.Select(s => s.Id).ToList()
            }).FirstOrDefaultAsync(e => e.Id == expertId, cancellationToken);

        return expertServices;
    }

    #endregion
}