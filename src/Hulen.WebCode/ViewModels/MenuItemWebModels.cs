using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class MenuItemWebModel
    {
        public IEnumerable<MenuItem> AllMenuItems { get; set; }
        public MenuItem MenuItem { get; set; }

        public List<int> MenuLevels { get; set; }
        public List<string> AccessGroups { get; set; }
        public List<string> Parents { get; set; }
    }
}
