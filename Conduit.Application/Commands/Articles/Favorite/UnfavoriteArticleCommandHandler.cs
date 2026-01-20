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
        public UnfavoriteArticleCommandHandler(IArticleRepository arepo, IUserRepository urepo)
        {
            _urepo = urepo;
            _arepo = arepo;
        }
        private IArticleRepository _arepo;
        private IUserRepository _urepo;

        public async Task<Unit> Handle(UnfavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            /*
            var article = await _arepo.GetArticleBySlug(request.articleSlug);
            var user = await _urepo.GetUserById((Guid)request.currentUserId);
            if (article != null)
            {
                article.RemoveFavorite(user);
                await _arepo.UpdateArticle(article);
            } //or throw exception
            */
            return Unit.Value;
        }
    }
}
