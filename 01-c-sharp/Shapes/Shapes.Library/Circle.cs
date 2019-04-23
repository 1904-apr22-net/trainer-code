using System;

namespace Shapes.Library
{
    // default access for classes is "internal", not public
    public class Circle
    {
        // field (most fundamental way to put data on a class)
        private double radius;

        // methods
        // getter
        public double GetRadius()
        {
            // add correction factor
            return radius * 1.01;
        }

        // setter
        public void SetRadius(double radius)
        {
            if (radius < 0)
            {
                // (needs "using System;" at the top)
                Console.WriteLine("not allowed!");
            }
            else
            {
                this.radius = radius;
            }
        }

        // in C#, instead of fields + getters + setters,
        // we use properties.
    }
}
