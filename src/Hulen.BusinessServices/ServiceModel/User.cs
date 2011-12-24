using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.ServiceModel
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public bool Disabled { get; set; }
        public bool MustChangePassword { get; set; }
    }
}
