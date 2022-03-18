using RestaurentMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RestaurentMVC.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-FCT3Q2DO;Initial Catalog=MeenuDatabase;Integrated Security=True");


        // GET:Login this Action method simple return the Login View
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Post:When user click on the submit button then this method will call
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User userObj)
        {
            if (ModelState.IsValid)
            {
                UserDBHandler dbObj = new UserDBHandler();
                if (dbObj.UserLogin(userObj.Email, userObj.Password))
                {
                    FormsAuthentication.SetAuthCookie(userObj.Email, false);
                    return RedirectToAction("Index", "Booking");
                    //ViewBag.Message = "Login Successfull";
                    //ModelState.Clear();
                }
                else
                {
                    ModelState.AddModelError("", "Your Account doesnot Exist");                    
                }
                return View(userObj);
            }
            else
            {
                return View(userObj);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Post:Register 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User userObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //checking if user already exist
                    if (!IsUserExist(userObj.Email))
                    {
                        string query = "insert into UserInfoTable (UId,UName,UEmail,UPassword,UPhonenumber,UPlace) values (3,@UName,@UEmail,@UPassword,@UPhonenumber,@UPlace)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Connection = con;
                            cmd.Parameters.AddWithValue("@UName", userObj.Name);
                            cmd.Parameters.AddWithValue("@UEmail", userObj.Email);
                            cmd.Parameters.AddWithValue("@UPassword", userObj.Password);
                            cmd.Parameters.AddWithValue("@UPhonenumber", userObj.ContactNo);
                            cmd.Parameters.AddWithValue("@UPlace", userObj.Place);


                            con.Open();
                            int i = cmd.ExecuteNonQuery();
                            con.Close();
                            if (i > 0)
                            {
                                FormsAuthentication.SetAuthCookie(userObj.Email, false);
                                return RedirectToAction("Index", "Booking");
                            }
                            else
                            {
                                ModelState.AddModelError("", "something went wrong try later!");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "User with same email already exist!");
                    }


                }
                else
                {
                    ModelState.AddModelError("", "Invalid EmailId or Password");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }



        private bool IsUserExist(string email)
        {
            bool IsUserExist = false;
            string query = "select * from UserInfoTable where UEmail=@Email";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    IsUserExist = true;
                }
            }
            return IsUserExist;
        }

    }
}
