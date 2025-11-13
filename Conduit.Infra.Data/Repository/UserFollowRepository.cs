using Conduit.Domain.Entities;
using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Repository
{
    public class UserFollowRepository : IFollowerRepository
    {
        private readonly ConduitDbContext _context;
        public UserFollowRepository(ConduitDbContext context) => _context = context;
        public async Task<IEnumerable<Guid>> GetAllFollowers(Guid id)
        {
            var followerIds = await _context.UserFollow.Where(uf => uf.FolloweeId == id).Select(uf => uf.FollowerId).ToListAsync();
            return followerIds;
        }
    }
}
