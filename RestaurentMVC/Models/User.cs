using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurentMVC.Models
{
    public class User
    {
        public String Name { get; set; }

        [Display(Name = "Phone Number")]
        public string ContactNo { get; set; }

        public string Place { get; set; }
        //[Required]
        //[EmailAddress]
        //[StringLength(150)]
        //[Display(Name = "Email Address: ")]
        public string Email { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[StringLength(150, MinimumLength = 6)]
        //[Display(Name = "Password: ")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}