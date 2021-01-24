using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        // POST (create restaurant method)
        // api/Restaurant
        [HttpPost]  //attribute that indicates this is a post method regardless of the name given
        public async Task<IHttpActionResult> CreateRestaurant ([FromBody] Restaurant model)
        {
            if (model is null)  //is keyword - pattern matching instead of ==
            {
                return BadRequest("Your request body can not be empty.");
            }

            if (ModelState.IsValid)    //if the model is valid (empty model is valid)
            {
                //Store the model in the database
                _context.Restaurants.Add(model);
                int changeCount = await _context.SaveChangesAsync();    //need to save to database since _context is just a "snapshot' of database; returns an int (task integer) if change counter is desired
                return Ok("Your restaurant was created!");
            }

            //The model is not valid, reject model
            return BadRequest(ModelState);   //ModelState will return what is wrong
        }
    }
}
