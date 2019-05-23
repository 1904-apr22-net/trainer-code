using DogRestService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DogRestService.DAL
{
    public class DogDbContext : DbContext
    {
        public DogDbContext()
        { }

        public DogDbContext(DbContextOptions<DogDbContext> options) : base(options)
        { }

        public DbSet<Dog> Dog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Dog>().HasData(new List<Dog>
            {
                new Dog { Id = 1, Name = "Fred", Breed = "Beagle" }
            });
    }
}
