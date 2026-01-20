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
        public DeleteArticleCommandHandler(IArticleRepository articleRepo) => _articleWriteRepo = articleRepo;
        private IArticleRepository _articleWriteRepo;
        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            /*
            var article = await _articleWriteRepo.GetArticleBySlug(request.slug);
            if (article.AuthorId == request.currentUserId) 
                await _articleWriteRepo.DeleteArticle(article);
            else
                throw new Exception("Unauthorized deletion");
            */
            return Unit.Value;
        }
    }
}
