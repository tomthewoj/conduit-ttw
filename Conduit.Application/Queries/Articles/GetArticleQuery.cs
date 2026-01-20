using Conduit.Application.DTOs.Responses;
using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Articles
{
    public record GetArticleQuery(Guid? currentUserId, string slug) : IRequest<ArticleResponse?>;
}
