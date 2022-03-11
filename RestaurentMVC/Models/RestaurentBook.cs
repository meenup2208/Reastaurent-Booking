using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurentMVC.Models
{
    public class RestaurentBook
    {
        private SqlConnection con;

        private void connection()
        {
            string connectionstring = "Data Source=LAPTOP-5IQ1TLRU;Initial Catalog=BookingDatabase;Integrated Security=True";
            //string constring = ConfigurationManager.ConnectionStrings["BookingsConn"].ToString();
            con = new SqlConnection(connectionstring);
        }


        public bool AddBookings(Booking smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateBookings", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Bookid", smodel.Bookid);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Phone", smodel.Phone);
            cmd.Parameters.AddWithValue("@TypeOfDining", smodel.TypeOfDining);
            cmd.Parameters.AddWithValue("@Date", smodel.Date);
            cmd.Parameters.AddWithValue("@Time", smodel.Time);
            cmd.Parameters.AddWithValue("@Guest", smodel.Guest);
            cmd.Parameters.AddWithValue("@Delete", 0);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public List<Booking> GetBookings(int pagenumber, int pagesize, string search)
        {
            connection();
            List<Booking> list = new List<Booking>();

            SqlCommand cmd = new SqlCommand("GetBookings", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pagenumber", pagenumber);
            cmd.Parameters.AddWithValue("@pagesize", pagesize);
            cmd.Parameters.AddWithValue("@search", search);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(
                    new Booking
                    {
                        Bookid = Convert.ToInt32(dr["Bookid"]),
                        Name = Convert.ToString(dr["Name"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        TypeOfDining = Convert.ToString(dr["TypeOfDining"]),
                        Date = Convert.ToDateTime(dr["Date"]),
                        Time = Convert.ToString(dr["Time"]),
                        Guest = Convert.ToInt32(dr["Guest"]),

                    });
            }
            return list;
        }


        public bool UpdateDetails(Booking smodel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateBookings", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Bookid", smodel.Bookid);
            cmd.Parameters.AddWithValue("@Name", smodel.Name);
            cmd.Parameters.AddWithValue("@Phone", smodel.Phone);
            cmd.Parameters.AddWithValue("@TypeOfDining", smodel.TypeOfDining);
            cmd.Parameters.AddWithValue("@Date", smodel.Date);
            cmd.Parameters.AddWithValue("@Time", smodel.Time);
            cmd.Parameters.AddWithValue("@Guest", smodel.Guest);
            cmd.Parameters.AddWithValue("@Delete", smodel.Delete);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }


        public bool DeleteBooking(int Bookid)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteBookings", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Bookid", Bookid);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public Booking GetBookingsById(int bookingid)
        {
            connection();
            Booking booking = new Booking();

            SqlCommand cmd = new SqlCommand("GetBookingsById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bookid", bookingid);

            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();
            //booking.Bookid = ;
            booking.Bookid = Convert.ToInt32(dt.Rows[0]["Bookid"]);
            booking.Name = Convert.ToString(dt.Rows[0]["Name"]);
            booking.Phone = Convert.ToString(dt.Rows[0]["Phone"]);
            booking.TypeOfDining = Convert.ToString(dt.Rows[0]["TypeOfDining"]);
            booking.Date = Convert.ToDateTime(dt.Rows[0]["Date"]);
            booking.Time = Convert.ToString(dt.Rows[0]["Time"]);
            booking.Guest = Convert.ToInt32(dt.Rows[0]["Guest"]);

            return booking;

        }
        public List<Booking> AllBookings(string search)
        {
            connection();
            List<Booking> list = new List<Booking>();

            SqlCommand cmd = new SqlCommand("AllBookings", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@search", search);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(
                    new Booking
                    {
                        Bookid = Convert.ToInt32(dr["Bookid"]),
                        Name = Convert.ToString(dr["Name"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        TypeOfDining = Convert.ToString(dr["TypeOfDining"]),
                        Date = Convert.ToDateTime(dr["Date"]),
                        Time = Convert.ToString(dr["Time"]),
                        Guest = Convert.ToInt32(dr["Guest"]),

                    });
            }
            return list;
        }

        public List<Booking> GETALLBOOKINGS()
        {
            connection();
            List<Booking> list = new List<Booking>();

            SqlCommand cmd = new SqlCommand("GETALLBOOKINGS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            sd.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(
                    new Booking
                    {
                        Bookid = Convert.ToInt32(dr["Bookid"]),
                        Name = Convert.ToString(dr["Name"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        TypeOfDining = Convert.ToString(dr["TypeOfDining"]),
                        Date = Convert.ToDateTime(dr["Date"]),
                        Time = Convert.ToString(dr["Time"]),
                        Guest = Convert.ToInt32(dr["Guest"]),

                    });
            }
            return list;
        }

    }
}