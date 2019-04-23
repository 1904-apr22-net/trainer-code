using System;

// from the terminal... (in Animals folder)
//  ( dotnet new classlib --name Animals.Library )

// we have a solution with two projects in it.
namespace Animals.Library
{
    // ( mv Animals.Library/Class1.cs Animals.Library/Dog.cs )
    public class Dog
    {
        // classes can contain methods.. they also contain data.
        // normally we use "properties" to store data in classes.

        public string Breed { get; set; }
        public string Name { get; set; }

        public void Bark()
        {
            Console.WriteLine("Bark");
        }

        // every Dog object will have his own breed, name, and ability to bark.
    }
}
