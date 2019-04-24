using System;

namespace Collections
{
    // reminder: default type access is internal
    class Program
    {
        static void Main(string[] args)
        {
            int num = 123;
            Random r = new Random();

            var num2 = 123;
            var r2 = new Random();

            // C# has "var" for compile-time type inference.
            // the compiler guesses the type of the variable
            // based on what is to the right of the equals sign.

            // use var when it's obvious to the reader what the type is
            // but don't use var when it would make the code less self-documenting

            Arrays();
        }

        // reminder: default member access is private
        static void Arrays()
        {
            // "int[]" is a type, it means, array of int.
            // this line declares an int[] variable, and creates a size-5 array
            // filled with the default value for int (0).
            int[] myNums = new int[5];
            // once an array have been created, it has that size forever.

            // for each i from 0 up to but not including 5
            for (int i = 0; i < myNums.Length; i++)
            {
                // set the value at the ith place
                myNums[i] = i * i;
                // print the value at the ith place
                Console.WriteLine(myNums[i]);
            }
            
            // the indexing in C# as in most languages is 0-based
            // so in an array of five...
            // we have 0, 1, 2, 3, 4 as the indices.

            // for each thing in the collection
            foreach (var item in myNums)
            {
                // print it
                Console.WriteLine(item);
                // we can't modify it, though
            }

            Console.WriteLine("How many elements? ");
            var count = int.Parse(Console.ReadLine());
            var array = new int[count];
            // we can set the length dynamically
            // it just cant be changed after that.

            // we have array initialization syntax that looks like this
            var names = new[] { "Nick", "fred", "bill" };
            // the type is inferred (string), the length is inferred (3)

            // we can put arrays inside arrays to get 2D and 3D etc data.
        }
    }
}
