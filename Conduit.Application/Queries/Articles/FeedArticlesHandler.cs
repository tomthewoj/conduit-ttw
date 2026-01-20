using Conduit.Application.DTOs.Responses;
using Conduit.Application.DTOs.Responses.Multiple;
using Conduit.Application.Interfaces;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Articles
{
    public class FeedArticlesHandler : IRequestHandler<FeedArticlesQuery, ListArticleItemsResponse>
    {
        IArticleRepository _repo;
        public FeedArticlesHandler(IArticleRepository articleRepo, ITagRepository tagRepo, IFavoriteRepository favoriteRepo, IUserRepository userRepo, IFollowingRepository followRepo)
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

        public async Task<ListArticleItemsResponse> Handle(FeedArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepo.FeedArticles((Guid)request.currentUserId,request.limit,request.offset);

            var articleIds = articles.Select(a => a.Id).ToList();
            var authorIds = articles.Select(a => a.AuthorId).Distinct().ToList();

            var tags = await _tagRepo.GetTagsForArticles(articleIds);
            var favoritesCount = await _favoriteRepo.GetFavoritesCountForArticles(articleIds);

            IReadOnlyDictionary<Guid, bool> favoritedByUser = request.currentUserId != null // this hurts
                ? await _favoriteRepo.IsFavoritedByUserForArticles(articleIds, (Guid)request.currentUserId)
                : new Dictionary<Guid, bool>();

            var authors = await _userRepo.GetUsersByIds(authorIds);


            var resultList = new List<SimpleArticleResponse>();
            foreach (var article in articles)
            {
                var author = authors[article.AuthorId];

                resultList.Add(new SimpleArticleResponse
                (
                    Slug: article.Slug,
                    Title: article.Title,
                    Description: article.Description,
                    TagList: new TagResponse(tags.GetValueOrDefault(article.Id)),
                    CreatedAt: article.CreatedAt,
                    UpdatedAt: article.UpdatedAt,
                    Favorited: favoritedByUser.GetValueOrDefault(article.Id),
                    FavoritesCount: favoritesCount.GetValueOrDefault(article.Id),
                    Author: new ProfileResponse(author.UserName, author.Profile.Bio, author.Profile.ImageLink, true) // this errors
                    )
                );
            }

            return new ListArticleItemsResponse(resultList, resultList.Count);
        }
    }
}
