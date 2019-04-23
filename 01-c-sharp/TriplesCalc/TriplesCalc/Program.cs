using System;

namespace TriplesCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            // get input
            Console.WriteLine("Please enter an integer: ");

            string input = Console.ReadLine();
            int num = int.Parse(input);

            // set up calculator
            Calculator calc = new Calculator();

            // run calculation
            calc.Calculate(num);
        }
    }
}
