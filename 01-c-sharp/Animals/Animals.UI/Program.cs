using Animals.Library;
using System;

// from the terminal...
/*
 * mkdir Animals
 * cd Animals
 * dotnet new console --name Animals.UI
 * dotnet new sln
 * dotnet sln add Animals.UI
 */

namespace Animals.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // declaring and instantiating a Dog
            // to access the class in the other project, I need a project reference.
            // (the lightbulb in VS can provide it)
            // (in terminal, from Animals.UI folder,...)
            // (dotnet add reference ../Animals.Library)
            // (and, add "using Animals.Library;" at the top of this file)
            Dog dog = new Dog();

            // accessing properties and methods on objects.
            dog.Name = "Bill";
            dog.Breed = "Labrador";
            dog.Bark();

            Console.WriteLine("Hello World!");
        }
    }
}
