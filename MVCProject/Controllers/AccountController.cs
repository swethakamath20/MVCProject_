using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.ViewModels;
using MVCProject.ServiceLayer;
using MVCProject.DomainModels;
using System.Data.Entity;
using MVCProject.Custom_Filters;

namespace MVCProject.Controllers
{
    public class AccountController : Controller
    {
        IEmployeeService es;
        public AccountController(EmployeeService es)
        {
            this.es = es;
        }
        // GET: Account
        public ActionResult Login()
        {
            LoginViewModel lvm = new LoginViewModel();
            return View(lvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel lvm)
        {
            if(ModelState.IsValid)
            {
                EmployeeViewModel evm = es.GetEmployeesByEmailAndPassword(lvm.Email, lvm.Password);
                if(evm!=null)
                {
                    Session["CurrentEmployeeID"] = evm.employeeID;
                    Session["CurrentEmployeeEmail"] = evm.Email;
                    Session["CurrentEmployeeName"] = evm.FirstName +" "+ evm.LastName;
                    Session["CurrentEmployeePassword"] = evm.PasswordHash;
                    Session["CurrentEmployeeContact"] = evm.ContactNumber;
                    Session["CurrentEmployeeDesignation"] = evm.Designation;
                    Session["CurrentEmployeeGender"] = evm.Gender;
                    Session["CurrentEmployeePM"] = evm.ProjectManagerName;
                    Session["CurrentEmployeeRoleID"] = evm.RoleID;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("x", "Invalid Email / Password");
                    return View(lvm);
                }
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(lvm);
            }
        }
        public ActionResult MyProfile()
        {
            return View();
        }
        [EmployeeAuthorizationFilter]
        public ActionResult EditProfile()
        {
            int eid = Convert.ToInt32(Session["CurrentEmployeeID"]);
            EmployeeViewModel evm= this.es.GetEmployeesByEmployeeID(eid);
            EditEmployeeViewModel eevm = new EditEmployeeViewModel() { FirstName = evm.FirstName, LastName=evm.LastName, Email = evm.Email, Mobile = evm.ContactNumber,EmployeeID=evm.employeeID};
            return View(eevm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [EmployeeAuthorizationFilter]
        public ActionResult EditProfile(EditEmployeeViewModel eevm)
        {
            if(ModelState.IsValid)
            {
                eevm.EmployeeID = Convert.ToInt32(Session["CurrentEmployeeID"]);
                this.es.UpdateEmployeeDetails(eevm);
                Session["CurrentEmployeeName"] = eevm.FirstName+" "+eevm.LastName;
                Session["CurrentEmployeeContact"] = eevm.Mobile;
                

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eevm);
            }
        }
        [HRAuthorizationFilter]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(EmployeeViewModel evm)
        { 
            if(ModelState.IsValid)
            {
                int eid = this.es.InsertEmployee(evm);
                return RedirectToAction("index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(evm);
            }
        }
        [HRAuthorizationFilter]
        public ActionResult ViewAllEmployees()
        {
            List<EmployeeViewModel> levm=this.es.GetEmployees();
            return View(levm);
        }
        [HRAuthorizationFilter]
        public ActionResult Update(int id)
        {
            
            EmployeeViewModel evm = this.es.GetEmployeesByEmployeeID(id);
            EditEmployeeViewModel eevm = new EditEmployeeViewModel() { FirstName = evm.FirstName, LastName = evm.LastName, Email = evm.Email, Mobile = evm.ContactNumber, EmployeeID = evm.employeeID };
            return View(eevm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EditEmployeeViewModel eevm)
        {
            if (ModelState.IsValid)
            {
                
                this.es.UpdateEmployeeDetails(eevm);
                
                return RedirectToAction("ViewAllEmployees", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(eevm);
            }
        }
        [HRAuthorizationFilter]
        public ActionResult DeleteEmployee(int id)
        {
            es.DeleteEmployee(id);
            return RedirectToAction("ViewAllEmployees","Account");
        }
        [EmployeeAuthorizationFilter]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}