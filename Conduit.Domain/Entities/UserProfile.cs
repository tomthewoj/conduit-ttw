using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Bio {  get; set; } = null;
        public string? ImageLink { get; set; } = null;
        public UserProfile(Guid userId, string? bio, string? image)
        {
            UserId = userId;
            Bio = bio;
            ImageLink = image;
        }
    }
}
