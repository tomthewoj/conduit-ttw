using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Interfaces
{
    public interface IFollowerRepository
    {
        Task<IEnumerable<Guid>> GetAllFollowers(Guid id);
    }
}
