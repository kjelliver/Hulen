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
using Hulen.Objects.Mappers;
using Hulen.Objects.Mappers.Interfaces;
using Hulen.Storage.Interfaces;
using Hulen.Storage.Repositories;

namespace Hulen.WebCode.Attributes
{
    public class HulenAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IAccessGroupService _accessGroupService;
        private readonly IAccessGroupRepository _accessGroupRepository;
        private readonly IAccessGroupMapper _accessGroupMapper;

        public HulenAuthorizeAttribute()
        {
            _userRepository = new UserRepository();
            _accessGroupRepository = new AccessGroupRepository();
            _accessGroupMapper = new AccessGroupMapper();
            _accessGroupService = new AccessGroupService(_accessGroupRepository, _accessGroupMapper);
            _userService = new UserService(_userRepository);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string callingController = "";
            string callingAction = "";
            List<string> accessGroups = new List<string>();

            if (httpContext.Session != null)
            {
                callingController = RouteTable.Routes.GetRouteData(httpContext).Values["controller"].ToString();
                callingAction = RouteTable.Routes.GetRouteData(httpContext).Values["action"].ToString();
                accessGroups = (List<string>) httpContext.Session["accessGroups"];

            }

            return _userService.HasUserAccessTo(callingController, callingAction, accessGroups);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var result = new ViewResult {ViewName = "StayTheFuckAway"};
            filterContext.Result = result;
        }
    }
}

