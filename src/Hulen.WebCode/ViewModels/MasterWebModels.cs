using System.Collections.Generic;
using Hulen.BusinessServices.ServiceModel;

namespace Hulen.WebCode.ViewModels
{
    public class MenuWebModel
    {
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }

    public class BannerWebModel
    {
        public User LoggedOnUser { get; set; }
    }
}
