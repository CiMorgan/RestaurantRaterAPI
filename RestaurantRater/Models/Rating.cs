using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }   //primary key - this object

        [ForeignKey(nameof(Restaurant))]  
        public int RestaurantId { get; set; }  //foreign key - reference to a primary key of another table

        //Navigation Property; 
        public virtual Restaurant Restaurant { get; set; }  // virtual keyword enables it to be over-written (builds the foreign key relationship)
        
        [Required]
        [Range (0,10)]
        public double FoodScore { get; set; }
        [Required, Range(0, 10)]
        public double CleanlinessScore { get; set; }
        [Required, Range(0, 10)]
        public double EnvironmentScore { get; set; }

        public double AverageRating  //property
        {
            get  //read only property; does not have setter
            {
                var totalScore = FoodScore + EnvironmentScore + CleanlinessScore;
                return totalScore / 3;
            }
        }

    }
}