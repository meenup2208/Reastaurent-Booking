using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingClassLibrary;
using Newtonsoft.Json;

namespace RestBookAPI.Controllers
{
    public class RestAPIController : ApiController
    {

        RestaurentBook restaurentBook = new RestaurentBook();
        // GET: api/RestAPI
        public IHttpActionResult Get(int pagenumber,int rowcount)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(restaurentBook.GetBookings(pagenumber, rowcount)));
                
            }
            catch
            {
                return NotFound();
            }

        }


        // POST: api/RestAPI
        public IHttpActionResult Post([FromBody] Booking booking)
        {
            try
            {
                restaurentBook.AddBookings(booking);
                return StatusCode(HttpStatusCode.Created);
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: api/RestAPI/5
        public IHttpActionResult Get(int Bookid)
        {
            try
            {
                return Ok(JsonConvert.SerializeObject(restaurentBook.GetBookingsById(Bookid)));

            }
            catch
            {
                return NotFound();
            }

        }
        // PUT: api/RestAPI/5
        public IHttpActionResult Put([FromBody]Booking booking)
        {
            try
            {
                restaurentBook.UpdateDetails(booking);
                return Ok();
            }
            catch
            {
                return NotFound();
            }

        }

        // DELETE: api/RestAPI/5
        public IHttpActionResult Delete(int Bookid)
        {
            try
            {
                restaurentBook.DeleteBooking(Bookid);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
