using RestaurentMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingClassLibrary.Enum;
using System.Web.Security;
using System.Web.Services.Description;

namespace RestaurentMVC.Controllers
{
    public class UserController : Controller
    {       
        // GET:Login this Action method simple return the Login View
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(User userObj)
        {
            UserDBHandler dbObj = new UserDBHandler();
            User user = dbObj.ForgetPassword(userObj.Email, userObj.Name, userObj.Place);
            
            if (user!=null)
            {
                var reset = dbObj.ResetPassword(user.UId,userObj.Password);
                return RedirectToAction("Login", "User");

            }
                      
            else
            {
                ModelState.AddModelError("", "Your Account doesnot Exist");
            }
            return View(userObj);
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();

        }


        //Post:When user click on the submit button then this method will call
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User userObj)
        {
            UserDBHandler dbObj = new UserDBHandler();
            Dictionary<string, Boolean> usercheck =dbObj.UserLogin(userObj.Email, userObj.Password);
            if (usercheck["IsValid"])
            {
                if (usercheck["IsAdmin"])
                {
                    FormsAuthentication.SetAuthCookie(userObj.Email, false);
                    return RedirectToAction("Dashboard", "User");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userObj.Email, false);
                    return RedirectToAction("Index", "Booking");
                }
            }
            else
            {
                ModelState.AddModelError("", "Your Account doesnot Exist");
            }
                return View(userObj);       
        }


        [Authorize]
        public ActionResult AdminBooking()
        {
            return View();
        }

        [Authorize]
        public ActionResult UserInfo()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserDetails()
        {
            UserDBHandler dbObj = new UserDBHandler();
            List<User> UserList = dbObj.GetAllUser();
            return Json(new { data = UserList }, JsonRequestBehavior.AllowGet);
        }



            [Authorize]
        [HttpGet]
        public ActionResult Register(string Operation)
        {
            User userObj = new User();
            if (Operation == Operations.Create.ToString())
            {
                userObj.operations = Operations.Create;
            }
            return PartialView("_UserOperation", userObj);
        }

        // Post:Register 
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User userObj)
        {
            try
            {
            string loggedEmail = User.Identity.Name;
                UserDBHandler dbObj = new UserDBHandler();
                User getObj = dbObj.GetUserByEmail(loggedEmail);
                int loggedBY= getObj.UId;
                userObj.CreatedBy = loggedBY;
                userObj.ModifiedBy = loggedBY;

            if (ModelState.IsValid)
                {

                    if (!dbObj.IsExistingUser(userObj.Email))
                    {

                        if (dbObj.UserRegister(userObj))
                        {
                            FormsAuthentication.SetAuthCookie(userObj.Email, false);
                            return RedirectToAction("UserInfo", "User");
                        }
                        else    {
                            ModelState.AddModelError("", "something went wrong try later!");
                        }
                    }
                    else   {
                        ModelState.AddModelError("", "User with same email already exist!");
                    }
                }
                else    {
                    ModelState.AddModelError("", "Invalid EmailId or Password");
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }


        [HttpGet]
        public ActionResult UserEdit(string Operation, int UId)
        {
            try
            {
                UserDBHandler dbObj = new UserDBHandler();
                User userObj = dbObj.GetUserById(UId);
                if (Operation == Operations.Edit.ToString())
                {
                    userObj.operations = Operations.Edit;
                }
                return PartialView("_UserOperation", userObj);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        [HttpGet]
        public ActionResult UserView(string Operation, int UId)
        {
            try{
                UserDBHandler dbObj = new UserDBHandler();
                User userObj = dbObj.GetUserById(UId);

                if (Operation == Operations.View.ToString())
                {
                    userObj.operations = Operations.View;
                }
                return PartialView("_UserOperation", userObj);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditSave(User userObj)
        {
            try
            {
                string loggedEmail = User.Identity.Name;
                UserDBHandler dbObj = new UserDBHandler();

                User getObj = dbObj.GetUserByEmail(loggedEmail);
                int loggedBY = getObj.UId;
                userObj.ModifiedBy = loggedBY;

                if (dbObj.UpdateUser(userObj))
                {
                    ViewBag.Message = "Customer Details Updated Successfully";
                }
                return Json(true, JsonRequestBehavior.AllowGet);                               
               
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }


        [HttpGet]
        public ActionResult DeleteUser(string operation, int UId)
        {
            try
            {
                UserDBHandler dbObj = new UserDBHandler();
                User userObj = dbObj.GetUserById(UId);
                if (operation == Operations.Delete.ToString())
                {
                    userObj.operations = Operations.Delete;
                }
                return PartialView("_UserDelete", userObj);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(int UId)
        {
            try
            {
                UserDBHandler dbObj = new UserDBHandler();
                if (dbObj.DeleteUser(UId))
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
    }
}
