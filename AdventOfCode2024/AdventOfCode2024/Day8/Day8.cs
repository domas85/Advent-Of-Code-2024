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
    class Day8
    {
        public static char[,] mapInput;
        public static int answer;
        public static List<Vector2> overlapingPositions = new List<Vector2>();

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();
            PrintMap(mapInput);

            SolvePart1();

            //SolvePart2();
        }

        public static void SolvePart2()
        {

        }


        public static void SolvePart1()
        {
            answer = 0;
            SearchForFrequencies();
            Console.WriteLine("The unique position count is: " + answer);

            //CountDistinctPositions();
        }

        public static void SearchForFrequencies()
        {
            char NodeChar = '.';
            Vector2 NodePos = new Vector2();
            for (int y = 0; y < mapInput.GetLength(1); y++)
            {
                for (int x = 0; x < mapInput.GetLength(0); x++)
                {
                    if (mapInput[x, y] != '#' && mapInput[x, y] != '.')
                    {
                        NodeChar = mapInput[x, y];
                        NodePos.X = x;
                        NodePos.Y = y;

                        SearchForOtherFrequenciesFromTheSameNode(NodeChar, NodePos);
                    }
                }
            }
        }

        public static void SearchForOtherFrequenciesFromTheSameNode(char node, Vector2 pos)
        {
            for (int y = 0; y < mapInput.GetLength(1); y++)
            {
                for (int x = 0; x < mapInput.GetLength(0); x++)
                {
                    if (mapInput[x, y] == node && x != pos.X && y != pos.Y)
                    {
                        int offsetOtherNodesX = (int)(x - pos.X);
                        int offsetOtherNodesY = (int)(y - pos.Y);
                        int offsetThisNodeX = (int)(pos.X + ((x - pos.X) * -1));
                        int offsetThisNodeY = (int)(pos.Y + ((y - pos.Y) * -1));

                        // don know why dont work
                        //if(offsetThisNodeX % 2 != 0 && offsetThisNodeY % 2 != 0 || x + offsetOtherNodesX % 2 != 0 && y + offsetOtherNodesY % 2 != 0) break;


                        if (x + offsetOtherNodesX < mapInput.GetLength(0) && y + offsetOtherNodesY < mapInput.GetLength(1) && x + offsetOtherNodesX > 0 && y + offsetOtherNodesY > 0)
                        {
                            if (mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] == '.' || mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] != '#')
                            {
                                var tempRegex = Regex.Match(mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY].ToString(), @"([^.#" + node + @"])");
                                if (tempRegex.ToString() != "" && !overlapingPositions.Contains(new Vector2(x,y)))
                                {
                                    overlapingPositions.Add(new Vector2 (x, y));
                                    answer++;
                                }


                                if (mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] == '.')
                                {
                                    answer++;
                                    mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] = '#';
                                }
                            }

                        }


                        if (offsetThisNodeX < mapInput.GetLength(0) && offsetThisNodeY < mapInput.GetLength(1) && offsetThisNodeX >= 0 && offsetThisNodeY >= 0)
                        {

                            if (mapInput[offsetThisNodeX, offsetThisNodeY] == '.' || mapInput[offsetThisNodeX, offsetThisNodeY] != '#')
                            {
                                var tempRegex = Regex.Match(mapInput[offsetThisNodeX, offsetThisNodeY].ToString(), @"([^.#" + node + @"])");
                                if(tempRegex.ToString() != "" && !overlapingPositions.Contains(new Vector2(pos.X, pos.Y)))
                                {
                                    overlapingPositions.Add(new Vector2(pos.X, pos.Y));
                                    answer++;
                                }


                                if (mapInput[offsetThisNodeX, offsetThisNodeY] == '.')
                                {
                                    answer++;
                                    mapInput[offsetThisNodeX, offsetThisNodeY] = '#';
                                }
                            }
                        }

                    }


                }
            }
            PrintMap(mapInput);
        }

        public static void CountDistinctPositions()
        {
            int count = 0;
            for (int y = 0; y < mapInput.GetLength(1); y++)
            {
                for (int x = 0; x < mapInput.GetLength(0); x++)
                {
                    if (mapInput[x, y] == '#')
                    {
                        count++;
                    }
                }
            }
            Console.WriteLine("The distinct positions count is: " + count);
        }

        public static void PrintMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.Write(string.Join(" ", map[x, y]));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day8\\Input.txt");

            if (sr != null)
            {
                mapInput = new char[sr[0].Length, sr.Length];

                int x = 0;

                for (int y = 0; y < sr.Length; y++)
                {
                    foreach (char s in sr[y])
                    {
                        mapInput[x, y] = s;
                        x++;
                    }
                    x = 0;
                }
            }
        }
    }
}
