using Shapes.Library;
using Shapes.Library.Interfaces;
using System;

namespace Shapes.UI
{
    class Program
    {
        // "private" means, only the current class can see the item.

        static void Main(string[] args)
        {
            Circle cir = new Circle();

            // not allowed, because, "radius" is "private".
            
            //double r = cir.radius;

            // using the getter method instead
            double r = cir.GetRadius();

            cir.SetRadius(-4);

            BetterCircle better = new BetterCircle();

            // the #1 benefit of properties is we can access them
            // using better syntax like this:
            better.Radius = 5;
            Console.WriteLine(better.Radius);
            // the code in the "get" and "set" of the property
            // is being run. the field is still hidden from me.

            Console.WriteLine("Hello World!");

            ShapeWork();
        }

        static void ShapeWork()
        {
            Rectangle r = new Rectangle();

            Console.WriteLine("Enter the length: ");

            r.Length = int.Parse(Console.ReadLine());
            r.Width = 3;

            Console.WriteLine(r.GetPerimeter());
            Console.WriteLine(r.Area);

            //BetterCircle circle = new BetterCircle();
            NoisyCircle noisyCircle = new NoisyCircle();
            BetterCircle circle = noisyCircle; // upcasting
            circle.Radius = 8;

            noisyCircle.GetPerimeter();
            circle.GetPerimeter();

            Console.WriteLine();

            PrintShapeDetails(r, "rectangle");
            PrintShapeDetails(circle, "circle");

            ColoredCircle blueCircle = new ColoredCircle();
        }

        static void PrintShapeDetails(IShape shape, string name)
        {
            Console.WriteLine("Shape " + name);
            Console.WriteLine("Area is " + shape.Area);
            Console.WriteLine("Perimeter is " + shape.GetPerimeter());
            Console.WriteLine("Sides is " + shape.Sides);
            Console.WriteLine();
        }
    }
}
