using System;

namespace TriplesCalc
{
    public class Calculator
    {
        // print every multiple of 3
        // between 1 and n
        // in reverse order.

        public void Calculate(int n)
        {
            // first step:
            // figure out the first number
            // to print.
            int num = n - (n % 3); //
            // % is called modulo, it means
            // remainder.

            // second step:
            // keep subtracting three from that
            // until it's below 1 or == 3 or etc.
            while (num > 0)
            {
                Console.WriteLine(num);
                num -= 3; // we have -= and +=
            }
        }
    }
}
