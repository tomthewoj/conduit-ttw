using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Tags
{
    public record GetAllTagsQuery() : IRequest<ICollection<Tag>>;
}
