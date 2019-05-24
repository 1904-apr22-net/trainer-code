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

        public DbSet<Account> Account { get; set; }
        public DbSet<Dog> Dog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .Property(a => a.Email)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(a => a.Name)
                .IsRequired();

            modelBuilder.Entity<Dog>()
                .Property(e => e.Name)
                .IsRequired();

            modelBuilder.Entity<Dog>()
                .Property(e => e.Breed)
                .IsRequired();

            modelBuilder.Entity<Account>().HasData(new List<Account>
            {
                new Account
                {
                    Id = 1,
                    Email = "nicholasescalona@outlook.com",
                    Name = "Nick Escalona"
                }
            });

            modelBuilder.Entity<Dog>().HasData(new List<Dog>
            {
                new Dog { Id = 1, Name = "Fred", Breed = "Beagle", OwnerId = 1 }
            });
        }
    }
}
