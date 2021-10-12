using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.ViewModels;
using MVCProject.ServiceLayer;
using MVCProject.Custom_Filters;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeService es;
        public HomeController(EmployeeService es)
        {
            this.es = es;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [EmployeeAuthorizationFilter]
        public ActionResult Search(string str,int RoleID)
        {
            
            if(RoleID== 1)
            {
                List<EmployeeViewModel> employees = this.es.GetEmployees().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower())&& (temp.RoleID==1)).ToList();
                ViewBag.str = str;
                return View(employees);

            }
            else if (RoleID == 2)
            {
                List<EmployeeViewModel> employees = this.es.GetEmployees().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower()) && temp.RoleID == 2).ToList();
                ViewBag.str = str;
                return View(employees);

            }
            else if (RoleID == 3)
            {
                List<EmployeeViewModel> employees = this.es.GetEmployees().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower()) && temp.RoleID == 3).ToList();
                ViewBag.str = str;
                return View(employees);

            }
            else
            {
                List<EmployeeViewModel> employee = this.es.GetEmployees().Where(temp => temp.FirstName.ToLower().Contains(str.ToLower())).ToList();
                ViewBag.str = str;
                return View(employee);
            }
        }
       
        public ActionResult ProfileView(int id)
        {
            EmployeeViewModel evm= this.es.GetEmployeesByEmployeeID(id);
            return View(evm);
        }
        //public ActionResult Contact()
        //        {
        //            return View();
        //        }
    }
}