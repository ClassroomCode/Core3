using System;
using System.Collections.Generic;
using System.Linq;

namespace DefEx
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new List<int> { 1, 2, 3, 4, 5 };

            var evens = nums.Where(n => n % 2 == 0);
            evens = evens.Where(n => n > 3);

            foreach (var n in evens)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("----------");

            nums.Add(8);

            foreach (var n in evens)
            {
                Console.WriteLine(n);
            }

            Console.ReadKey();
        }
    }
}
