using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Comments
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        IArticleRepository _repo;
        public DeleteCommentCommandHandler(IArticleRepository articleWriteRepo)
        {
            _articleWriteRepo = articleWriteRepo;
        }

        private IArticleRepository _articleWriteRepo;
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            /*
            var article = await _repo.GetArticleBySlug(request.slug);
            var comment = new Comment(request.comment, (Guid)request.userId, article.Id);
            await _repo.RemoveComment(comment);
            */
            return Unit.Value;
            
        }
    }
}
