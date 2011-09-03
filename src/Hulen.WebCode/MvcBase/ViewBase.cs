using System.Collections.Generic;
using System.Web;

namespace Hulen.WebCode.MvcBase
{
    public static class ViewBase
    {
        public static bool UserHasAccessTo(string accessGroup)
        {
            var accessGroups = new List<string>();

            if (HttpContext.Current.Session != null)
            {
                accessGroups = (List<string>)HttpContext.Current.Session["accessGroups"];
            }

            return accessGroups.Contains(accessGroup);
        }
    }
}