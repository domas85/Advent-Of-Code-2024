using DSA;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AOC
{


    class Day11
    {

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

        }


        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day10\\Input.txt");

            if (sr != null)
            {
                

                int x = 0;

                for (int y = 0; y < sr.Length; y++)
                {
                    foreach (char s in sr[y])
                    {
                        
                        x++;
                    }
                    x = 0;
                }
            }
        }

    }
}