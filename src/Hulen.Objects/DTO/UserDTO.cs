using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password {get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
    }
}
