using EfIntro.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EfIntro.App
{
    class Program
    {
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
            throw new NotImplementedException();
        }

        private static void UpdateAMovie(MovieDbContext dbContext)
        {
            throw new NotImplementedException();
        }

        private static void AddAMovie(MovieDbContext dbContext)
        {
            throw new NotImplementedException();
        }

        private static void PrintMovies(MovieDbContext dbContext)
        {
            foreach (Movie movie in dbContext.Movie)
            {
                Console.WriteLine($"{movie.MovieId}: {movie.Title} ({movie.ReleaseDate.Year})");
            }
            Console.WriteLine();
        }

        private static MovieDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MovieDbContext>();
            optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);

            return new MovieDbContext(optionsBuilder.Options);
        }
    }
}
