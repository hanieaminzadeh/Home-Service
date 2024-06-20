using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Contracts.BidContracts;

public interface IBidRepository
{
    Task CreateBid(BidDto model, CancellationToken cancellationToken);
    Task<int> CountBids(CancellationToken cancellationToken);
    Task<bool> IsActive(int id, CancellationToken cancellationToken);
    Task Active(int id, CancellationToken cancellationToken);
    Task DeActive(int id, CancellationToken cancellationToken);
    Task AcceptRequest(int id, CancellationToken cancellationToken);
    Task DeleteBidById(int id, CancellationToken cancellationToken);
    Task<List<BidDto>> GetAllBids(CancellationToken cancellationToken);
    Task<Bid>? GetBidById(int id, CancellationToken cancellationToken);
    Task UpdateBid(BidDto model, CancellationToken cancellationToken);
    Task<List<BidDto>> GetBidsByOrderId(int orderId, CancellationToken cancellationToken);
    Task CreateBidByRequestId(CreateBidDto model, CancellationToken cancellationToken);
    Task<int> GetRequestIdByBidId(int bidId, CancellationToken cancellationToken);
}
