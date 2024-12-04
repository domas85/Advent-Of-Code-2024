using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public class Day3
    {
        public static List<string> input = new List<string>();
        public static int result = 0;
        public static bool isEnabled = true;

        public static void Run()
        {
            Console.Write("Starting... \n ");
            ReadInput();

            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            int count = 0;
            for (int i = 0; i < input.Count; i++)
            {
                var toCheck = Regex.Match(input[i], @"(mul\(\d+\,\d+\))").ToString();

                if (input[i].Equals("do()"))
                {
                    isEnabled = true;
                }
                if (input[i].Equals("don't()"))
                {
                    isEnabled = false;
                }


                if (isEnabled && input[i].Equals(toCheck))
                {
                    var mul = input[i];
                    var nums = Regex.Matches(mul, @"(\d+)");

                    var num1 = int.Parse(nums[0].ToString());
                    var num2 = int.Parse(nums[1].ToString());

                    Console.Write("\n multiplying " + num1 + " with " + num2);

                    result += num1 * num2;
                    count++;
                }
            }
            Console.Write("\n \n the answer is: " + result);
            Console.Write("\n and there were: " + count + " multiplication calculations \n ");
        }

        public static void SolvePart1()
        {
            int count = 0;
            for (int i = 0; i < input.Count; i++)
            {
                var toCheck = Regex.Match(input[i], @"(mul\(\d+\,\d+\))").ToString();

                if (input[i].Equals(toCheck))
                {
                    var mul = input[i];
                    var nums = Regex.Matches(mul, @"(\d+)");

                    var num1 = int.Parse(nums[0].ToString());
                    var num2 = int.Parse(nums[1].ToString());

                    Console.Write("\n multiplying " + num1 + " with " + num2);

                    result += num1 * num2;
                    count++;
                }
            }
            Console.Write("\n \n the answer is: " + result);
            Console.Write("\n and there were: " + count + " multiplication calculations \n ");
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllText("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day3\\Input.txt");

            if (sr != null)
            {
                var inputs = Regex.Split(sr, @"(mul\(\d+\,\d+\))|(do\(\))|(don't\(\))");


                for (int i = 0; i < inputs.Length; i++)
                {
                    var toCheck = Regex.Match(inputs[i], @"(mul\(\d+\,\d+\))").ToString();

                    if (inputs[i].Equals(toCheck) && !string.IsNullOrEmpty(inputs[i]) || inputs[i].Equals("do()") || inputs[i].Equals("don't()"))
                    {
                        input.Add(inputs[i].ToString());
                    }
                }
            }

            //if (line != null && line.Contains(" "))
            //{
            //    var strArray = inputs.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            //    int[] intArray = System.Array.ConvertAll(strArray, num => int.Parse(num));
            //    List<int> temp = new List<int>();
            //    temp.AddRange(intArray);
            //    allReports.Add(temp);
            //}

        }
    }
}
