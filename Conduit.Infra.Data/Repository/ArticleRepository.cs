using Conduit.Application.DTOs;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Domain.ValueObjects;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Conduit.Infra.Data.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ConduitDbContext _context;
        public ArticleRepository(ConduitDbContext context) => _context = context;
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }

        public async Task CreateArticle(Article article, IReadOnlyCollection<string> tags)
        {
            var articleEntity = await MapArticle(article);
            await _context.Articles.AddAsync(articleEntity);
            await AddTags(articleEntity, tags);
            await SaveChangesAsync();
        }
        private async Task AddTags(ArticleEntity article, IReadOnlyCollection<string> tags)
        {
            var normalizedTags = tags.Select(t => t.Trim().ToLower()).Distinct().ToList();
            var existingTags = await _context.Tags.Where(t => normalizedTags.Contains(t.Name)).ToListAsync();
            var missingTags = normalizedTags
                .Except(existingTags.Select(t => t.Name))
                .Select(name => new TagEntity { Id = Guid.NewGuid(), Name = name })
                .ToList();
            await _context.Tags.AddRangeAsync(missingTags);

            var allTags = existingTags.Concat(missingTags).ToList();
            var articleTags = allTags.Select(t => new ArticleTagsEntity
            {
                Article = article,
                Tag = t
            });
            await _context.ArticleTags.AddRangeAsync(articleTags);
        }
        private async Task<ArticleEntity> MapArticle(Article article)
        {
            var articleEntity = new ArticleEntity();
            articleEntity.Id = article.Id;
            articleEntity.Slug = article.Slug;
            articleEntity.Body = article.Body;
            articleEntity.Title = article.Title;
            articleEntity.Description = article.Description;
            articleEntity.CreatedAt = DateTime.Now;
            articleEntity.UpdatedAt = DateTime.Now;
            articleEntity.AuthorId = (Guid)article.AuthorId;
            return articleEntity;
        }

        public async Task UpdateArticle(Article article)
        {
            var articleForUpdate = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == article.Slug);
            if (articleForUpdate == null)
                throw new Exception("Article not found");
            if (article.Body!= null) articleForUpdate.Body = article.Body;
            if (article.Title != null) articleForUpdate.Title = article.Title;
            if (article.Description != null) articleForUpdate.Description = article.Description;
            _context.Update(articleForUpdate);
            await SaveChangesAsync();
        }

        public async Task DeleteArticle(Article article)
        {
            var articleForDeletion = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == article.Slug);
            if (articleForDeletion == null)
                throw new Exception("Article not found");
            _context.Articles.Remove(articleForDeletion);
            await SaveChangesAsync();
        }

        public async Task FavoriteArticle(Article article, User user)
        {
            var articleFavorite = new ArticleFavoriteEntity
            {
                ArticleId = article.Id,
                AuthorId = user.Id
            };
            await _context.ArticleFavorite.AddAsync(articleFavorite);
            await SaveChangesAsync();
        }
        public async Task AddComment(Comment comment)
        {
            var newComment = new CommentEntity
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                AuthorId = comment.AuthorId,
                Body = comment.Body,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Comments.AddAsync(newComment);
            await SaveChangesAsync();
        }
        public async Task DeleteComment(Comment comment)
        {
            var commentForDeletion = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
            if (commentForDeletion == null) throw new Exception("Comment not found");
            _context.Comments.Remove(commentForDeletion);
            await SaveChangesAsync();
        }
        public async Task<(IReadOnlyList<Article>, int)> FeedArticles(Guid currentUserId, int limit, int offset)
        {
            var followeeIds = await _context.UserFollow.Where(f => f.FollowerId == currentUserId).Select(f => f.FolloweeId).ToListAsync();
            var totalCount = await _context.Articles.Where(a => followeeIds.Contains(a.AuthorId)).CountAsync();
            var articles = await _context.Articles.Where(a => followeeIds.Contains(a.AuthorId)).OrderByDescending(d => d.CreatedAt).Skip(offset).Take(limit).Select(a => Article.CreateArticle
            (
                a.Id,
                a.Slug,
                a.Title,
                a.Description,
                a.Body,
                a.AuthorId
             )).ToListAsync();
            return (articles, totalCount);
        }

        public async Task<(IReadOnlyList<Article>,int)> ListArticles(string tag, string author, string favorited, int limit, int offset) // limit/offsety do helpera
        {
            IQueryable<ArticleEntity> query = _context.Articles;
            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(a => a.Author.UserName == author);
            }
            if (!string.IsNullOrWhiteSpace(favorited))
            {
                query = query.Where(f => f.Favorited.Any(a => a.Author.UserName == favorited)); 
            }
            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(a => a.ArticleTags.Any(at => at.Tag.Name == tag));
            }
            var totalCount = await query.CountAsync();
            var articles = await query.OrderByDescending(a => a.CreatedAt).Skip(offset).Take(limit).Select(a => Article.CreateArticle
            (
                a.Id,
                a.Slug,
                a.Title,
                a.Description,
                a.Body,
                a.AuthorId
             )).ToListAsync();
            return (articles, totalCount);
        }
        public async Task<Article?> GetArticleBySlug(string slug)
        {
            var article = await _context.Articles.Where(a => a.Slug == slug).Select(a => Article.CreateArticle
            (
                a.Id,
                a.Slug,
                a.Title,
                a.Description,
                a.Body,
                a.AuthorId
             )).FirstOrDefaultAsync();
            return article;
        }

        public async Task DeleteArticle(Guid articleId)
        {
            var article = await _context.Articles
                            .Include(a => a.ArticleTags)
                            .FirstOrDefaultAsync(a => a.Id == articleId);

            if (article == null)
                throw new KeyNotFoundException();

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }

        public Task DeleteComment(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Comment>> GetComments(Guid articleId)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid?> GetArticleIdBySlug(string slug)
        {
            return await _context.Articles.Where(a => a.Slug == slug).Select(a => (Guid?)a.Id).FirstOrDefaultAsync();
        }

    }
}
