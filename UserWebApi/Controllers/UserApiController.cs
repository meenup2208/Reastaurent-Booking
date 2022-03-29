using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Http.Results;
using RestaurentMVC.Models;
using Newtonsoft.Json;

namespace UserWebApi.Controllers
{
    [RoutePrefix("api/User")]
    public class UserApiController : ApiController
    {
        [HttpGet]
        [Route("GetUser")]
        public string UserDetails()
        {
            try
            {
                UserDBHandler dbObj = new UserDBHandler();
            List<User> UserList = dbObj.GetAllUser();
            return JsonConvert.SerializeObject(UserList);
            }
            catch (Exception ex)
            {
                string excep = ex.Message;
                return excep;
            }
        }



        [HttpGet]
        [Route("GetUserById")]
        public string GetUserByID(int id)
        {
            UserDBHandler dbObj = new UserDBHandler();
            User userObj = dbObj.GetUserById(id);
            return JsonConvert.SerializeObject(userObj);
        }


        // Post:Register 
        [HttpPost]
        [Route("CreateUsers")]
        public string CreateUsers(User userObj)
        {
          
                string loggedBY = User.Identity.Name;
                userObj.CreatedBy = loggedBY;
                userObj.ModifiedBy = loggedBY;

                UserDBHandler dbObj = new UserDBHandler();
            if (ModelState.IsValid)
            {
                if (!dbObj.IsExistingUser(userObj.Email))
                {

                    if (dbObj.UserRegister(userObj))
                    {
                        return "Registration Successfull";
                    }
                    else
                    {
                        return "Registration UnSuccessfull";
                    }
                }
                else
                {
                    return "User with same email already exist!";
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid EmailId or Password");
            }
            return "Sucsess";
        }




        [HttpPut]
        [Route("UpdateUser")]
        public string EditSave([FromBody] User userObj)
        {
            try
            {
                bool result = false;
                UserDBHandler dbObj = new UserDBHandler();
                User dataObj = dbObj.GetUserById(userObj.UId);
                userObj.ModifiedBy = User.Identity.Name;
                result = dbObj.UpdateUser(userObj);
                ModelState.Clear();                              
                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }
        }



        [HttpDelete]
        [Route("Delete")]
        public string Delete(int UId)
        {
            
                bool result = false;
                UserDBHandler dbObj = new UserDBHandler();
            result = dbObj.DeleteUser(UId);
           
            return JsonConvert.SerializeObject(result);

        }
    }
}