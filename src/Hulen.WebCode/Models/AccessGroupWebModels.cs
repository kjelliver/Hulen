using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.ViewModels;

namespace Hulen.WebCode.Models
{
    public class AccessGroupIndexModel
    {
        public List<AccessGroupViewModel> AllAccessGroups { get; set; }
    }

    public class AccessGroupEditModel
    {
        public AccessGroupViewModel AccessGroup { get; set; }
        public List<string> AvailableRoles { get; set; }
        public List<string> RegisteredRoles { get; set; }
    }
}
