using HomeService.Core.Contracts.CommentContracts;
using HomeService.Core.DTOs;
using HomeService.Core.Entities;
using HomeService.Infrastructure.DataBase.Sql.Ef;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace HomeService.Infrastructure.DataAccess.Repo.Ef.Repositories;

public class CommentRepository : ICommentRepository
{

    #region Fields
    private readonly AppDbContext _context;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<CommentRepository> _logger;
    #endregion

    #region Ctors
    public CommentRepository(AppDbContext context, IMemoryCache memoryCache, ILogger<CommentRepository> logger)
    {
        _context = context;
        _memoryCache = memoryCache;
        _logger = logger;
    }
    #endregion

    #region Implementations
    public async Task CreateCommentDto(CreateCommentDto model, CancellationToken cancellationToken)
    {
        var newComment = new Comment()
        {
            CommentText = model.CommentText,
            CustomerId = model.CustomerId,
            ExpertId = model.ExpertId,
            Score = model.Score,
        };
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateComment(CommentDto model, CancellationToken cancellationToken)
    {
        var newComment = new Comment()
        {
            CommentText = model.CommentText,
            CreatAt = DateTime.Now,
            CustomerId = model.CustomerId,
            Customer = model.Customer,
            ExpertId = model.ExpertId,
            Expert = model.Expert,
            Score = model.Score,
        };
        try
        {
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Comment not created Maybe it has already been added {exception}", ex);
        }
    }

    public async Task<int> CountComments(CancellationToken cancellationToken)
        => await _context.Comments.CountAsync(cancellationToken);

    public async Task<bool> IsActive(int id, CancellationToken cancellationToken)
        => await _context.Comments.AnyAsync(c => c.Id == id, cancellationToken);

    public async Task Active(int id, CancellationToken cancellationToken)
    {
        var comment = await FindComment(id, cancellationToken);
        comment.Active = true;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeActive(int id, CancellationToken cancellationToken)
    {
        var comment = await FindComment(id, cancellationToken);

        comment.Active = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCommentById(int id, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (comment == null) return;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<CommentDto>> GetAllComments(CancellationToken cancellationToken)
    {
        var result = await _context.Comments
            .Select(model => new CommentDto()
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                CommentText = model.CommentText,
                CreatAt = DateTime.Now,
                Customer = model.Customer,
                Expert = model.Expert,
                Score = model.Score,
                Active = model.Active,
                IsDeleted = model.IsDeleted,
            }).ToListAsync(cancellationToken);

        return result;
    }

    //public async Task<Comment>? GetCommentById(int id, CancellationToken cancellationToken)
    //{
    //    var comment = await _context.Comments
    //        .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    //    return comment ?? new Comment();
    //}

    public async Task<CommentDto> GetCommentById(int commentId, CancellationToken cancellationToken)
    {
        var comment = _memoryCache.Get<CommentDto>("commentDto");
        if (comment is null)
        {
            comment = await _context.Comments
            .Select(c => new CommentDto
            {
                Id = c.Id,
                CommentText = c.CommentText,
            }).FirstOrDefaultAsync(a => a.Id == commentId, cancellationToken);

            if (comment != null)
            {
                _memoryCache.Set("commentDto", comment, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(120)
                });
                _logger.LogInformation("commentDto returned from database, and cached in memory successfully.");
                return comment;
            }
            else
            {
                _logger.LogError("We expected the commentDto to return from the database, but it returned null.");
                throw new Exception("Something wents wrong!, please try again.");
            }
        }
        _logger.LogInformation("commentDto returned from InMemoryCache.");
        return comment;
    }

    public async Task UpdateComment(CommentDto model, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == model.Id, cancellationToken);

        if (comment == null) return;

        try
        {
            comment.Id = model.Id;
            comment.CommentText = model.CommentText;
            comment.Score = model.Score;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            comment = new Comment();
        }
    }

    public async Task<List<CommentDto>> GetAllCommentsByExpertId(int expertId, CancellationToken cancellationToken)
    {
        var result = await _context.Comments
            .Include(x=>x.Expert)
            .Where(x=>x.ExpertId == expertId && x.Active)
            .Select(model => new CommentDto()
            {
                Id = model.Id,
                CustomerId = model.CustomerId,
                CommentText = model.CommentText,
                CreatAt = DateTime.Now,
                Customer = model.Customer,
                Expert = model.Expert,
                Score = model.Score,
                Active = model.Active,
                IsDeleted = model.IsDeleted,
            }).ToListAsync(cancellationToken);

        return result;
    }

    public async Task<bool> HaveComment(int expertId, CancellationToken cancellationToken)
    {
        return await _context.Comments
            .AnyAsync(x => x.ExpertId == expertId && x.Active, cancellationToken);
    }

    #endregion

    #region PrivateMethods
    private async Task<Comment> FindComment(int id, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (comment != null)
        {
            return comment;
        }

        throw new Exception($"Comment with id {id} not found");
    }

    #endregion

}
