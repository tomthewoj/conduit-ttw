using Conduit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Infra.Data.Models
{
    public class UserProfileEntity
    {
        public Guid UserId { get; set; }
        public string? Bio { get; set; }
        public string? ImageLink { get; set; }
        public UserProfileEntity() { }
        public UserProfileEntity(Guid userid) => UserId = userid;
    }
}
