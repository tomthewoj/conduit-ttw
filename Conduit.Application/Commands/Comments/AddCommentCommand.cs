using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Comments
{
    public record AddCommentCommand(Guid currentUserId, string slug, string commentBody) : IRequest<Unit>;
}
