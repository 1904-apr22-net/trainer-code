using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes.Library
{
    public class NoisyCircle : BetterCircle
    {
        // what i want to do is override what the parent class does
        // and put in a lot of WriteLine to print out what is happening
        // to this object.

        // in C#, by default, you cannot override, you can only
        // add new properties/methods/etc.

        // we opt-in to the possibility of override in the parent class
        // using "virtual" modifier.

        public override double Radius
        {
            // with private access, even a subclass cannot see that thing.
            get
            {
                Console.WriteLine("Getting radius");
                return _radius;
            }
            set
            {
                Console.WriteLine("Setting radius");
                _radius = value;
            }
        }

        public override double Area
        {
            get
            {
                Console.WriteLine("Getting area");
                return base.Area;
            }
        }

        public new double GetPerimeter()
        {
            Console.WriteLine("getting perimeter.");
            // we can always access the parent class's implementation with
            // "base"
            return base.GetPerimeter();
        }
    }
}
