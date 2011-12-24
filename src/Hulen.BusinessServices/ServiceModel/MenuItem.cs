using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulen.BusinessServices.ServiceModel
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int MenuLevel { get; set; }
        public string Parent { get; set; }
        public int SortOrder { get; set; }
        public bool IsLink { get; set; }
        public string AccessGroup { get; set; }
    }
}
