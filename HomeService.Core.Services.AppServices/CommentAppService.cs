using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;

namespace HomeService.Core.Services.AppServices;

public class CommentAppService : ICommentAppService
{

    #region Fields
    private readonly ICommentService _commentService;
    #endregion

    #region Ctors
    public CommentAppService(ICommentService commentService)
    {
        _commentService = commentService;
    }
    #endregion

    #region Implementations
    public async Task CreateComment(CommentDto model, CancellationToken cancellationToken)
        => await _commentService.CreateComment(model, cancellationToken);

    public async Task<int> CountComments(CancellationToken cancellationToken)
        => await _commentService.CountComments(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _commentService.IsActive(id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        await _commentService.Active(id, cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        await _commentService.DeActive(id, cancellationToken);
    }

    public async Task DeleteCommentById(int id, CancellationToken cancellationToken)
    {
        await _commentService.DeleteCommentById(id, cancellationToken);
    }

    public async Task<List<CommentDto>> GetAllComments(CancellationToken cancellationToken)
        => await _commentService.GetAllComments(cancellationToken);

    public async Task<CommentDto> GetCommentById(int commentId, CancellationToken cancellationToken)
        => await _commentService.GetCommentById(commentId, cancellationToken);

    public async Task UpdateComment(CommentDto model, CancellationToken cancellationToken)
    {
        await _commentService.UpdateComment(model, cancellationToken);
    }

    public async Task<List<CommentDto>> GetAllCommentsByExpertId(int expertId, CancellationToken cancellationToken)
    {
        return await _commentService.GetAllCommentsByExpertId(expertId, cancellationToken);
    }

    public async Task<bool> HaveComment(int expertId, CancellationToken cancellationToken)
    {
       return await _commentService.HaveComment(expertId, cancellationToken);
    }

    public async Task CreateCommentDto(CreateCommentDto model, CancellationToken cancellationToken)
        => await _commentService.CreateCommentDto(model, cancellationToken);
    #endregion
}
