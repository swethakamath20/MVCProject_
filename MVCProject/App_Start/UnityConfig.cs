using System.Web.Http;
using Unity;
using Unity.WebApi;
using MVCProject.ServiceLayer;
using System.Web.Mvc;

namespace MVCProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<IEmployeeService, EmployeeService>();
            
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}