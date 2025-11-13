using Microsoft.AspNetCore.Mvc;

namespace Conduit.DTOs
{
    public class RegisterUserDto : Controller
    {
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
