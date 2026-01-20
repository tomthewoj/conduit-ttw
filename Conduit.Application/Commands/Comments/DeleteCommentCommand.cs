using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Comments
{
    public record DeleteCommentCommand(Guid? userId, string slug, string comment) : IRequest<Unit>;
}
