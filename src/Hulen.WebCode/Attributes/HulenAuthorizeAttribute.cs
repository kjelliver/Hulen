using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Hulen.WebCode.Attributes
{
    public class HulenAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _accessGroup;


        public HulenAuthorizeAttribute()
        {
        }

        public HulenAuthorizeAttribute(string accessGroup)
        {
            _accessGroup = accessGroup;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var accessGroups = new List<string>();

            if (httpContext.Session != null)
            {
                accessGroups = (List<string>) httpContext.Session["accessGroups"];
                if(accessGroups != null)
                {
                    return accessGroups.Contains(_accessGroup);
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var result = new ViewResult {ViewName = "StayTheFuckAway"};
            filterContext.Result = result;
        }
    }
}

