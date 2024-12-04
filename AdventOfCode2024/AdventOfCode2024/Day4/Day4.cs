using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public class Day4
    {
        public static List<string> input = new List<string>();


        public static void Run()
        {
            Console.Write("Starting... \n ");
            ReadInput();

            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {

        }

        public static void SolvePart1()
        {
            
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllText("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day4\\Input.txt");

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
