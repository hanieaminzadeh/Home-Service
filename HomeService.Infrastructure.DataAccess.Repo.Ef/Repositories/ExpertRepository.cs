using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;


public class ExpertRepository : IExpertRepository
{
    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<ExpertRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    #endregion

    #region Ctors
    public ExpertRepository(AppDbContext context, ILogger<ExpertRepository> logger, IMemoryCache memoryCache)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    #endregion

    #region Implementations
    public async Task CreateExpert(ExpertDto model, CancellationToken cancellationToken)
    {
        var newExpert = new Expert()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            BirthDate = model.BirthDate,
            ProfileImgUrl = model.ProfileImgUrl,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            City = model.City,
            CardNumber = model.CardNumber,
            ShebaNumber = model.ShebaNumber,
            Services = model.Services,
            CreateAt = DateTime.Now,
        };
        try
        {
            await _context.Experts.AddAsync(newExpert);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Expert not created Maybe it has already been added Or there is another error {exception}", ex);
        }
    }

    public async Task<int> CountExperts(CancellationToken cancellationToken)
        => await _context.Experts.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
    => await _context.Experts.AnyAsync(e => e.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var expert = await FindExpert(id, cancellationToken);
        expert.Active = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var expert = await FindExpert(id, cancellationToken);

        expert.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteExpert(ExpertDto model, CancellationToken cancellationToken)
    {
        var expert = await _context.Experts
            .FirstOrDefaultAsync(e => e.FirstName == model.FirstName ||
                                 e.LastName == model.LastName ||
                                 e.PhoneNumber == model.PhoneNumber, cancellationToken);

        if (expert == null) return;

        _context.Experts.Remove(expert);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteExpertById(int id, CancellationToken cancellationToken)
    {
        var expert = await _context.Experts
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (expert == null) return;

        _context.Experts.Remove(expert);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ExpertDto>> GetAllExperts(CancellationToken cancellationToken)
    {
        var result = await _context.Experts
            .Select(model => new ExpertDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                ProfileImgUrl = model.ProfileImgUrl,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                Address = model.Address,
                CardNumber = model.CardNumber,
                Services = model.Services,
                Bids = model.Bids,
                CreateAt = DateTime.Now,
            }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<Expert>? GetExpertById(int id, CancellationToken cancellationToken)
    {
        var expert = await _context.Experts
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        return expert ?? new Expert();
    }

    public async Task<ExpertProfileDto>? GetById(int? id, CancellationToken cancellationToken)
    {
        var expert = await _context.Experts
            //.Where(x => x.ApplicationUserId == id)
            .Select(model => new ExpertProfileDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ProfileImgUrl = model.ProfileImgUrl,
                Address = model.Address,
                City = model.City,
                PhoneNumber = model.PhoneNumber,
                Description = model.Description,
                ServiceIds = model.Services.Select(model => model.Id).ToList(),
            }).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return expert ?? new ExpertProfileDto();
    }

    public async Task<int?> GetExpertIdByApplicationUserId(int? applicationUserId, CancellationToken cancellationToken)
    {
        var expertId = await _context.Experts
        .Where(c => c.ApplicationUserId == applicationUserId)
        .Select(c => c.Id)
        .FirstOrDefaultAsync(cancellationToken);

        return expertId;
    }

    //public async Task UpdateExpert(ExpertDto model, CancellationToken cancellationToken)
    //{
    //    var expert = await _context.Experts
    //        .FirstOrDefaultAsync(e => e.Id == model.Id, cancellationToken);

    //    if (expert == null) return;

    //    try
    //    {
    //        expert.FirstName = model.FirstName;
    //        expert.LastName = model.LastName;
    //        expert.BirthDate = model.BirthDate;
    //        expert.ProfileImgUrl = model.ProfileImgUrl;
    //        expert.PhoneNumber = model.PhoneNumber;
    //        expert.CityId = model.CityId;
    //        expert.City = model.City;
    //        expert.Address = model.Address;
    //        expert.CardNumber = model.CardNumber;
    //        expert.ShebaNumber = model.ShebaNumber;
    //        expert.Services = model.Services;
    //        expert.Bids = model.Bids;
    //        model.CreateAt = DateTime.Now;

    //        await _context.SaveChangesAsync(cancellationToken);
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError("There is probably an error in the changes applied to the expert Or there is another error {exception}", ex);
    //    }
    //}

    public async Task UpdateExpert(ExpertProfileDto model, CancellationToken cancellationToken)
    {
        var expert = await _context.Experts
            .Include(s => s.Services)
            .FirstOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

        if (expert != null)
        {

            expert.Services ??= new List<Service>();

            expert.Services.Clear();

            if (model.ServiceIds is not null)
            {
                foreach (var serviceId in model.ServiceIds)
                {
                    var service = await _context.Services
                        .FirstOrDefaultAsync(s => s.Id == serviceId, cancellationToken);

                    expert.Services.Add(service);
                }
            }

            expert.FirstName = model.FirstName;
            expert.LastName = model.LastName;
            expert.ProfileImgUrl = model.ProfileImgUrl;
            expert.PhoneNumber = model.PhoneNumber;
            expert.CityId = model.CityId;
            expert.City = model.City;
            expert.Address = model.Address;
            expert.CardNumber = model.CardNumber;
            expert.ShebaNumber = model.ShebaNumber;
            expert.Bids = model.Bids;
            expert.Description = model.Description;
            model.CreateAt = DateTime.Now;
        }
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region PrivateMethods
    private async Task<Expert> FindExpert(int id, CancellationToken cancellationToken)
    {
        var expert = await _context.Experts
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (expert != null)
        {
            return expert;
        }

        throw new Exception($"expert with id {id} not found");
    }

    #endregion
}