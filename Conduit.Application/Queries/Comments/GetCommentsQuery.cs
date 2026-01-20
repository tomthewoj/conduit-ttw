using Conduit.Application.DTOs.Responses;
using Conduit.Application.DTOs.Responses.Multiple;
using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Comments
{
    public record class GetCommentsQuery(Guid? currentUserId, string slug,int limit = 10, int offset = 0 ) : IRequest<ListCommentItemsResponse>;
}
