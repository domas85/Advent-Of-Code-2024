using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public struct Rules
    {
        public int thus_number;
        public List<int> before;
        public List<int> after;
    }


    public class Day5
    {
        public static string input;
        public static Dictionary<int, Rules> rules = new Dictionary<int, Rules>();
        public static void Run()
        {
            Console.Write("Starting... \n ");
            ReadInput();

            SolvePart1();
            //SolvePart2();
        }

        public static void SolvePart2()
        {

        }

        public static void SolvePart1()
        {
            var number = new Rules();

            number.thus_number = 1;
            number.before = new List<int>();
            number.after = new List<int>();
            number.before.Add(21);
            number.after.Add(23);

            rules.Add(number.thus_number, number);

        }

        public static void ReadInput()
        {

            StreamReader sr = new StreamReader("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day5\\Sample.txt");

            string line = "";

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    var strArray = line.Split("|", StringSplitOptions.None);
                    if (line.Length <= 1) break;
                    int[] intArray = System.Array.ConvertAll(strArray, num => int.Parse(num));

                    left.Add(intArray[0]);
                    right.Add(intArray[1]);
                }
            }
            for (int i = 0; i < left.Count; i++)
            {
                Console.WriteLine(left[i]);
            }
            Console.WriteLine("");
            for (int i = 0; i < left.Count; i++)
            {
                Console.WriteLine(right[i]);
            }
        }
    }
}
