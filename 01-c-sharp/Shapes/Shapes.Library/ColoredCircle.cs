using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes.Library
{
    public class ColoredCircle : BetterCircle
    {
        // instead of copy+paste from my BetterCircle class,
        // i can inherit all of its behavior / properties.

        public string Color { get; set; }
        // add one property to the class, on top of
        // what he already has from BetterCircle.
    }
}
