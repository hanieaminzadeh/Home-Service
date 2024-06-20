using HomeService.Core.Contracts.CityContracts;
using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class CommentService : ICommentService
{

    #region Fields
    private readonly ICommentRepository _commentRepository;
    #endregion

    #region Ctors
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    #endregion

    #region Implementations
    public async Task CreateComment(CommentDto model, CancellationToken cancellationToken)
        => await _commentRepository.CreateComment(model, cancellationToken);

    public async Task<int> CountComments(CancellationToken cancellationToken)
        => await _commentRepository.CountComments(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _commentRepository.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _commentRepository.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _commentRepository.DeActive(id, cancellationToken);
    }

    public async Task DeleteCommentById(int id, CancellationToken cancellationToken)
    {
        await _commentRepository.DeleteCommentById(id, cancellationToken);
    }

    public async Task<List<CommentDto>> GetAllComments(CancellationToken cancellationToken)
        => await _commentRepository.GetAllComments(cancellationToken);

    public async Task<CommentDto> GetCommentById(int commentId, CancellationToken cancellationToken)
        => await _commentRepository.GetCommentById(commentId, cancellationToken);

    public async Task UpdateComment(CommentDto model, CancellationToken cancellationToken)
    {
        await _commentRepository.UpdateComment(model, cancellationToken);
    }

    public async Task<List<CommentDto>> GetAllCommentsByExpertId(int expertId, CancellationToken cancellationToken)
    {
        return await _commentRepository.GetAllCommentsByExpertId(expertId, cancellationToken);
    }

    public async Task<bool> HaveComment(int expertId, CancellationToken cancellationToken)
    {
       return await _commentRepository.HaveComment(expertId, cancellationToken);
    }

    public async Task CreateCommentDto(CreateCommentDto model, CancellationToken cancellationToken)
        => await _commentRepository.CreateCommentDto(model, cancellationToken);
    #endregion
}
