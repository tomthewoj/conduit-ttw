using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Register
{
    public class CheckUserExistsHandler : IRequestHandler<CheckUserExistsQuery, bool>
    {
        IUserRepository _repo;
        public CheckUserExistsHandler(IUserRepository repo) => _repo = repo;

        public async Task<bool> Handle(CheckUserExistsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ExistsByUsernameAsync(request.Username);
        }
    }
}
