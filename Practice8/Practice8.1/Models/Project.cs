using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice8._1.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 2)]
        public string ProjectName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProjectStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ProjectEndDate { get; set; }

        public virtual ICollection<ProjectEmployee> ProjectEmployees { get; set; }
    }
}