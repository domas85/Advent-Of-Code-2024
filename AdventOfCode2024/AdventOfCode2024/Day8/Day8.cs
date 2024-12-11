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
        public static List<Vector2> antiNotes = new List<Vector2>();

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();
            PrintMap(mapInput);

            //SolvePart1();

            SolvePart2();

            //StolenSolution();
        }

        public static void SolvePart2()
        {
            answer = 0;
            SearchForNodePart2();
            PrintMap(mapInput);
            Console.WriteLine("The unique position count is: " + answer);
        }

        public static void SearchForNodePart2()
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
                        answer++;
                        SearchForFrequenciesOfTheSameCharRecursive(NodeChar, NodePos);
                    }
                }
            }
        }

        public static void SearchForFrequenciesOfTheSameCharRecursive(char node, Vector2 pos)
        {
            for (int y = 0; y < mapInput.GetLength(1); y++)
            {
                for (int x = 0; x < mapInput.GetLength(0); x++)
                {
                    if (mapInput[x, y] == node && x != pos.X && y != pos.Y)
                    {
                        int offsetOtherNodesX = (int)(x - pos.X);
                        int offsetOtherNodesY = (int)(y - pos.Y);
                        int offsetThisNodeX = (int)((x - pos.X) * -1);
                        int offsetThisNodeY = (int)((y - pos.Y) * -1);

                        // don know why dont work
                        if ((x + offsetOtherNodesX) % 2 == 0 && (y + offsetOtherNodesY) % 2 == 0) break;
                        
                        SearchFrequenciesOnFirstSide(node, x, y, offsetOtherNodesX, offsetOtherNodesY, 0, 0);

                        SearchFrequenciesOnOtherSide(node, pos, offsetThisNodeX, offsetThisNodeY, 0, 0);
                    }
                }
            }
            
        }

        public static void SearchFrequenciesOnFirstSide(char node, int x, int y, int offsetOtherNodesX, int offsetOtherNodesY, int nextX, int nextY)
        {
            nextX += offsetOtherNodesX;
            nextY += offsetOtherNodesY;


            if (x + nextX < mapInput.GetLength(0) && y + nextY < mapInput.GetLength(1) && x + nextX >= 0 && y + nextY >= 0)
            {
                if (mapInput[x + nextX, y + nextY] == '.' || mapInput[x + nextX, y + nextY] != '#')
                {
                    var tempRegex = Regex.Match(mapInput[x + nextX, y + nextY].ToString(), @"([^.#" + node + @"])");
                    if (tempRegex.ToString() != "" && !overlapingPositions.Contains(new Vector2(x + nextX, y + nextY)))
                    {
                        //answer++;
                        overlapingPositions.Add(new Vector2(x + nextX, y + nextY));
                    }

                    if (mapInput[x + nextX, y + nextY] == '.')
                    {
                        answer++;
                        mapInput[x + nextX, y + nextY] = '#';
                    }
                }
                SearchFrequenciesOnFirstSide(node, x, y, offsetOtherNodesX, offsetOtherNodesY, nextX, nextY);
            }
        }

        public static void SearchFrequenciesOnOtherSide(char node, Vector2 pos, int offsetThisNodeX, int offsetThisNodeY, int nextX, int nextY)
        {
            nextX += offsetThisNodeX;
            nextY += offsetThisNodeY;


            if ((int)pos.X + nextX < mapInput.GetLength(0) && (int)pos.Y + nextY < mapInput.GetLength(1) && (int)pos.X + nextX >= 0 && (int)pos.Y + nextY >= 0)
            {

                if (mapInput[(int)pos.X + nextX, (int)pos.Y + nextY] == '.' || mapInput[(int)pos.X + nextX, (int)pos.Y + nextY] != '#')
                {
                    var tempRegex = Regex.Match(mapInput[(int)pos.X + nextX, (int)pos.Y + nextY].ToString(), @"([^.#" + node + @"])");
                    if (tempRegex.ToString() != "" && !overlapingPositions.Contains(new Vector2(pos.X + nextX, pos.Y + nextY)))
                    {
                        //answer++;
                        overlapingPositions.Add(new Vector2(pos.X + nextX, pos.Y + nextY));
                    }

                    if (mapInput[(int)pos.X + nextX, (int)pos.Y + nextY] == '.')
                    {
                        answer++;
                        mapInput[(int)pos.X + nextX, (int)pos.Y + nextY] = '#';
                    }
                }
                SearchFrequenciesOnOtherSide(node, pos, offsetThisNodeX, offsetThisNodeY, nextX, nextY);
            }
        }


        public static void SolvePart1()
        {
            answer = 0;
            SearchForNode();
            Console.WriteLine("The unique position count is: " + answer);
        }

        public static void SearchForNode()
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

                        SearchForFrequenciesOfTheSameChar(NodeChar, NodePos);
                    }
                }
            }
        }

        public static void SearchForFrequenciesOfTheSameChar(char node, Vector2 pos)
        {
            for (int y = 0; y < mapInput.GetLength(1); y++)
            {
                for (int x = 0; x < mapInput.GetLength(0); x++)
                {
                    if (mapInput[x, y] == node && x != pos.X && y != pos.Y)
                    {
                        int offsetOtherNodesX = (int)(x - pos.X);
                        int offsetOtherNodesY = (int)(y - pos.Y);
                        int offsetThisNodeX = (int)((x - pos.X) * -1);
                        int offsetThisNodeY = (int)((y - pos.Y) * -1);

                        // don know why dont work
                        if ((x + offsetOtherNodesX) % 2 == 0 && (y + offsetOtherNodesY) % 2 == 0) break;

                        if (x + offsetOtherNodesX < mapInput.GetLength(0) && y + offsetOtherNodesY < mapInput.GetLength(1) && x + offsetOtherNodesX >= 0 && y + offsetOtherNodesY >= 0)
                        {
                            if (mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] == '.' || mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] != '#')
                            {
                                var tempRegex = Regex.Match(mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY].ToString(), @"([^.#" + node + @"])");
                                if (tempRegex.ToString() != "" && !overlapingPositions.Contains(new Vector2(x + offsetOtherNodesX, y + offsetOtherNodesY)))
                                {
                                    overlapingPositions.Add(new Vector2(x + offsetOtherNodesX, y + offsetOtherNodesY));
                                    answer++;
                                }

                                if (mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] == '.')
                                {
                                    answer++;
                                    mapInput[x + offsetOtherNodesX, y + offsetOtherNodesY] = '#';
                                }
                            }
                        }

                        if ((int)pos.X + offsetThisNodeX < mapInput.GetLength(0) && (int)pos.Y + offsetThisNodeY < mapInput.GetLength(1) && (int)pos.X + offsetThisNodeX >= 0 && (int)pos.Y + offsetThisNodeY >= 0)
                        {

                            if (mapInput[(int)pos.X + offsetThisNodeX, (int)pos.Y + offsetThisNodeY] == '.' || mapInput[(int)pos.X + offsetThisNodeX, (int)pos.Y + offsetThisNodeY] != '#')
                            {
                                var tempRegex = Regex.Match(mapInput[(int)pos.X + offsetThisNodeX, (int)pos.Y + offsetThisNodeY].ToString(), @"([^.#" + node + @"])");
                                if (tempRegex.ToString() != "" && !overlapingPositions.Contains(new Vector2(pos.X + offsetThisNodeX, pos.Y + offsetThisNodeY)))
                                {
                                    overlapingPositions.Add(new Vector2(pos.X + offsetThisNodeX, pos.Y + offsetThisNodeY));
                                    answer++;
                                }

                                if (mapInput[(int)pos.X + offsetThisNodeX, (int)pos.Y + offsetThisNodeY] == '.')
                                {
                                    answer++;
                                    mapInput[(int)pos.X + offsetThisNodeX, (int)pos.Y + offsetThisNodeY] = '#';
                                }
                            }
                        }
                    }
                }
            }
            PrintMap(mapInput);
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

        public static void StolenSolution()
        {
            var sr = "C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day8\\Input.txt";
            RunSolution(sr, true);
        }

        public static void RunSolution(string inputfile, bool isTest)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            long supposedanswer1 = 14;
            long supposedanswer2 = 34;

            var S = File.ReadAllLines(inputfile).ToList();
            long answer1 = 0;
            long answer2 = 0;

            var antinodes = new HashSet<(int x, int y)>();
            var antinodes2 = new HashSet<(int x, int y)>();
            var nodes = new Dictionary<char, List<(int x, int y)>>();
            for (int y = 0; y < S.Count; y++)
            {
                string s = S[y];
                for (int x = 0; x < s.Length; x++)
                {
                    var c = s[x];
                    if (c != '.')
                    {
                        if (!nodes.TryGetValue(c, out var positions)) nodes[c] = positions = new List<(int x, int y)>();
                        positions.Add((x, y));
                    }
                }
            }

            foreach (var node in nodes.Values)
            {
                if (node != null)
                {
                    for (int i = 0; i < node.Count - 1; i++)
                    {
                        var loc1 = node[i];
                        for (int j = i + 1; j < node.Count; j++)
                        {
                            var loc2 = node[j];
                            var dx = loc2.x - loc1.x;
                            var dy = loc2.y - loc1.y;
                            var ax = loc1.x;
                            var ay = loc1.y;
                            int P = 0;
                            while (ax >= 0 && ax < S[0].Length && ay >= 0 && ay < S.Count)
                            {
                                if (P == 1) antinodes.Add((ax, ay));
                                antinodes2.Add((ax, ay));
                                ax -= dx;
                                ay -= dy;
                                P++;
                            }
                            ax = loc2.x;
                            ay = loc2.y;
                            P = 0;
                            while (ax >= 0 && ax < S[0].Length && ay >= 0 && ay < S.Count)
                            {
                                if (P == 1) antinodes.Add((ax, ay));
                                antinodes2.Add((ax, ay));
                                ax += dx;
                                ay += dy;
                                P++;
                            }
                        }
                    }
                }
            }
            answer1 = antinodes.Count;
            answer2 = antinodes2.Count;

            Aoc.w(1, answer1, supposedanswer1, isTest);
            Aoc.w(2, answer2, supposedanswer2, isTest);
            Console.WriteLine("Duration: " + stopwatch.ElapsedMilliseconds.ToString() + " miliseconds.");
        }
    }
}
public static class Aoc
{
    public static void w<T>(int number, T val, T supposedval, bool isTest)
    {
        string? v = (val == null) ? "(null)" : val.ToString();
        string? sv = (supposedval == null) ? "(null)" : supposedval.ToString();

        var previouscolour = Console.ForegroundColor;
        Console.Write("Answer Part " + number + ": ");
        Console.ForegroundColor = (v == sv || !isTest) ? ConsoleColor.Green : ConsoleColor.Red;
        Console.Write(v);
        Console.ForegroundColor = previouscolour;
        if (isTest)
        {
            Console.Write(" ... supposed (example) answer: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(sv);
            Console.ForegroundColor = previouscolour;
        }
        Console.WriteLine();
    }
}
