using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Articles.Favorite
{
    public class FavoriteArticleCommandHandler : IRequestHandler<FavoriteArticleCommand,Unit>
    {
        public FavoriteArticleCommandHandler(IArticleRepository articleRepository, IFavoriteRepository favoriteRepository)
        {
            _articleRepository = articleRepository;
            _favoriteRepository = favoriteRepository;
        }
        private IArticleRepository _articleRepository;
        private IFavoriteRepository _favoriteRepository;

        public async Task<Unit> Handle(FavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlug(request.slug);
            if (article == null)
                throw new Exception("Article not found");
            if (article.AuthorId == request.currentUserId)
                throw new UnauthorizedAccessException("Cannot favorite user's own article");
            await _favoriteRepository.AddFavorite(article.Id, request.currentUserId);
            return Unit.Value;
        }
    }
}
