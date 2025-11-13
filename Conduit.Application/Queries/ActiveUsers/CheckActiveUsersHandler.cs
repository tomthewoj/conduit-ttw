using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.ActiveUsers
{
    public class CheckActiveUsersHandler(IUserRepository repo) : IRequestHandler<CheckActiveUsersQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _repo = repo;

        public async Task<IEnumerable<User>> Handle(CheckActiveUsersQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllActiveUsers();
        }
    }
}
