using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using RestaurentMVC.Models;
using BookingClassLibrary.Enum;


namespace RestaurentMVC.Controllers
{
    public class BookingController : Controller
    {
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

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {

                return Json(true, JsonRequestBehavior.AllowGet);
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
                return PartialView();
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
                return PartialView();
            }
        }

        [HttpPost]
        public ActionResult Edit(Booking booking)
        {
            try
            {

                
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

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return PartialView();
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
                return PartialView();
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
                return PartialView();
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

                List<Booking> All_list = restaurentBook.AllBookings();
                
                int recordsTotal = 0;
                List<Booking> List = restaurentBook.GetBookings(start, length, search);
                List = List.OrderBy(sortColumnName + " " + sortDirection).ToList<Booking>();

                recordsTotal = All_list.Count();
               
                
                
                return Json(new { recordsTotal = recordsTotal, recordsFiltered = recordsTotal,  data = List}, JsonRequestBehavior.AllowGet);


            }
            catch
            {
                return PartialView();
            }
        }

    }

}