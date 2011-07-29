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
        public List<string> RequestedRoles { get; set; }

        public string[] AvailableSelected { get; set; }
        public string[] RequestedSelected { get; set; }

        public string SavedRequested { get; set; }
    }
}
