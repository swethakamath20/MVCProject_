using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.DomainModels
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public long ContactNumber { get; set; }
        public string Designation { get; set; }
        public int RoleID { get; set; }
        public string Gender { get; set; }
        public string ProjectManagerName { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
        

        
    }
}
