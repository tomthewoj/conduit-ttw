using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Articles
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
    {
        public UpdateArticleCommandHandler(IArticleRepository repo) => _repo = repo;
        private IArticleRepository _repo;
        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _repo.GetArticleBySlug(request.slug);
            if (article != null)
            {
                article.UpdateArticle(request.Title, request.Description, request.Body);
                await _repo.UpdateArticle(article);
            }

            return Unit.Value;
        }
    }
}
