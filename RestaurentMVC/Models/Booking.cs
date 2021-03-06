using BookingClassLibrary.Enum;
using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurentMVC.Models
{
    public class Booking
    {
        public int Bookid { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }

        [RegularExpression("[0-9]{10}", ErrorMessage = "Phone Number is not valid")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Specify Dining Type.")]
        public string TypeOfDining { get; set; }

        [Date]
        [Required(ErrorMessage = "dd/mm/yyyy")]
        public DateTime Date { get; set; }

        public string Time { get; set; }

        [Range(1, 100, ErrorMessage = "100 Guest maximum")]
        public int Guest { get; set; }

        public bool Delete { get; set; }

        public int CreatedBy { get; set; }
        public string CreatedName { get; set; }

        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedName { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Operations operations { get; set; }

    }
    
}