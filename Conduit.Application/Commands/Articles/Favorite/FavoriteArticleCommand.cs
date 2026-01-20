using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Commands.Articles.Favorite
{
    public record FavoriteArticleCommand(string articleSlug, Guid? currentUserId) : IRequest<Unit>;
}
