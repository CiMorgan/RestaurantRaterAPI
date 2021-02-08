﻿using System;
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

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public double Rating
        {
            get
            {
                double totalAverageRating = 0;
                //add all Ratings
                foreach (var rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }

                //get average from total
                return Ratings.Count > 0
                    ? Math.Round(totalAverageRating / Ratings.Count, 2) :
                    0;
            }
        }
        public bool IsRecommended
        {
            get
            {
                return Rating > 8;
            }
        }
        // Average Food Rating

        // Average Cleanliness Rating

        // Average Environment Rating
      
    }
}