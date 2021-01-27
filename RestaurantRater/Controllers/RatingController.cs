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

        //Get a rating by its Id

        //Get ALL Ratings

        //Get ALL Ratings for a specific restaurant by restaurant Id

        //Update Rating

        //Delete Rating

    }
}
