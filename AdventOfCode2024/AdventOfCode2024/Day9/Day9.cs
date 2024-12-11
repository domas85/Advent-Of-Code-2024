using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace AOC
{
    class Day9
    {
        public static List<int> blocks = new List<int>();
        public static List<int> freeSpace = new List<int>();
        public static List<string> disk = new List<string>();


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
            GetFileBlock();
            ProcessFileBlock();
        }

        public static void ProcessFileBlock()
        {
            for (int i = disk.Count; i > 0; i--)
            {
                for (int x = 0; x < disk.Count; x++)
                {
                    // move blocks
                }
            }
        }

        public static void GetFileBlock()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int x = 0; x < blocks[i]; x++)
                {
                    disk.Add(i.ToString());
                }
                if (i < freeSpace.Count)
                {
                    for (int x = 0; x < freeSpace[i]; x++)
                    {
                        disk.Add(".");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine(string.Join("", disk));
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllText("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day9\\Sample.txt");

            Console.WriteLine("input: " + sr);

            if (sr != null)
            {
                for (int y = 0; y < sr.Length; y += 2)
                {
                    blocks.Add(int.Parse(sr[y].ToString()));
                    if (y + 1 < sr.Length)
                    {
                        freeSpace.Add(int.Parse(sr[y + 1].ToString()));
                    }
                }
            }
        }
    }
}