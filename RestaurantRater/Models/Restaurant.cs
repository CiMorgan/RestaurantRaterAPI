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

        [Required]
        public double Rating { get; set; }
    }
}