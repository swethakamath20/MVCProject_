﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCProject.ViewModels
{
     public class EditEmployeeViewModel
    {
        public int EmployeeID { get; set; }

        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string LastName { get; set; }

        [Required]
        public long Mobile { get; set; }
        public string Designation { get; set; }
        public string ProjectManagerName { get; set; }
    }
}
