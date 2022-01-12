using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClassLibrary
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
        public string Date { get; set; }

        public string Time { get; set; }

        [Range(1, 100, ErrorMessage = "100 Guest maximum")]
        public int Guest { get; set; }

        public bool Delete { get; set; }
    }
    public enum Operation
    {
        Create,
        View,
        Edit,
        Delete
    }
}
