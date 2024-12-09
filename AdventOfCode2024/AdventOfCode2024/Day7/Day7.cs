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
        public static int[] results;
        public static List<int[]> inputs = new List<int[]>();

        public static List<int> resultsAfterCalc = new List<int>();
        public static int answer;

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
                Console.WriteLine("It Has these possible results: ");
                Console.WriteLine("");

                foreach(int result in resultsAfterCalc)
                {
                    Console.WriteLine(result);
                }

                if (resultsAfterCalc.Contains(results[i]))
                {
                    Console.WriteLine("has a correct combination \n");

                    answer += results[i];
                    resultsAfterCalc.Clear();
                }
                else
                {
                    resultsAfterCalc.Clear();
                    Console.WriteLine("does not have a correct combination \n");
                }
            }
            Console.WriteLine("The total calibration result is " + answer);
        }
        public static void Addition(int[] number, int startIndex, int result)
        {
            result += number[startIndex];
            if (startIndex < number.Length - 1)
            {
                Multiplication(number, startIndex + 1, result);
                Addition(number, startIndex + 1, result);
            }
            else
            {
                resultsAfterCalc.Add(result);

            }
        }

        public static void Multiplication(int[] number, int startIndex, int result)
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
                results = new int[sr.Length];
                for (int i = 0; i < sr.Length; i++)
                {
                    var inputsRaw = Regex.Matches(sr[i], @"(\d+)");

                    results[i] = int.Parse(inputsRaw[0].ToString());

                    int[] intArray = new int[inputsRaw.Count - 1];
                    for (int j = 1; inputsRaw.Count > j; j++)
                    {
                        intArray[j - 1] = int.Parse(inputsRaw[j].ToString());
                    }
                    inputs.Add(intArray);
                }
            }
        }


    }
}
