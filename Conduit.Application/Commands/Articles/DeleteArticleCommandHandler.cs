using Conduit.Application.Interfaces;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Articles
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
    {
        public DeleteArticleCommandHandler(IArticleRepository articleRepo) => _articleRepo = articleRepo;
        private readonly IArticleRepository _articleRepo;
        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepo.GetArticleBySlug(request.slug);

            if (article == null)
                throw new Exception("Article not found");
            if (article.AuthorId != request.currentUserId)
                throw new UnauthorizedAccessException("Cannot delete another user's article");

            await _articleRepo.DeleteArticle(article.Id);
            return Unit.Value;
        }
    }
}
