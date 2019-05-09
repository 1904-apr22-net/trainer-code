using Microsoft.EntityFrameworkCore;
using MovieApp.DA.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.DA
{
    /*
     * code-first EF... we write the DbContext child class,
     * and the entity classes in its DbSets.
     * 
     * we configure with, (1) conventions, (2) Data Annotations, and/or
     * (3) fluent API (preferred).
     * 
     * 1. we need a data access layer project.
     * 2. install Microsoft.EntityFrameworkCore.Tools and
     *    Microsoft.EntityFrameworkCore.Design on it,
     *    also install Microsoft.EntityFrameworkCore.SqlServer on the ASP.NET
     *    project as well as the data access project.
     * 3. make a class the inherits from DbContext.
     * 4. class needs:
     *    a. zero-parameter constructor
     *    b. ctor accepting DbContextOptions, using the parent class ctor for it.
     *    c. DbSets for our tables (with entity classes).
     *    d. OnModelConfiguring override with fluent API configuration.
     * 5. register the DbContext as a service in Startup.ConfigureServices
     *    (use user secrets to store the connection string - which should point
     *    to a DB that doesn't exist yet.)
     * 6. in Package Manager Console (with default project set to the data access
     *    project), run Add-Migration. (give the migration a name to describe the
     *    change)
     * 7. in Package Manager Console with the right defualt project,
     *    run Update-Database. this applies all un-applied migrations.
     * 8. any time you change the model (the EF configuration, entities, etc)
     *    go to step 6.
     */
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {
        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // no need to call empty parent class implementation (base.OnModelCreating)

            // configuring the movie
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Id)
                    .ValueGeneratedOnAdd();
                // should configure an IDENTITY column

                // i would use WithMany additionally to configure the
                // reverse nav property if i had made one
                entity.HasOne(m => m.Genre);

                entity.Property(g => g.Title)
                    .HasMaxLength(300);

                entity.Property(m => m.ReleaseDate)
                    .IsRequired()
                    .HasColumnType("DATETIME2");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(g => g.Name)
                    .HasMaxLength(200);
            });

            // seeding data
            // (applied via migrations, even though it is not "schema")

            // we have to use an anonymous object here in order
            // to provide a value for the shadow property GenreId.
            // (you can't use the navigation properties when seeding data in this way,
            // this is a bit more "low-level")
            modelBuilder.Entity<Movie>().HasData(new
            {
                Id = 1,
                Title = "Star Wars IV",
                ReleaseDate = new DateTime(1970, 1, 1),
                GenreId = 1 // action genre
            });

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Drama" }
            );
        }
    }
}
