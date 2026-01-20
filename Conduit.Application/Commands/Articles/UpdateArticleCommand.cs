using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Articles
{
    public record UpdateArticleCommand(string slug, Guid? AuthorId,
    string? Title,
    string? Description,
    string? Body) : IRequest<Unit>;
}
