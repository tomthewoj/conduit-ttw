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
        ICommentRepository _commentRepository;
        public DeleteCommentCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var commentForDeletion = await _commentRepository.GetCommentById(request.commentId);
            if (commentForDeletion == null) throw new Exception("Comment doesn't exist");
            if (commentForDeletion.AuthorId != request.currentUserId) throw new Exception("Comment doesn't belong to the user");
            await _commentRepository.DeleteComment(request.commentId);
            return Unit.Value; 
        }
    }
}
