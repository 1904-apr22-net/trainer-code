using System;

// a namespace is just a sort of street address to a collection of classes
// the usual convention is, to have your namespaces match your folder structure
namespace CSharpBasics
{
    // a class "Program"
    // (double slash is comments) in VS
    // Ctrl+K,Ctrl+C comments, Ctrl+K,Ctrl+U uncomments
    // (in VS Code... it's just Ctrl+/ for both)
    class Program
    {
        // a method "Main" on the class "Program"
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // variables and types
            // (type) (new name) = (value);
            string s = "abc";
            s = "cba";

            // declaration of the variable
            string s2;
            // the initialization of the variable
            // (giving it its initial value)
            s2 = "a";

            //numeric types
            // integer types
            //  int (4 byte) (use this one)
            //  byte (1), short (2), long (8)

            // decimal types
            //  double (8 byte) (use this one)
            //  float (4)
            // the benefit of the higher memory of double
            // .. we can store more precision, about 15 decimal places

            // boolean
            bool b = true;
            b = false;

            // boolean operators
            b = true || true; // OR
            b = false && true; // AND

            // comparison operators
            b = (3 == 3); // equals
            b = (3 != 3); // not equals
            b = (3 < 4); // less than
            b = (3 > 4); // greater than
            b = (3 >= 4); // greater than or equal to
            b = (3 <= 4); // less than or equal to

            // control flow statements

            // conditional
            if (3 <= 4)
            {
                // run some code, only if 3 <= 4 is true
            }
            else
            {
                // otherwise, run this code here.
            }

            // loops
            //  a for loop, using a loop variable "num", that runs 10 times.
            for (int num = 0; num < 10; num++)
            {
                // this code runs 10 times
                Console.WriteLine(num); // print out 0 to 9.
            }
            
            // while loop - keep running the loop until the condition
            // becomes false.
            while (b)
            {
                // this code runs many times so long as the condition is true.
            }

            switch (s)
            {
                case "1":
                    // do something
                    break;
                case "abc":
                    // so something else
                    break;
                default:
                    // if no match, do yet something else
                    break;
            }
            // in C# we can put plenty of things in the switch statement
            // it will compare with ==

            // VS gives us tab-completion on things like this
            AnotherMethod();
        }

        // (modifiers) (return type) (method name) (in parens, the parameters)
        static void AnotherMethod()
        {
            // extract some code out to another reusable function.
        }
    }
}
