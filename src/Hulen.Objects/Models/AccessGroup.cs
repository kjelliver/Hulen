using System;
using System.Collections.Generic;

namespace Hulen.Objects.Models
{
    public class AccessGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<string> RolesThatHaveAccess { get; set; }
    }
}
