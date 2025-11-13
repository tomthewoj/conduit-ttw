using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public string Bio {  get; set; }
        public string ImageLink {  get; set; }
    }
}
