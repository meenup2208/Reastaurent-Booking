using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RestaurentMVC.Models;


namespace UserWebApi
{
    public class UserApiDBHandler
    {
        private SqlConnection con;

        private void connection()
        {
            string connectionstring = "Data Source=LAPTOP-FCT3Q2DO;Initial Catalog=MeenuDatabase;Integrated Security=True";
            con = new SqlConnection(connectionstring);
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
                        CreatedBy = Convert.ToString(dr["CreatedBy"]),
                        CreatedDate = Convert.ToDateTime(dr["CreatedDate"]),
                        ModifiedBy = Convert.ToString(dr["ModifiedBy"]),
                        ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"])

                    });
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
            cmd.Parameters.AddWithValue("@IsAdmin", 0);
            cmd.Parameters.AddWithValue("@IsDelete", 0);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CreatedBy", userObj.CreatedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@ModifiedBy", userObj.ModifiedBy);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
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

            cmd.Parameters.AddWithValue("@UId", UId);

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
            cmd.Parameters.AddWithValue("@UEmail", userObj.Email);
            cmd.Parameters.AddWithValue("@UPassword", userObj.Password);
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