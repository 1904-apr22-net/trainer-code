﻿using RestaurantReviews.Library.Models;
using System;
using System.Collections.Generic;

namespace RestaurantReviews.Library.Interfaces
{
    /// <summary>
    /// A repository managing data access for restaurant objects and their review members.
    /// </summary>
    public interface IRestaurantRepository : IDisposable
    {
        /// <summary>
        /// Get all restaurants with deferred execution, including any associated reviews.
        /// </summary>
        /// <returns>The collection of restaurants</returns>
        IEnumerable<Restaurant> GetRestaurants(string search = null);

        /// <summary>
        /// Get a restaurant by ID, including any associated reviews.
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
        /// Get a review.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        Review GetReviewById(int reviewId);

        /// <summary>
        /// Add a review and optionally associate it with a restaurant.
        /// </summary>
        /// <param name="review">The review</param>
        /// <param name="restaurant">The restaurant (or null if none)</param>
        void AddReview(Review review, Restaurant restaurant = null);

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

        /// <summary>
        /// Get the ID of the restaurant associated to the review with the given ID.
        /// </summary>
        /// <param name="reviewId">The ID of the review</param>
        /// <returns>The ID of the restaurant</returns>
        int RestaurantIdFromReviewId(int reviewId);

        /// <summary>
        /// Persist changes to the data source.
        /// </summary>
        void Save();
    }
}
