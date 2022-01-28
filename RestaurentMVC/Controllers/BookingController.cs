using System;
using System.Collections.Generic;
using System.Linq;
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
                    RestaurentBook sdb = new RestaurentBook();
                    if (sdb.AddBookings(bmodel))
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
                RestaurentBook sdb = new RestaurentBook();
                Booking booking = sdb.GetBookingsById(Bookid);

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



        [HttpPost]
        public ActionResult Edit(Booking bmodel)
        {
            try
            {

                
                if (ModelState.IsValid)
                {
                    RestaurentBook sdb = new RestaurentBook();
                    if (sdb.UpdateDetails(bmodel))
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
                RestaurentBook sdb = new RestaurentBook();
                Booking booking = sdb.GetBookingsById(Bookid);
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
                RestaurentBook sdb = new RestaurentBook();
                if (sdb.DeleteBooking(Bookid))
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
                RestaurentBook obj = new RestaurentBook();

                var pagenumber = Convert.ToInt32(Request.Form["start"]);
                var pagesize = Convert.ToInt32( Request.Form["length"]);
                var search = Request.Form["search[value]"];

                List<Booking> List = obj.GetBookings(pagenumber,pagesize,search);

                return Json(new { data = List }, JsonRequestBehavior.AllowGet);


            }
            catch
            {
                return View();
            }
        }

    }

}