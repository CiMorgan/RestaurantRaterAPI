using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();  //create a DbContext instance field - call in the controller methods
        // POST "create restaurant method"   CREATE
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
                _context.Restaurants.Add(model); //model is added to created DbContext field (_context) if it is valid
                int changeCount = await _context.SaveChangesAsync();    //need to save to database since _context is just a "snapshot' of database; returns an int (task integer) if change counter is desired
                return Ok("Your restaurant was created!");  //200 OK result plus a string of choice
            }

            //If the model is not valid, reject model
            return BadRequest(ModelState);   //ModelState will return what is wrong with model
        }
        // GET ALL - getting all restaurants
        // api/Restaurant
        [HttpGet]       
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();  //asynchronously turn whole restaurant table into a list of restaurants
            return Ok(restaurants);
        }

        // Get By ID
        // api/Restaurant/{id} id is a route paramater
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)  //getting id from url
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);  //FindAsync takes in primary key

            if (restaurant != null)
            {
                return Ok(restaurant);   //returns restaurant
            }
            return NotFound();
        }

        //PUT (update)
        //api/Restaurant/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            //Check the ids if they match
            if (id != updatedRestaurant?.Id)   // ? checks to see if restaurant is null first. If it is, evaluated as false. If not null, does id check.
            {
                return BadRequest("Ids do not match.");  
            }
            // Check the modelstate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //Find the restaurant in the database
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            //If the restaurant doesn't exist, return message
            if (restaurant is null)
                return NotFound();

            //Update the restaurant
            restaurant.Name = updatedRestaurant.Name;
            restaurant.Address = updatedRestaurant.Address;

            //Save changes
            await _context.SaveChangesAsync();
            return Ok("The restaurant was updated.");
        }

        // Delete
        //api/Restaurant/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant is null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);

            if(await _context.SaveChangesAsync() == 1)    //returns int of number of changes (delete restaurant is one change)
            {
                return Ok("The restaurant was deleted.");
            }
            return InternalServerError();   //restaurant found but not deleted
        }
    }
}
