using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Custom_Filters
{
    public class HRAndPMAuthorizationFilter:FilterAttribute,IAuthorizationFilter
    {
            public void OnAuthorization(AuthorizationContext filterContext)
            {
                if (filterContext.RequestContext.HttpContext.Session["CurrentEmployeeRoleID"].ToString() != "2" && filterContext.RequestContext.HttpContext.Session["CurrentEmployeeRoleID"].ToString() != "3")
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Index" }));
                }
            }
        }
    
}