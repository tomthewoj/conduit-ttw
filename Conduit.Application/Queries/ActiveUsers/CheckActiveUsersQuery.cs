using Conduit.Domain.Entities;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.ActiveUsers
{
    public class CheckActiveUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}
