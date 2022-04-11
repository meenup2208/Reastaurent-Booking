using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.ModelBinding;
using System.Web.Security;

namespace RestaurentMVC.Models
{
    public class UserDBHandler
    {
        private SqlConnection con;

        private void connection()
        {
            string connectionstring = "Data Source=LAPTOP-FCT3Q2DO;Initial Catalog=MeenuDatabase;Integrated Security=True";
            con = new SqlConnection(connectionstring);
        }

        public Dictionary<string, Boolean> UserLogin(string email, string password)
        {
            connection();

            SqlCommand cmd = new SqlCommand("UserLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@LoginEmail", email);
            cmd.Parameters.AddWithValue("@LoginPassword", password);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            Dictionary<string, Boolean> returnObj = new Dictionary<string, Boolean>();
            returnObj.Add("IsValid", false);
            returnObj.Add("IsAdmin", false);

            if (dt.Rows.Count > 0)
            {               
                returnObj["IsValid"]= true;
                returnObj["IsAdmin"]= Convert.ToInt32(dt.Rows[0]["IsAdmin"]) == 1;
            }          
            return returnObj;

        }

        public User ForgetPassword(string email,string name, string place)
        {
            connection();

            SqlCommand cmd = new SqlCommand("ForgetPassword", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@place", place);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            User userObj = new User();

            userObj.UId = Convert.ToInt32(dt.Rows[0]["UId"]);
            userObj.Name = Convert.ToString(dt.Rows[0]["UName"]);
            userObj.Email = Convert.ToString(dt.Rows[0]["UEmail"]);
            userObj.Place = Convert.ToString(dt.Rows[0]["UPlace"]);

            return userObj;

        }

        public bool ResetPassword(int id, string password)
        {
            connection();
            SqlCommand cmd = new SqlCommand("ResetPassword", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UId", id);
            cmd.Parameters.AddWithValue("@password", password);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }



        public List<User> GetAllUser()
        {
            connection();
            List<User> userList = new List<User>();

            SqlCommand cmd = new SqlCommand("GetAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                userList.Add(
                    new User
                    {
                        UId = Convert.ToInt32(dr["UId"]),
                        Name = Convert.ToString(dr["UName"]),
                        ContactNo = Convert.ToString(dr["UPhonenumber"]),
                        Designation = Convert.ToString(dr["UDesignation"]),
                        Email = Convert.ToString(dr["UEmail"]),
                        Place = Convert.ToString(dr["UPlace"]),
                        CreatedBy = Convert.ToInt32(dr["CreatedBy"]),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        ModifiedBy = Convert.ToInt32(dr["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])

                    });
                foreach (var n in userList)
                {
                    var Createduser = GetUserById(n.CreatedBy);
                    var Modifieduser = GetUserById(n.ModifiedBy);
                    n.CreatedName = Createduser.Name;
                    n.ModifiedName = Modifieduser.Name;
                }
            }
            return userList;
        }

        public bool UserRegister(User userObj)
        {           
            connection();          

            SqlCommand cmd = new SqlCommand("UserRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@UName", userObj.Name);
            cmd.Parameters.AddWithValue("@UDesignation", userObj.Designation);
            cmd.Parameters.AddWithValue("@UEmail", userObj.Email);
            cmd.Parameters.AddWithValue("@UPassword", userObj.Password);
            cmd.Parameters.AddWithValue("@UPhonenumber", userObj.ContactNo);
            cmd.Parameters.AddWithValue("@UPlace", userObj.Place);
            cmd.Parameters.AddWithValue("@IsAdmin", userObj.IsAdmin);
            cmd.Parameters.AddWithValue("@Delete", 0);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", userObj.CreatedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", userObj.ModifiedBy);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 0)
            {
                return true;   
            }
            else
            {
                return false;
            }
        }

        public User GetUserById(int UId)
        {
            connection();
            User userObj = new User();

            SqlCommand cmd = new SqlCommand("GetUserById", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UId",UId);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();
            userObj.UId = Convert.ToInt32(dt.Rows[0]["UId"]);
            userObj.Name = Convert.ToString(dt.Rows[0]["UName"]);
            userObj.ContactNo = Convert.ToString(dt.Rows[0]["UPhonenumber"]);
            userObj.Designation = Convert.ToString(dt.Rows[0]["UDesignation"]);
            userObj.Email = Convert.ToString(dt.Rows[0]["UEmail"]);
            userObj.Place = Convert.ToString(dt.Rows[0]["UPlace"]);
                     
            return userObj;
        }

        public User GetUserByEmail(string loggedEmail)
        {
            connection();
            User userObj = new User();

            SqlCommand cmd = new SqlCommand("GetUserByEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UEmail", loggedEmail);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();
            userObj.UId = Convert.ToInt32(dt.Rows[0]["UId"]);
            userObj.Name = Convert.ToString(dt.Rows[0]["UName"]);
            userObj.ContactNo = Convert.ToString(dt.Rows[0]["UPhonenumber"]);
            userObj.Designation = Convert.ToString(dt.Rows[0]["UDesignation"]);
            userObj.Email = Convert.ToString(dt.Rows[0]["UEmail"]);
            userObj.Place = Convert.ToString(dt.Rows[0]["UPlace"]);

            return userObj;
        }

        public bool UpdateUser(User userObj)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UserInfoUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UId", userObj.UId);
            cmd.Parameters.AddWithValue("@UName", userObj.Name);
            cmd.Parameters.AddWithValue("@UDesignation", userObj.Designation);
            cmd.Parameters.AddWithValue("@UPhonenumber", userObj.ContactNo);
            cmd.Parameters.AddWithValue("@UPlace", userObj.Place);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", userObj.ModifiedBy);
           
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteUser(int UId)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteUserInfo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UId", UId);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool IsExistingUser(string email)
        {
            connection();
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