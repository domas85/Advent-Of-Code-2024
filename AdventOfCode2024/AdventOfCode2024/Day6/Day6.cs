using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{

    public class Day6
    {
        public static char[,] map;
        public static int guardX = 0;
        public static int guardY = 0;


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
            FindGuardStartingPosition();




        }

        public static void FindGuardStartingPosition()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].Equals('^'))
                    {
                        guardX = x;
                        guardY = y;
                    }
                }
            }
            PrintMap();
        }

        public static void TraverseUp()
        {

        }

        public static void PrintMap()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(string.Join(" ", map[x, y]));
                }
                Console.WriteLine("");
            }
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day6\\Sample.txt");

            if (sr != null)
            {
                map = new char[sr[0].Length, sr.Length];

                int x = 0;

                for (int i = 0; i < sr.Length; i++)
                {
                    foreach (char s in sr[i])
                    {
                        map[i, x] = s;
                        x++;
                    }
                    x = 0;
                }
            }
        }
    }
}
