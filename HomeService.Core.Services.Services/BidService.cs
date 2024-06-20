using HomeService.Core.Contracts.BidContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.Services;

public class BidService : IBidService
{
    #region Fields
    private readonly IBidRepository _bidRepository;
    #endregion

    #region Ctors
    public BidService(IBidRepository bidRepository)
    {
        _bidRepository = bidRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateBid(BidDto model, CancellationToken cancellationToken)
        => await _bidRepository.CreateBid(model, cancellationToken);

    public async Task<int> CountBids(CancellationToken cancellationToken)
        => await _bidRepository.CountBids(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _bidRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
        => await _bidRepository.Active(id, cancellationToken);

    public async Task DeActive(int id, CancellationToken cancellationToken)
        => await _bidRepository.DeActive(id, cancellationToken);

    public async Task AcceptRequest(int id, CancellationToken cancellationToken)
        => await _bidRepository.AcceptRequest(id, cancellationToken);

    public async Task DeleteBidById(int id, CancellationToken cancellationToken)
        => await _bidRepository.DeleteBidById(id, cancellationToken);

    public async Task<List<BidDto>> GetAllBids(CancellationToken cancellationToken)
        => await _bidRepository.GetAllBids(cancellationToken);

    public async Task<Bid>? GetBidById(int id, CancellationToken cancellationToken)
        => await _bidRepository.GetBidById(id, cancellationToken);

    public async Task UpdateBid(BidDto model, CancellationToken cancellationToken)
        => await _bidRepository.UpdateBid(model, cancellationToken);

    public async Task<List<BidDto>> GetBidsByOrderId(int orderId, CancellationToken cancellationToken)
        => await _bidRepository.GetBidsByOrderId(orderId, cancellationToken);

    public async Task CreateBidByRequestId(CreateBidDto model, CancellationToken cancellationToken)
        => await _bidRepository.CreateBidByRequestId(model, cancellationToken);

    public async Task<int> GetRequestIdByBidId(int bidId, CancellationToken cancellationToken)
        => await _bidRepository.GetRequestIdByBidId(bidId, cancellationToken);


    #endregion
}
