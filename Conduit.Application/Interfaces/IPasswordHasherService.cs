using Conduit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Application.Interfaces
{
    public interface IPasswordHasherService
    {
        public string HashPassword(User user, string password);
        public bool VerifyPassword(User user, string password, string hashed);
    }
}
