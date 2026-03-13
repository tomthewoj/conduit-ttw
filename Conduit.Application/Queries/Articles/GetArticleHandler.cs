using Conduit.Application.DTOs.Responses;
using Conduit.Application.Interfaces;
using Conduit.Application.Queries.Users;
using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Articles
{
    public class GetArticleHandler : IRequestHandler<GetArticleQuery, ArticleResponse?>
    {
        public GetArticleHandler(IArticleRepository articleRepo, ITagRepository tagRepo, IFavoriteRepository favoriteRepo, IUserRepository userRepo, IFollowingRepository followRepo)
        {
            _articleRepo = articleRepo;
            _tagRepo = tagRepo;
            _favoriteRepo = favoriteRepo;
            _userRepo = userRepo;
            _followRepo = followRepo;
        }
        private IArticleRepository _articleRepo;
        private ITagRepository _tagRepo;
        private IFavoriteRepository _favoriteRepo;
        private IUserRepository _userRepo;
        private IFollowingRepository _followRepo;

        public async Task<ArticleResponse?> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepo.GetArticleBySlug(request.slug);
            var tags = await _tagRepo.GetTagsForArticle(article.Id);
            var favoritesCount = await _favoriteRepo.GetFavoritesCount(article.Id);

            var author = await _userRepo.GetUserById(article.AuthorId);

            bool isUserFollowing = false;
            bool isFavoritedByUser = false;
            if (request.currentUserId != null)
            {
                isUserFollowing = await _followRepo.IsUserFollowing((Guid)request.currentUserId, author.Id);
                isFavoritedByUser = await _favoriteRepo.IsFavoritedByUser(article.Id, (Guid)request.currentUserId);
            }


            var response = new ArticleResponse(
                article.Slug,
                article.Title,
                article.Description,
                article.Body,
                tags,
                article.CreatedAt,
                article.UpdatedAt,
                isFavoritedByUser,
                favoritesCount,
                new ProfileResponse(author.UserName, author.Profile.Bio, author.Profile.ImageLink, isUserFollowing));
            return response;
        }
    }
}
