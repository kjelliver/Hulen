using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.Objects.ViewModels
{
    public class AccessGroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public List<string> RolesThatHaveAccess { get; set; }
    }
}
