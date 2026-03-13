using Conduit.Application.Interfaces;
using Conduit.Application.Queries.Articles;
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
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Unit>
    {
        public CreateArticleCommandHandler(IArticleRepository articleRepo, ITagRepository tagRepo) => (_articleRepo, _tagRepo) = (articleRepo, tagRepo);
        private IArticleRepository _articleRepo;
        private ITagRepository _tagRepo;
        public async Task<Unit> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {

            var article = Article.CreateArticle(null, null, request.Title, request.Description, request.Body, (Guid)request.AuthorId);
            await _articleRepo.CreateArticle(article, request.Tags);
            return Unit.Value;
        }
    }
}
