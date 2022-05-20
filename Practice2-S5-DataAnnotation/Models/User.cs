using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Practice2_S5_DataAnnotation.Models
{
    public class User
    {
        [Required][StringLength(500,MinimumLength = 7)]
        public string Name { get; set; }
        [Required][DataType(DataType.Password)][StringLength(500,MinimumLength = 6)]
        public string Password  { get; set; }
        [Required][DataType(DataType.Password)][Compare("Password", ErrorMessage = "Nhap sai pass")]
        public string ConfirmPassword { get; set; }
        [Required][RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        public string Email { get; set; }
    }
}