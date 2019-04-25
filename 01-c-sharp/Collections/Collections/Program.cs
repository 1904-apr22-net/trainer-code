using System;
using System.Collections;
using System.Collections.Generic;

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

            //Arrays();
            Lists();
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
            // "jagged arrays"

            int[][] twoD = new int[3][];
            twoD[0] = new int[4];
            twoD[1] = new[] { 1, 6 };
            twoD[2] = new int[4];

            // access the first row and the second column
            var x = twoD[0][1];

            // that's one way to get 2d, 3d, etc.
            // the other way is called "multidimensional array"

            int[,] twoDMulti = new int[4, 5]; // 4 by 5
            twoDMulti[2, 3] = 5; // set 3rd row, 4th column to 5.

            // however we usually avoid arrays unless there is a performance need.
        }

        static void Lists()
        {
            // first, we had "non-generic" List, which was called ArrayList
            ArrayList numList = new ArrayList();
            // this has a changeable length, starting out at size 0.
            // technically, it is a list of "object" type.
            numList.Add(2);
            numList.Add(5);
            numList.Add(8);

            numList.Remove(8);

            //var num = numList[0];
            int num = (int)numList[0];
            // this is called "casting". it attempts to convert
            // whatever's to the right, to the given type.
            // the compiler will let me write this,
            // but it might fail at runtime.

            var twice = num + num; // not allowed - the variable "num" is object type.

            //string s = (string)numList[1]; // will throw InvalidCastException
            string s2 = ((int)numList[1]).ToString();
            string theNumberThree = 3.ToString(); // "3"
            //int.Parse("asdf"); // will throw FormatException

            // implicit and explicit casting.
            double d = 4;
            // C# knows that i can't lose any information going from int to double.
            // so it will cast automatically.
            int n = (int)d;
            // C# expects that there could be data loss going from double to int.
            // the explicit cast is needed.

            // for objects made from classes, the rule is, you can cast
            // implicitly from child types to parent types.

            // object is the parent class of Random
            object o = new Random(); // implicit casting ("upcasting")
            // upcasting is implicit.
            // because every Random can do everything object can do.

            Random r = (Random)o; // downcasting is explicit.
            // because at runtime that "o" might not really be a Random.



            // generic list.
            // with generics, we can write code for many different types,
            // and then when we need the code, we'll decide at that time what the
            // type will be.

            var genericIntList = new List<int> { 3 };

            var value = genericIntList[0];

            var twoDStringList = new List<List<string>>
            {
                new List<string> { "1", "3", "5" },
                new List<string> { "as", "2", "ggg" }
            };
            // just like for arrays, we have a "initialization syntax" for
            // Lists, that winds up calling the Add method under the hood.

            // List class can make many List instances, each of which
            // might have its own type that it requires.
        }

        static void OtherCollections()
        {
            // we have some other classes
            var set = new HashSet<string> { "abc", "ab", "ab" };
            set.Add("221df"); // if that string was already in the set
            // nothing at all would happen.

            // this is based on the mathematical idea of "set":
            // no duplicates allowed. / duplicates ignored.
            // they also have no defined order.

            var number = set.Count; // 2, because "ab" dupe is ignored.

            foreach (var item in set)
            {
                // no guarantees about what order they will come out in.
            }

            // we have "map" or "dictionary"
            var numberOfTimesSeenWord = new Dictionary<string, int>();

            numberOfTimesSeenWord["food"] = 1;
            numberOfTimesSeenWord["pc"] = 3;

            // a dictionary will store some value, for each key.

            // in the same way that in a list, each index from 0 to the total
            // has some value...

            // in the dictionary, each key that you insert at will have one value
            // at that spot
            // both hashset and dictionary are implemented with hashtables.

            // this means that searching through them for one thing
            // is very cheap and fast.

            var list = new List<int> { 3, 4, 6, 2, 6 };

            var newSet = new HashSet<int>(list);
            // the set doesn't _contain_ the list,
            // it gets its initial values from the list.
            var count = newSet.Count; // 4.
        }

        static void OutDemo()
        {
            // we use out often for sort of conditional operations
            var str = "1234";

            //var num = int.Parse(str);

            if (int.TryParse(str, out var num))
            {
                // in here, we have "num" as int, if there is an int
            }
            else
            {
                // in here, there is no num, and str did not have an int
            }

            var dict = new Dictionary<string, string>();

            if (dict.TryGetValue("food", out var value))
            {
                // in here, we have "value" if there was anything for key "food"
            }
            else
            {
                // in here, there was nothing at "food" in that dictionary
            }

            var a = "abc";
            var b = "cba";
            var c = 1234;
            Swap(ref a, ref b);
        }

        // uses "ref" to swap two variables' values.
        static void Swap<T>(ref T one, ref T two)
        {
            T swap = one;
            one = two;
            two = swap;
        }
    }
}
