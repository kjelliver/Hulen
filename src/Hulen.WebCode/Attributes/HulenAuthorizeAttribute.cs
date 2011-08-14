using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Hulen.BusinessServices.Interfaces;
using Hulen.BusinessServices.Services;
using Hulen.Objects.DTO;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.WebCode.Attributes
{
    public class HulenAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public HulenAuthorizeAttribute()
        {
            _userRepository = new UserRepository();
            _userService = new UserService(_userRepository);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
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

            return ValidateUserAccess(_userService.GetOneUser(username), callingController, callingAction);
        }

        private static bool ValidateUserAccess(UserDTO user, string callingController, string callingAction)
        {

            return true;

            //if(user != null)
            //{
            //    if (callingController == "Home")
            //        return user.HomeAccessTo;
            //    if (callingController == "UserAdmin")
            //        return user.UserAdminAccessTo;
            //    if (callingController == "AccountInfo")
            //        return user.AccountInfoAccessTo;
            //    if (callingController == "FileImport")
            //        return user.FileImportAccessTo;
            //    if (callingController == "AccessGroup")
            //        return true;
            //    return false;
            //}
            //return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var result = new ViewResult {ViewName = "StayTheFuckAway"};
            filterContext.Result = result;
        }
    }
}

