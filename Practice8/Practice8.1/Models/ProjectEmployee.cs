using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice8._1.Models
{
    public class ProjectEmployee
    {   
        public int ProjectEmployeeId { get; set; }

        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        [Required]
        public string Task { get; set; }

        public Project Project { get; set; }

        public Employee Employee { get; set; }
    }
}