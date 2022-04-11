using BookingClassLibrary.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace RestaurentMVC.Models
{
    public class User
    {
        public int UId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression("[0-9]{10}", ErrorMessage = "Phone Number is not valid")]
        public string ContactNo { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        //[EmailAddress]
        //[StringLength(150)]
        //[Display(Name = "Email Address: ")]
        public string Email { get; set; }

        [Required]
        //[DataType(DataType.Password)]
        //[StringLength(150, MinimumLength = 6)]
        //[Display(Name = "Password: ")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsDelete { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Operations operations { get; set; }


    }
}