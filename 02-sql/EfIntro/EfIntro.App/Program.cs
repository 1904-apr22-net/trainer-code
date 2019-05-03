using EfIntro.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Linq;

namespace EfIntro.App
{
    class Program
    {
        public static readonly LoggerFactory AppLoggerFactory = new LoggerFactory(new[]
        {
            new ConsoleLoggerProvider((_, level)
                => level >= LogLevel.Information, true)
        });

        static void Main(string[] args)
        {
            // steps for EF DB-first approach:

            // 1. install Microsoft.EntityFrameworkCore.SqlServer to the
            //    startup project.
            // 2. install Microsoft.EntityFrameworkCore.Design to the data access
            //    project. if using VS, also install Microsoft.EntityFrameworkCore.Tools.
            // 3. add project reference from startup project to data access project.
            // 4. either...
            // a. in VS... open Package Manager Console, change Default project
            //    to the data access project, and run:
            //    Scaffold-DbContext -Connection <connection-string-in-quotes>
            //        -Provider Microsoft.EntityFrameworkCore.SqlServer
            //        -StartupProject <name-of-startup-proj> -OutputDir Entities -Force
            // b. in Git Bash... from the data access project directory, run:
            //    dotnet ef dbcontext scaffold <connection-string-in-quotes>
            //        Microsoft.EntityFrameworkCore.SqlServer
            //        --startup-project <path-to-startup-proj> --force
            //        --output-dir Entities
            // 5. delete the OnConfiguring overload with the connections string in
            //    it from the generated DbContext file.

            // 6. anytime you change the database in future, start again from step 4.

            // --------------------------

            using (MovieDbContext dbContext = CreateDbContext())
            {
                PrintMovies(dbContext);

                AddAMovie(dbContext);

                PrintMovies(dbContext);

                UpdateAMovie(dbContext);

                PrintMovies(dbContext);

                DeleteAMovie(dbContext);

                PrintMovies(dbContext);
            }
        }

        private static void DeleteAMovie(MovieDbContext dbContext)
        {
            // the most recently modified movie, with LINQ
            Movie movie = dbContext.Movie
                .OrderByDescending(m => m.DateModified)
                .First();

            dbContext.Remove(movie);
            // this will use the SQL-defined "on delete cascade" behavior if present.
            // (if there are any foreign keys pointing at the thing we are removing.)

            dbContext.SaveChanges();
        }

        private static void UpdateAMovie(MovieDbContext dbContext)
        {
            // the most recently modified movie, with LINQ
            Movie movie = dbContext.Movie
                .OrderByDescending(m => m.DateModified)
                .First();

            // the objects you pull out of the DbSets are "tracked"
            // by EF. meaning EF watches changes to all its properties
            // for later application with SaveChanges.

            // set active if inactive, and vice versa
            movie.Active = !movie.Active;



            dbContext.SaveChanges();
        }

        private static void AddAMovie(MovieDbContext dbContext)
        {
            // because DbSet is IQueryable...
            // this LINQ expression does not ever actually run as C#,
            // it is translated to a SQL query. "LINQ To SQL"
            // e.g. in SQL string compare is case-insensitive:
            var actionGenre = dbContext.Genre.First(g => g.Name == "acTIOn");

            var movie = new Movie
            {
                Title = "Star Wars VIII",
                ReleaseDate = new DateTime(2018, 1, 1),
                Genre = actionGenre
            };

            // i am ultimately setting up a foreign key relationship
            // but i'm doing it by setting navigation properties

            dbContext.Add(movie);

            // this would also work... but i don't need to do both!
            //actionGenre.Movie.Add(movie);

            // nothing has yet been sent to the real DB...

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                dbContext.Movie.Remove(movie);
                Console.WriteLine(ex.Message);
            }

            // now the new movie is in the real DB.
        }

        private static void PrintMovies(MovieDbContext dbContext)
        {
            foreach (Movie movie in dbContext.Movie.Include(m => m.Genre))
            {
                Console.WriteLine($"{movie.Genre.Name} {movie.MovieId}: {movie.Title} ({movie.ReleaseDate.Year})");
            }
            Console.WriteLine();
        }

        private static MovieDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
            optionsBuilder
                .UseSqlServer(SecretConfiguration.ConnectionString)
                .UseLoggerFactory(AppLoggerFactory);

            return new MovieDbContext(optionsBuilder.Options);
        }
    }
}
