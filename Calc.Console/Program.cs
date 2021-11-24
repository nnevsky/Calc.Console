//using Calc.Lib.Arithmetic;
using Calc.Lib1;
using System;

namespace Calc.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("You can type example use arithmetics or enter path to text file with examples...");

            string input= System.Console.ReadLine();
            while(!string.IsNullOrEmpty(input))
            {
                double result = new Number(input).Result;
                System.Console.WriteLine($"{input} = {result}");

                input = System.Console.ReadLine();
            }
        }
    }
}
