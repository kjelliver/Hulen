using System;

namespace Hulen.Storage.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password {get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Disabled { get; set; }
        public bool MustChangePassword { get; set; }
    }
}
