using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Articles.Favorite
{
    public class UnfavoriteArticleCommandHandler : IRequestHandler<UnfavoriteArticleCommand, Unit>
    {
        public UnfavoriteArticleCommandHandler(IArticleRepository articleRepository, IFavoriteRepository favoriteRepository)
        {
            _articleRepository = articleRepository;
            _favoriteRepository = favoriteRepository;
        }
        private IArticleRepository _articleRepository;
        private IFavoriteRepository _favoriteRepository;

        public async Task<Unit> Handle(UnfavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetArticleBySlug(request.slug);
            if (article == null)
                throw new Exception("Article not found");
            await _favoriteRepository.RemoveFavorite(article.Id, request.currentUserId);
            return Unit.Value;
        }
    }
}
