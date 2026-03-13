using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ConduitDbContext _context;
        public CommentRepository(ConduitDbContext context) => _context = context;
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
        public async Task AddComment(Comment comment)
        {
            var resultComment = new CommentEntity { ArticleId = comment.ArticleId, AuthorId = comment.AuthorId,  Body = comment.Body,  CreatedAt = comment.CreatedAt, UpdatedAt = comment.UpdatedAt};
            await _context.Comments.AddAsync(resultComment);
            await SaveChangesAsync();
        }

        public async Task DeleteComment(Guid commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if(comment == null) return;
            _context.Comments.Remove(comment);
            await SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Comment>> GetAllComments(Guid articleId, int limit, int offset)
        {
            return await _context.Comments.Where(c => c.ArticleId == articleId).OrderByDescending(d => d.CreatedAt).Select(c => new Comment(c.Id,c.Body,c.AuthorId,c.ArticleId, c.CreatedAt, c.UpdatedAt)).Skip(offset).Take(limit).ToListAsync();
        }

        public Task UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment?> GetCommentById(Guid commentId)
        {
            var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (entity == null) return null;

            return new Comment(
                entity.Id,
                entity.Body,
                entity.AuthorId,
                entity.ArticleId,
                entity.CreatedAt,
                entity.UpdatedAt
            );
        }
    }
}
