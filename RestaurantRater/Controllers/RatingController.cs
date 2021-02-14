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
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        //Creat new ratings
        [HttpPost]

        public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
        {
            //Check if model is null
            if (model is null)
                return BadRequest("Your request body can not be empty.");

            //Check if modelstate is invalid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Find the restaurant by the model.RestaurantId and see that it exists
            var restaurantentity = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurantentity is null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");

            //Create the rating

            ////Add to the rating table
            //_context.Ratings.Add(model);

            //Add to restaurant entity
            restaurantentity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated restaurant {restaurantentity.Name} successfully");

            return InternalServerError();

        }
        //Get ALL Ratings
        //api/Rating
        [HttpGet]
        public async Task<IHttpActionResult> GetAllRating()
        {
            List<Rating> ratings = await _context.Ratings.ToListAsync();  //asynchronously turn whole restaurant table into a list of restaurants
            return Ok(ratings);
        }

        //Get a rating by its Id
        //api/Rating/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetRatingById([FromUri] int id)  //getting id from url
        {
            Rating rating = await _context.Ratings.FindAsync(id);  //FindAsync takes in primary key

            if (rating != null)
            {
                return Ok(rating);   //returns restaurant
            }
            return NotFound();
        }

        //Get ALL Ratings for a specific restaurant by restaurant Id
        //api/Rating/{id}
        //[HttpGet]
        //public async Task<IHttpActionResult> GetRatingByRestaurant([FromUri] int restId)
        //{
        //    Restaurant restaurant = await _context.Restaurants.FindAsync(restId);

        //    if (restaurant != null)
        //    {
        //        return Ok(restaurant);   //returns restaurant
        //    }
        //    return NotFound();
        //}

        //Update Rating
        //api/Rating/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating([FromUri] int id, [FromBody] Rating updatedRating)
        {
            //Check the ids if they match
            if (id != updatedRating?.Id)   
            {
                return BadRequest("Ids do not match.");
            }
            // Check the modelstate
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Rating rating = await _context.Ratings.FindAsync(id);

            if (rating is null)
                return NotFound();

            //Update the restaurant
            rating.FoodScore = updatedRating.FoodScore;
            rating.CleanlinessScore = updatedRating.CleanlinessScore;
            rating.EnvironmentScore = updatedRating.EnvironmentScore;

            //Save changes
            await _context.SaveChangesAsync();
            return Ok("The rating was updated.");
        }

        //Delete Rating
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRating([FromUri] int id)
        {
            Rating rating = await _context.Ratings.FindAsync(id);
            if (rating is null)
                return NotFound();

            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() == 1)   //checks to see if delete occurred
            {
                return Ok("The rating was deleted.");
            }
            return InternalServerError();   
        }

    }
}
