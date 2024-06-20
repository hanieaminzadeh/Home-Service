using HomeService.Core.Contracts.BidContracts;
using HomeService.Core.Contracts.ExpertContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class BidRepository : IBidRepository
{
    #region Fields
    private readonly AppDbContext _context;
    private readonly ILogger<BidRepository> _logger;
    private readonly IMemoryCache _memoryCache;
    private readonly IExpertRepository _expertRepository;
    #endregion

    #region Ctors
    public BidRepository(AppDbContext context, ILogger<BidRepository> logger, IMemoryCache memoryCache, IExpertRepository _expertRepository)
    {
        _context = context;
        _logger = logger;
        _memoryCache = memoryCache;
        this._expertRepository = _expertRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateBid(BidDto model, CancellationToken cancellationToken)
    {
        var newBid = new Bid()
        {
            RequestId = model.RequestId,
            Request = model.Request,
            ExpertId = model.ExpertId,
            Expert = model.Expert,
            DateOfRegisteration = DateTime.Now,
            DateOfWork = model.DateOfWork,
            ProposedPrice = model.ProposedPrice,
        };
        try
        {
            await _context.Bids.AddAsync(newBid);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception($"bid not created Maybe it has already been added", ex);
        }
    }

    public async Task<int> CountBids(CancellationToken cancellationToken)
        => await _context.Bids.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
    => await _context.Bids.AnyAsync(e => e.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var bid = await FindBid(id, cancellationToken);
        bid.Active = true;
        bid.IsWinner = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var bid = await FindBid(id, cancellationToken);

        bid.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AcceptRequest(int id, CancellationToken cancellationToken)
    {
        var bid = await _context.Bids
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        bid.IsWinner = true;
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is a problem accepting the request, find it {exception}", ex);
        }
    }

    public async Task DeleteBidById(int id, CancellationToken cancellationToken)
    {
        var bid = await _context.Bids
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (bid == null) return;

        _context.Bids.Remove(bid);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<BidDto>> GetAllBids(CancellationToken cancellationToken)
    {
        var result = await _context.Bids
            .Select(model => new BidDto
            {
                Request = model.Request,
                Expert = model.Expert,
                DateOfRegisteration = DateTime.Now,
                DateOfWork = model.DateOfWork,
                ProposedPrice = model.ProposedPrice,
            }).ToListAsync(cancellationToken);

        return result;
    }

    //public async Task<List<BidDto>> Get(int id, CancellationToken cancellationToken)
    //{
    //    var result = await _context.Bids
    //        .Select(model => new BidDto
    //        {
    //            Request = model.Request,
    //            Expert = model.Expert,
    //            DateOfRegisteration = DateTime.Now,
    //            DateOfWork = model.DateOfWork,
    //            ProposedPrice = model.ProposedPrice,
    //        }).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    //    return result;
    //}

    public async Task<Bid>? GetBidById(int id, CancellationToken cancellationToken)
    {
        var bid = await _context.Bids
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        return bid ?? new Bid();
    }

    public async Task UpdateBid(BidDto model, CancellationToken cancellationToken)
    {
        var bid = await _context.Bids
            .FirstOrDefaultAsync(b => b.Id == model.Id, cancellationToken);

        if (bid == null) return;

        try
        {
            bid.RequestId = model.RequestId;
            bid.ExpertId = model.ExpertId;
            bid.Request = model.Request;
            bid.Expert = model.Expert;
            bid.DateOfRegisteration = model.DateOfRegisteration;
            bid.DateOfWork = model.DateOfWork;
            bid.ProposedPrice = model.ProposedPrice;
            bid.IsWinner = model.IsWinner;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("There is probably an error in the changes applied to the bid Or there is another error {exception}", ex);
        }
    }

    public async Task<List<RequestDto>> Get(int customerId, CancellationToken cancellationToken)
    {
        var requests = await _context.Requests
            .Where(c => c.CustomerId == customerId)
            .Select(r => new RequestDto()
            {
                Id = r.Id,
            }).ToListAsync(cancellationToken);
        var service = await _context.Services
           .Select(s => new ServiceDto
           {
               Id = s.Id,
               Title = s.Title,
           }).FirstOrDefaultAsync(s => s.Id == customerId, cancellationToken);

        return requests;
    }


    public async Task<List<BidDto>> GetBidsByOrderId(int orderId, CancellationToken cancellationToken)
    {
        var bids = await _context.Bids
            .Where(b => b.RequestId == orderId)
            .Include(b => b.Expert)
            .ThenInclude(x=>x.ApplicationUser)
            .Select(b => new BidDto()
            {
                Id = b.Id,
                Expert = b.Expert,
                IsWinner = b.IsWinner,
                DateOfRegisteration = b.DateOfRegisteration,
                Active = b.Active,
                ProposedPrice = b.ProposedPrice,
                ExpertId = b.Expert.Id,
                Request = b.Request,
                DateOfWork = b.DateOfWork,
                RequestId = b.RequestId
            }).ToListAsync(cancellationToken);

        return bids;
    }

    public async Task CreateBidByRequestId(CreateBidDto model, CancellationToken cancellationToken)
    {
        var newBid = new Bid()
        {
            RequestId = model.RequestId,
            ExpertId = model.ExpertId,
            DateOfRegisteration = DateTime.Now,
        };
        await _context.Bids.AddAsync(newBid);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetRequestIdByBidId(int bidId, CancellationToken cancellationToken)
    {
        var record = await _context.Bids.FirstOrDefaultAsync(x => x.Id == bidId, cancellationToken);
        return (int)record.RequestId;
    }

    #endregion

    #region PrivateMethods
    private async Task<Bid> FindBid(int id, CancellationToken cancellationToken)
    {
        var bid = await _context.Bids
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        if (bid != null)
        {
            return bid;
        }

        throw new Exception($"bid with id {id} not found");
    }

    #endregion
}
