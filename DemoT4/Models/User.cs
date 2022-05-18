using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoT4.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string  Email { get; set; }
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

    }
}