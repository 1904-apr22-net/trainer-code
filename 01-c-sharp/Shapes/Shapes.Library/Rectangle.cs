using Shapes.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes.Library
{
    // "rectangle implements IShape interface"
    // this means, the Rectangle will provide everything an IShape is required
    // to provide.
    public class Rectangle : IShape
    {
        public double Length { get; set; }

        // in both VS and VS Code, because we write properties so often
        // we can type "prop<TAB><TAB>"
        public double Width { get; set; }

        // very simple property, get only, constant value.
        public int Sides => 4;

        public double GetPerimeter()
        {
            return 2 * Length + 2 * Width;
        }

        // we can do properties that are not necessarily tied to one field exactly.
        // we also, they do not need a "set"

        //public double Area
        //{
        //    get
        //    {
        //        // could do extra stuff in here
        //        return Length * Width;
        //    }
        //}

        // we have a syntax named "expression body"
        // to replace methods that just say "return <something>" and nothing else.
        //public double Area { get => Length * Width; }

        // a third way to write the exact same thing -
        // even shorter when we just have a simple computation to make.
        public double Area => Length * Width;
    }
}
