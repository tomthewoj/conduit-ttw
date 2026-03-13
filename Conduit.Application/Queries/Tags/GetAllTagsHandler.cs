using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Tags
{
    public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, ICollection<Tag>>
    {
        private readonly ITagRepository _tagRepository;
        public GetAllTagsHandler(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public async Task<ICollection<Tag>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            return await _tagRepository.GetAllTags(cancellationToken);
        }
    }
}
