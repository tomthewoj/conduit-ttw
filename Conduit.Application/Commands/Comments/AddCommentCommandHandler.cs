using Conduit.Application.Interfaces;
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
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Unit>
    {
        ICommentRepository _repo;
        IArticleRepository _articleRepo;
        public AddCommentCommandHandler(ICommentRepository repo, IArticleRepository articleRepo)
        {
            _repo = repo;
            _articleRepo = articleRepo;
        }
        public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var article = await _articleRepo.GetArticleBySlug(request.slug);
            if (article == null) throw new Exception("Article doesn't exist");
            await _repo.AddComment(new Comment(null,request.commentBody, request.currentUserId, article.Id,DateTime.UtcNow,DateTime.UtcNow));
            return Unit.Value;
        }
    }
}
