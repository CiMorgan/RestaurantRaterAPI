using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestaurantRater.Models
{
    public class RestaurantDbContext : DbContext    //DbContext class allows you to query to database and make changes - need to save
    {
        public RestaurantDbContext() : base("DefaultConnection")   //using the base constructor of the DbContext class; inputs the name of the connection string
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }  //DbSet - db table of restaurant entities
        public DbSet<Rating> Ratings { get; set; }
    }
}