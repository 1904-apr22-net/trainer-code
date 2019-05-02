using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DataAccess
{
    public static class Mapper
    {
        public static Library.Models.Restaurant Map(Entities.Restaurant restaurant) => new Library.Models.Restaurant
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Reviews = Map(restaurant.Review).ToList()
        };

        public static Entities.Restaurant Map(Library.Models.Restaurant restaurant) => new Entities.Restaurant
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Review = Map(restaurant.Reviews).ToList()
        };

        public static Library.Models.Review Map(Entities.Review review) => new Library.Models.Review
        {
            Id = review.Id,
            ReviewerName = review.ReviewerName,
            Score = review.Score,
            Text = review.Text
        };

        public static Entities.Review Map(Library.Models.Review review) => new Entities.Review
        {
            Id = review.Id,
            ReviewerName = review.ReviewerName,
            Score = review.Score ?? throw new ArgumentException("review score cannot be null.", nameof(review)),
            Text = review.Text
        };

        public static IEnumerable<Library.Models.Restaurant> Map(IEnumerable<Entities.Restaurant> restaurants) =>
            restaurants.Select(Map);

        public static IEnumerable<Entities.Restaurant> Map(IEnumerable<Library.Models.Restaurant> restaurants) =>
            restaurants.Select(Map);

        public static IEnumerable<Library.Models.Review> Map(IEnumerable<Entities.Review> reviews) =>
            reviews.Select(Map);

        public static IEnumerable<Entities.Review> Map(IEnumerable<Library.Models.Review> reviews) =>
            reviews.Select(Map);
    }
}
