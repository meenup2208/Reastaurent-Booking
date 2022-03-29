using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using RestaurentMVC.Models;
using BookingClassLibrary.Enum;
using ArrayToPdf;
using System.Data;

namespace RestaurentMVC.Controllers
{
    public class BookingController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(string Operation)
        {
            Booking booking = new Booking();
           
            if (Operation == Operations.Create.ToString())
            {
                booking.operations = Operations.Create;
            }
            return PartialView("_Operations", booking);
        }

        [HttpPost]
        public ActionResult Create(Booking bmodel)
        {
            try
            {
                string loggedBY = User.Identity.Name;
                bmodel.CreatedBy = loggedBY;
                bmodel.ModifiedBy = loggedBY;


                if (ModelState.IsValid)
                {
                    RestaurentBook restaurentBook = new RestaurentBook();
                    if (restaurentBook.AddBookings(bmodel))
                    {
                        ViewBag.Message = "Booking Created Successfully";
                        ModelState.Clear();
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return View();
                }
            }
            catch
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(string Operation, int Bookid)
        {

            try
            {
                RestaurentBook restaurentBook = new RestaurentBook();
                Booking booking = restaurentBook.GetBookingsById(Bookid);
               
                if (Operation == Operations.Edit.ToString())
                {
                    booking.operations = Operations.Edit;
                }
                return PartialView("_Operations", booking);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult View(string Operation, int Bookid)
        {

            try
            {
                RestaurentBook restaurentBook = new RestaurentBook();
                Booking booking = restaurentBook.GetBookingsById(Bookid);
                
                if (Operation == Operations.View.ToString())
                {
                    booking.operations = Operations.View;
                }
                return PartialView("_Operations", booking);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            try
            {
                string loggedBy = User.Identity.Name;
                booking.ModifiedBy = loggedBy;
                if (ModelState.IsValid)
                {
                    RestaurentBook restaurentBook = new RestaurentBook();
                    if (restaurentBook.UpdateDetails(booking))
                    {
                        ViewBag.Message = "Booking Updated";
                        ModelState.Clear();
                    }
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }




        [HttpGet]
        public ActionResult Delete(string Operation,int Bookid)
        {
            try
            {
                RestaurentBook restaurentBook = new RestaurentBook();
                Booking booking = restaurentBook.GetBookingsById(Bookid);
                if (Operation == Operations.Delete.ToString())
                {
                    booking.operations = Operations.Delete;
                }
                return PartialView("_Delete", booking);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(int Bookid)
        {
            try
            {
                RestaurentBook restaurentBook = new RestaurentBook();
                if (restaurentBook.DeleteBooking(Bookid))
                {
                    ViewBag.AlertMsg = " Deleted Successfully";
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult Bookinglist()

        {
            try
            {
                RestaurentBook restaurentBook = new RestaurentBook();
                
                var start = Convert.ToInt32(Request.Form["start"]);
                var length = Convert.ToInt32( Request.Form["length"]);
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][data]"];
                string sortDirection = Request["order[0][dir]"];
                var search = Request.Form["search[value]"];

                List<Booking> All_list = restaurentBook.AllBookings(search);                
                int recordsTotal = 0;
                recordsTotal = All_list.Count();

                List<Booking> List = restaurentBook.GetBookings(start, length, search);
                List = List.OrderBy(sortColumnName + " " + sortDirection).ToList<Booking>();            
                               
                return Json(new { recordsTotal = recordsTotal, recordsFiltered = recordsTotal,  data = List}, JsonRequestBehavior.AllowGet);

            }
            catch(Exception ex)
            {
                string error = ex.Message;
                throw;

            }
        }

        [HttpGet]
        public ActionResult Print()
        {
            RestaurentBook restaurentBook = new RestaurentBook();
            List<Booking> objbooking = restaurentBook.GETALLBOOKINGS();

            var table = new DataTable("Booking Table");
            table.Columns.Add("Booking ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("TypeOfDining", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));
            table.Columns.Add("Time", typeof(string));
            table.Columns.Add("Guest", typeof(int));

            foreach (Booking booking in objbooking)
                table.Rows.Add(booking.Bookid, booking.Name, booking.Phone, booking.TypeOfDining, booking.Date, booking.Time, booking.Guest);

            var pdf = table.ToPdf();
            System.IO.File.WriteAllBytes(@"C:\Users\user\source\repos\Meenu_Restaurant\Reastaurent-Booking\RestaurentMVC\Pdf\result.pdf", pdf);
            return PartialView("_Printview");

        }


    }

}