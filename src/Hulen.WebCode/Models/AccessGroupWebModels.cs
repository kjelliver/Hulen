using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hulen.Objects.Models;

namespace Hulen.WebCode.Models
{
    public class AccessGroupIndexModel
    {
        public List<AccessGroup> AllAccessGroups { get; set; }
    }

    public class AccessGroupEditModel
    {
        public AccessGroup AccessGroup { get; set; }
        public List<string> AvailableRoles { get; set; }
        public List<string> RequestedRoles { get; set; }

        public string[] AvailableSelected { get; set; }
        public string[] RequestedSelected { get; set; }

        public string SavedRequested { get; set; }

        public void GetSavedRoles()
        {
            var sb = new StringBuilder();
            foreach(string role in RequestedRoles)
            {
                sb.Append(role + ",");
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            SavedRequested = sb.ToString();
        }

        public void AddRolesToAccessGroup()
        {
            if (AccessGroup.RolesThatHaveAccess == null)
                AccessGroup.RolesThatHaveAccess = new List<string>();
            AccessGroup.RolesThatHaveAccess.AddRange(RequestedRoles);
        }
    }
}
