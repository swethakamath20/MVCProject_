using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCProject.DomainModels
{
    public class Leave
    {
        [Key]
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }
        public string LeaveReason { get; set; }
        public string LeaveStatus { get; set; }


        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        //public ICollection<Employee> Employees;
        
    }
}
