using Conduit.Domain.Interfaces;
using Conduit.Infra.Data.Context;
using Conduit.Infra.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Repository
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ConduitDbContext _context;
        public FollowingRepository(ConduitDbContext context) => _context = context;
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
        public Task<bool> IsUserFollowing(Guid userId, Guid followee)
        {
            return _context.UserFollow.Where(uf => uf.FollowerId == userId && uf.FolloweeId == followee).AnyAsync();
        }

            public async Task FollowUser(Guid userId, Guid followeeId)
            {
            var exists = await _context.UserFollow.AnyAsync(x => x.FollowerId == userId && x.FolloweeId == followeeId);
            if (exists) return;

            await _context.UserFollow.AddAsync(new UserFollowEntity
            {
                FollowerId = userId,
                FolloweeId = followeeId
            });

            await SaveChangesAsync();
        }

            public async Task UnfollowUser(Guid userId, Guid followeeId)
            {
            var entity = await _context.UserFollow.FirstOrDefaultAsync(x => x.FollowerId == userId && x.FolloweeId == followeeId);
            if (entity == null) return;

            _context.UserFollow.Remove(entity);
            await SaveChangesAsync();
        }   
    }
}
