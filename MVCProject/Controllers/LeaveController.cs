using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.ViewModels;
using MVCProject.ServiceLayer;
using MVCProject.Custom_Filters;
using System.Net.Mail;
using System.Net;


namespace MVCProject.Controllers
{
    public class LeaveController : Controller
    {
        ILeaveService ls;
        IEmployeeService es;
        public LeaveController(LeaveService ls,EmployeeService es)
        {
            this.ls = ls;
            this.es = es;
        }
        [EmployeeAuthorizationFilter]
        public ActionResult LeaveRequest()
        {
            LeaveViewModel lvm = new LeaveViewModel();
            return View(lvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveRequest(LeaveViewModel lvm)
        { 
            lvm.EmployeeID = Convert.ToInt32(Session["CurrentEmployeeID"]);
            lvm.EmployeeName = Convert.ToString(Session["CurrentEmployeeName"]);
            lvm.designation = Convert.ToString(Session["CurrentEmployeeDesignation"]);
            lvm.LeaveStatus = "Pending";
            this.ls.InsertLeaveRequest(lvm);
            
            return RedirectToAction("Index", "Home");
        }
        [PMAuthorizationFilter]
        public ActionResult ViewLeaveRequests()
        { 
       
            List<LeaveViewModel> requests = this.ls.GetLeaves();
            
            return View(requests);
        }
        //[HttpPost]
        //public ActionResult UpdateLeaveRequests(LeaveViewModel requests)
        //{

        //    this.ls.UpdateLeaveStatusByLeaveID(requests);

        //    return RedirectToAction("ViewLeaveRequests","leave");
        //}
        [HttpPost]
        [HRAndPMAuthorizationFilter]
        public ActionResult LeaveUpdation(LeaveViewModel requests)
        {

            MailViewModel MailViewModel = this.ls.UpdateLeaveStatusByLeaveID(requests);
            try
            {
                var senderEmail = new MailAddress("mvcp990@gmail.com", "mvc");
                var receiverEmail = new MailAddress(MailViewModel.Email, "Receiver");
                var password = "mvcp1234";
                var sub = MailViewModel.LeaveStatus + " your leave request";
                var body = MailViewModel.FirstName + ", your leave request has been " + MailViewModel.LeaveStatus;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("ViewLeaveRequests", "Leave");


        }


        // public ActionResult Details()
        // {

        //     this.ls.GetLeaveByLeaveID(lid);
        //     return View();
        //}
        [EmployeeAuthorizationFilter]
        public ActionResult LeaveStatus()
        {
            int eid = Convert.ToInt32(Session["CurrentEmployeeID"]);
            List<LeaveViewModel> lvm=this.ls.GetLeavesByEmployeeID(eid);
            return View(lvm);
        }
    }
}