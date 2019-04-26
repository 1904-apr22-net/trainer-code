using NLog;
using RestaurantReviews.Library.Interfaces;
using RestaurantReviews.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DataAccess.Repositories
{
    /// <summary>
    /// A repository managing data access for restaurant objects and their review members,
    /// using an in-memory collection (no data persistence).
    /// </summary>
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ICollection<Restaurant> _data;

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new restaurant repository given a suitable restaurant data source.
        /// </summary>
        /// <param name="data">The data source</param>
        public RestaurantRepository(ICollection<Restaurant> data) =>
            _data = data ?? throw new ArgumentNullException(nameof(data));

        /// <summary>
        /// Get all restaurants with deferred execution.
        /// </summary>
        /// <returns>The collection of restaurants</returns>
        public IEnumerable<Restaurant> GetRestaurants(string search = null)
        {
            IEnumerable<Restaurant> items = _data;
            if (search != null)
            {
                items = items.Where(r => r.Name.Contains(search));
            }
            foreach (Restaurant item in items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Get a restaurant by ID.
        /// </summary>
        /// <returns>The restaurant</returns>
        public Restaurant GetRestaurantById(int id) => _data.First(r => r.Id == id);

        /// <summary>
        /// Add a restaurant, including any associated reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant</param>
        public void AddRestaurant(Restaurant restaurant)
        {
            if (restaurant.Id != 0)
            {
                _logger.Warn($"Restaurant to be added has an ID ({restaurant.Id}) already: ignoring.");
            }
            // calculate an appropriate ID: the first integer higher
            // than the maximum existing ID.
            var id = _data.Select(x => x.Id).DefaultIfEmpty().Max() + 1;
            restaurant.Id = id;

            _logger.Info($"Adding restaurant with ID {id}");

            _data.Add(restaurant);
        }

        /// <summary>
        /// Delete a restaurant by ID. Any reviews associated to it will not be deleted.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant</param>
        public void DeleteRestaurant(int restaurantId)
        {
            _logger.Info($"Deleting restaurant with ID {restaurantId}");
            _data.Remove(_data.First(r => r.Id == restaurantId));
        }

        /// <summary>
        /// Update a restaurant as well as its reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant with changed values</param>
        public void UpdateRestaurant(Restaurant restaurant)
        {
            _logger.Info($"Updating restaurant with ID {restaurant.Id}");
            DeleteRestaurant(restaurant.Id);
            AddRestaurant(restaurant);
        }

        /// <summary>
        /// Add a review and associate it with a restaurant.
        /// </summary>
        /// <param name="review">The review</param>
        /// <param name="restaurant">The restaurant</param>
        public void AddReview(Review review, Restaurant restaurant)
        {
            if (restaurant.Id != 0)
            {
                _logger.Warn($"Review to be added has an ID ({review.Id}) already: ignoring.");
            }
            // calculate an appropriate ID: the first integer higher
            // than the maximum existing ID.
            // SelectMany does what Select does, and then
            // flattens one level of nested sequences.
            var id = _data.SelectMany(res => res.Reviews.Select(rev => rev.Id))
                .DefaultIfEmpty().Max() + 1;
            review.Id = id;

            _logger.Info($"Adding review with ID {review.Id} to restaurant with ID {restaurant.Id}");
            restaurant.Reviews.Add(review);
        }

        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        public void DeleteReview(int reviewId)
        {
            _logger.Info($"Deleting review with ID {reviewId}");
            Restaurant restaurant = _data.First(x => x.Reviews.Any(y => y.Id == reviewId));
            restaurant.Reviews.Remove(restaurant.Reviews.First(y => y.Id == reviewId));
        }

        /// <summary>
        /// Update a review.
        /// </summary>
        /// <param name="review">The review with changed values</param>
        public void UpdateReview(Review review)
        {
            _logger.Info($"Updating review with ID {review.Id}");
            Restaurant restaurant = _data.First(x => x.Reviews.Any(y => y.Id == review.Id));
            var index = restaurant.Reviews.IndexOf(restaurant.Reviews.First(y => y.Id == review.Id));
            restaurant.Reviews[index] = review;
        }
    }
}
