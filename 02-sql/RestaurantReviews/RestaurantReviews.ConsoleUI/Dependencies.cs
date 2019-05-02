using Microsoft.EntityFrameworkCore;
using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.DataAccess.Repositories;
using RestaurantReviews.Library.Interfaces;
using RestaurantReviews.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RestaurantReviews.ConsoleUI
{
    public static class Dependencies
    {
        public static IRestaurantRepository CreateRestaurantRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantReviewsDbContext>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);

            var dbContext = new RestaurantReviewsDbContext(optionsBuilder.Options);

            return new RestaurantRepository(dbContext);
        }

        public static XmlSerializer CreateXmlSerializer() =>
            new XmlSerializer(typeof(List<Library.Models.Restaurant>));
    }
}
