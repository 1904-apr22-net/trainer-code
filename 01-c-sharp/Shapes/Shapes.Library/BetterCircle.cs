using Shapes.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes.Library
{
    public class BetterCircle : IShape
    {
        public int Sides => 1;

        // for data that we don't YET have any special requirements, but we might
        // want to add them someday, we still shouldn't use public fields.
        // we can use "auto-property".

        public double X { get; set; }
        public double Y { get; set; }

        // these provide a hidden automatic backing field
        // much shorter way to get all the advantages of at least a no-extra-logic
        // getter + setter.

        // when a field is used to start a property's value like this
        // we call it a "backing field"
        protected double _radius;
        //private double _radius;
        // underscore is one common convention for private fields.

        // property that does the same thing as Circle's getters and setters.
        public virtual double Radius
            // "virtual": subclasses are allowed to override this property/method
            // and change how it works for their instances.
        {
            get
            {
                return _radius * 1.01;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    _radius = value;
                }
            }
        }

        // we call "=>" "arrow" or maybe "goes to"
        public double GetPerimeter() => 2 * Math.PI * Radius;

        //public double Area => Math.PI * Radius * Radius;
        public virtual double Area => Math.PI * Math.Pow(Radius, 2);
        // circle area is "pi r squared"
    }
}
