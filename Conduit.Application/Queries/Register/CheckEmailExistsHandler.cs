using Conduit.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Register
{
    public class CheckEmailExistsHandler : IRequestHandler<CheckEmailExistsQuery, bool>
    {
        private readonly IUserRepository _repo;
        public CheckEmailExistsHandler(IUserRepository repo) => _repo = repo;
        public async Task<bool> Handle(CheckEmailExistsQuery request, CancellationToken cancellationToken)
        {
            return await _repo.ExistsByEmailAsync(request.Email);
        }
    }
}
