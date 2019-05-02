using Microsoft.EntityFrameworkCore;
using NLog;
using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantReviews.DataAccess.Repositories
{
    /// <summary>
    /// A repository managing data access for restaurant objects and their review members,
    /// using Entity Framework.
    /// </summary>
    /// <remarks>
    /// This class ought to have better exception handling and logging.
    /// </remarks>
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantReviewsDbContext _dbContext;

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new restaurant repository given a suitable restaurant data source.
        /// </summary>
        /// <param name="dbContext">The data source</param>
        public RestaurantRepository(RestaurantReviewsDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        /// <summary>
        /// Get all restaurants with deferred execution.
        /// </summary>
        /// <returns>The collection of restaurants</returns>
        public IEnumerable<Library.Models.Restaurant> GetRestaurants(string search = null)
        {

            IQueryable<Entities.Restaurant> items = _dbContext.Restaurant
                .Include(r => r.Review).AsNoTracking();
            if (search != null)
            {
                items = items.Where(r => r.Name.Contains(search));
            }
            return Mapper.Map(items);
        }

        /// <summary>
        /// Get a restaurant by ID.
        /// </summary>
        /// <returns>The restaurant</returns>
        public Library.Models.Restaurant GetRestaurantById(int id) =>
            Mapper.Map(_dbContext.Restaurant.Find(id));

        /// <summary>
        /// Add a restaurant, including any associated reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant</param>
        public void AddRestaurant(Library.Models.Restaurant restaurant)
        {
            if (restaurant.Id != 0)
            {
                _logger.Warn($"Restaurant to be added has an ID ({restaurant.Id}) already: ignoring.");
            }

            _logger.Info($"Adding restaurant");

            Entities.Restaurant entity = Mapper.Map(restaurant);
            entity.Id = 0;
            _dbContext.Add(entity);
        }

        /// <summary>
        /// Delete a restaurant by ID. Any reviews associated to it will also be deleted.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant</param>
        public void DeleteRestaurant(int restaurantId)
        {
            _logger.Info($"Deleting restaurant with ID {restaurantId}");
            Entities.Restaurant entity = _dbContext.Restaurant.Find(restaurantId);
            _dbContext.Remove(entity);
        }

        /// <summary>
        /// Update a restaurant as well as its reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant with changed values</param>
        public void UpdateRestaurant(Library.Models.Restaurant restaurant)
        {
            _logger.Info($"Updating restaurant with ID {restaurant.Id}");

            // calling Update would mark every property as Modified.
            // this way will only mark the changed properties as Modified.
            Entities.Restaurant currentEntity = _dbContext.Restaurant.Find(restaurant.Id);
            Entities.Restaurant newEntity = Mapper.Map(restaurant);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Add a review and associate it with a restaurant.
        /// </summary>
        /// <param name="review">The review</param>
        /// <param name="restaurant">The restaurant</param>
        public void AddReview(Library.Models.Review review, Library.Models.Restaurant restaurant = null)
        {
            if (restaurant.Id != 0)
            {
                _logger.Warn($"Review to be added has an ID ({review.Id}) already: ignoring.");
            }

            _logger.Info($"Adding review to restaurant with ID {restaurant.Id}");

            if (restaurant != null)
            {
                // get the db's version of that restaurant
                // (can't use Find with Include)
                Entities.Restaurant restaurantEntity = _dbContext.Restaurant
                    .Include(r => r.Review).First(r => r.Id == restaurant.Id);
                Entities.Review newEntity = Mapper.Map(review);
                restaurantEntity.Review.Add(newEntity);
                // also, modify the parameters
                restaurant.Reviews.Add(review);
            }
            else
            {
                Entities.Review newEntity = Mapper.Map(review);
                _dbContext.Add(newEntity);
            }
        }

        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        public void DeleteReview(int reviewId)
        {
            _logger.Info($"Deleting review with ID {reviewId}");

            Entities.Review entity = _dbContext.Review.Find(reviewId);
            _dbContext.Remove(entity);
        }

        /// <summary>
        /// Update a review.
        /// </summary>
        /// <param name="review">The review with changed values</param>
        public void UpdateReview(Library.Models.Review review)
        {
            _logger.Info($"Updating review with ID {review.Id}");

            Entities.Review currentEntity = _dbContext.Review.Find(review.Id);
            Entities.Review newEntity = Mapper.Map(review);

            _dbContext.Entry(currentEntity).CurrentValues.SetValues(newEntity);
        }

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
