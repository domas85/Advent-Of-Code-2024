using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AOC
{
    class Day7
    {
        public static long[] results;
        public static List<long[]> inputs = new List<long[]>();

        public static List<long> resultsAfterCalc = new List<long>();
        public static long answer;

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();

            SolvePart1();

            //SolvePart2();

        }

        public static void SolvePart2()
        {

        }


        public static void SolvePart1()
        {

            for (int i = 0; i < inputs.Count; i++)
            {
                Addition(inputs[i], 0, 0);
                Console.WriteLine("Result: " + results[i]);
                Console.WriteLine("Input: " + string.Join(" ", inputs[i]));

                if (resultsAfterCalc.Contains(results[i]))
                {
                    Console.WriteLine("Of the "+ resultsAfterCalc.Count +" possible combinations there is a correct combination \n");

                    answer += results[i];
                    resultsAfterCalc.Clear();
                }
                else
                {
                    resultsAfterCalc.Clear();
                    //Console.WriteLine("does not have a correct combination \n");
                }
            }
            Console.WriteLine("The total calibration result is " + answer);
        }
        public static void Addition(long[] number, int startIndex, long result)
        {
            result += number[startIndex];
            if (startIndex < number.Length - 1)
            {
                Multiplication(number, startIndex + 1, result);
                Addition(number, startIndex + 1, result);
                JoiningNumbers(number, startIndex + 1, result); // for part 2
            }
            else
            {
                resultsAfterCalc.Add(result);
            }
        }

        public static void Multiplication(long[] number, int startIndex, long result)
        {
            if (startIndex == 0)
            {
                result = 1;
            }

            result *= number[startIndex];
            if (startIndex < number.Length - 1)
            {
                Addition(number, startIndex + 1, result);
                Multiplication(number, startIndex + 1, result);
                JoiningNumbers(number, startIndex + 1, result); // for part 2
            }
            else
            {
                resultsAfterCalc.Add(result);
            }
        }

        public static void JoiningNumbers(long[] number, int startIndex, long result)
        {
            long[] joinArray = new long[2];
            joinArray[0] = result;
            joinArray[1] = number[startIndex];

            var temp = string.Join("", joinArray);

            result = long.Parse(temp);

            if (startIndex < number.Length - 1)
            {
                Addition(number, startIndex + 1, result);
                Multiplication(number, startIndex + 1, result);
                JoiningNumbers(number, startIndex + 1, result);
            }
            else
            {
                resultsAfterCalc.Add(result);
            }
        }


        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day7\\Input.txt");

            if (sr != null)
            {
                results = new long[sr.Length];
                for (int i = 0; i < sr.Length; i++)
                {
                    var inputsRaw = Regex.Matches(sr[i], @"(\d+)");

                    results[i] = long.Parse(inputsRaw[0].ToString());

                    long[] intArray = new long[inputsRaw.Count - 1];
                    for (int j = 1; inputsRaw.Count > j; j++)
                    {
                        intArray[j - 1] = long.Parse(inputsRaw[j].ToString());
                    }
                    inputs.Add(intArray);
                }
            }
        }
    }
}
