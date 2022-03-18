using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

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

        public bool UserLogin(string email, string password)
        {
            connection();
            bool IsValid = false;

            SqlCommand cmd = new SqlCommand("UserLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //string query = "select * from UserInfoTable where UEmail=@Email and UPassword=@Password";
            //using (SqlCommand cmd = new SqlCommand(query, con))

            // {
            cmd.Parameters.AddWithValue("@LoginEmail", email);
            cmd.Parameters.AddWithValue("@LoginPassword", password);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (dt.Rows.Count > 0)
            {
                IsValid = true;
            }
            //}
            return IsValid;

        }

    }
}