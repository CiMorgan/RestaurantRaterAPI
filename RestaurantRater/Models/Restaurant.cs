using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class Restaurant
    {
        [Key]  //sets the property below to be unique identifier
        public int Id { get; set; }  //primary key

        [Required]  //doesn't allow the property below to be empty
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();   //will have a list even if it has nothing in it - initialized inline

        public double Rating
        {
            get  //read only (no setter)
            {
                double totalAverageRating = 0;
                //add all Ratings - everything in list
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //get average from total (using ternary)
                return Ratings.Count > 0   //condition
                    ? Math.Round(totalAverageRating / Ratings.Count, 2) :   //if true - need Math.Round to round to 2 decimal points
                    0;   //if not true
            }
        }
        public bool IsRecommended
        {
            get  //read only (no setter)
            {
                return Rating > 8; //will return true if Rating > 8
            }
        }
        // Average Food Rating

        // Average Cleanliness Rating

        // Average Environment Rating
      
    }
}