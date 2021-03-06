﻿using RestaurantReviews.Library.Models;
using System.Collections.Generic;

namespace RestaurantReviews.Library.Interfaces
{
    /// <summary>
    /// A repository managing data access for restaurant objects and their review members.
    /// </summary>
    public interface IRestaurantRepository
    {
        /// <summary>
        /// Get all restaurants with deferred execution.
        /// </summary>
        /// <returns>The collection of restaurants</returns>
        IEnumerable<Restaurant> GetRestaurants(string search = null);

        /// <summary>
        /// Get a restaurant by ID.
        /// </summary>
        /// <returns>The restaurant</returns>
        Restaurant GetRestaurantById(int id);

        /// <summary>
        /// Add a restaurant, including any associated reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant</param>
        void AddRestaurant(Restaurant restaurant);

        /// <summary>
        /// Delete a restaurant by ID. Any reviews associated to it will also be deleted.
        /// </summary>
        /// <param name="restaurantId">The ID of the restaurant</param>
        void DeleteRestaurant(int restaurantId);

        /// <summary>
        /// Update a restaurant as well as its reviews.
        /// </summary>
        /// <param name="restaurant">The restaurant with changed values</param>
        void UpdateRestaurant(Restaurant restaurant);

        /// <summary>
        /// Add a review and associate it with a restaurant.
        /// </summary>
        /// <param name="review">The review</param>
        /// <param name="restaurant">The restaurant</param>
        void AddReview(Review review, Restaurant restaurant);

        /// <summary>
        /// Delete a review by ID.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        void DeleteReview(int reviewId);

        /// <summary>
        /// Update a review.
        /// </summary>
        /// <param name="review">The review with changed values</param>
        void UpdateReview(Review review);
    }
}
