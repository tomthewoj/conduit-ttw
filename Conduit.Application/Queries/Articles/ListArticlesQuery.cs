using Conduit.Application.DTOs.Responses;
using Conduit.Application.DTOs.Responses.Multiple;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Articles
{
    public record ListArticlesQuery(Guid? currentUserId, string tag, string author, string favorited, int limit, int offset) : IRequest<ListArticleItemsResponse>;
}
