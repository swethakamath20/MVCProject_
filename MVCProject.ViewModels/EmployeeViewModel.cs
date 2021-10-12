using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCProject.ViewModels
{
    public class EmployeeViewModel
    {
        public int employeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public long ContactNumber { get; set; }
        public int RoleID { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public string ProjectManagerName { get; set; }
    }
}
