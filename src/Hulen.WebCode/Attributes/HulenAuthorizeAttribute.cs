using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Hulen.WebCode.Attributes
{
    public class HulenAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            string username = "";
            string callingController = "";
            string callingAction = ""; 

            if (httpContext.Session != null)
            {
                username = httpContext.Session["currentUserID"].ToString();
                callingController = RouteTable.Routes.GetRouteData(httpContext).Values["controller"].ToString();
                callingAction = RouteTable.Routes.GetRouteData(httpContext).Values["action"].ToString();
            }

            return ValidateUserAccess(username, callingController, callingAction);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var result = new ViewResult {ViewName = "StayTheFuckAway"};
            filterContext.Result = result;
        }

        private static bool ValidateUserAccess(string username, string callingController, string callingAction)
        {
            return false;
        }
    }
}

