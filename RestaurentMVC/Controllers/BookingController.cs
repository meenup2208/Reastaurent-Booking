using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingClassLibrary;
namespace RestaurentMVC.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            return View();
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
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {

                return View();
            }
        }


        public ActionResult Edit(int Bookid)
        {

            try
            {
                RestaurentBook sdb = new RestaurentBook();
                return View(sdb.GetBookingsById(Bookid));
            }
            catch
            {
                return View();
            }
        }

        

        [HttpPost]
        public ActionResult Edit(int Bookid, Booking bmodel)
        {
            try
            {

                RestaurentBook sdb = new RestaurentBook();
                sdb.UpdateDetails(bmodel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        
        
        public ActionResult Delete(int? Bookid)
        {
            try
            {
                RestaurentBook sdb = new RestaurentBook();
                return View(sdb.GetBookingsById((int)Bookid));
            }
            catch
            {
                return View();
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
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
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