using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Queries.Register
{
    public record CheckEmailExistsQuery(string Email) : IRequest<bool>;
}
