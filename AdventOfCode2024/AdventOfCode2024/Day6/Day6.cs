using System;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AOC
{
    class Day6 : IDay<int>
    {
        public static char[,] mapRaw;
        public static char[,] mapClean;
        public static int guardX = 0;
        public static int guardY = 0;
        public static int sizeX = 0;
        public static int sizeY = 0;
        public static List<Vector2> startingPoints = new List<Vector2>();

        public static int loops = 0;
        public static int possibleObstructions = 0;
        public static Vector2 direction;
        public static bool brekLoop;

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();

            sizeX = mapRaw.GetLength(0);
            sizeY = mapRaw.GetLength(1);

            //SolvePart1();

            SolvePart2();

        }

        public static void SolvePart2()
        {
            PrintMap(mapRaw);

            mapClean = new char[sizeX, sizeY];
            Array.Copy(mapRaw, mapClean, mapRaw.Length);

            MoveGuardFromStartingPosition(); //Get the map with all distinct moves

            PrintMap(mapRaw);

            CheckPossibleObstructions();

            Console.WriteLine("there can be: " + possibleObstructions + " obstructions placed to make a loop");

        }

        public static void CheckPossibleObstructions()
        {
            char[,] mapCopy = new char[sizeX, sizeY];
            Array.Copy(mapRaw, mapCopy, mapRaw.Length);

            for (int y = 0; y < mapCopy.GetLength(1); y++)
            {
                for (int x = 0; x < mapCopy.GetLength(0); x++)
                {
                    if (mapCopy[x, y] == 'X' || mapClean[x, y] == '^')
                    {

                        Array.Copy(mapClean, mapRaw, mapClean.Length);
                        mapRaw[x, y] = '#';
                        MoveGuardFromStartingPosition();
                        //mapRaw[x, y] = 'O';
                        //PrintMap(mapRaw);

                        startingPoints.Clear();
                        loops = 0;
                    }
                }
            }
        }

        public static void SolvePart1()
        {
            PrintMap(mapRaw);

            MoveGuardFromStartingPosition();

            PrintMap(mapRaw);

            CountDistinctPositions();
            Console.WriteLine("loops: " + possibleObstructions);

        }
        public static void MoveGuardFromStartingPosition()
        {
            for (int y = 0; y < mapRaw.GetLength(1); y++)
            {
                for (int x = 0; x < mapRaw.GetLength(0); x++)
                {
                    if (mapRaw[x, y].Equals('^'))
                    {
                        guardX = x;
                        guardY = y;
                    }
                }
            }

            startingPoints.Add(new Vector2(guardX, guardY));

            direction = new Vector2(0, -1);
            brekLoop = true;
            while (brekLoop)
            {
                switch (direction.X, direction.Y)
                {
                    case (0, -1):
                        if (loops > 0 && startingPoints.Contains(new Vector2(guardX, guardY)))
                        {
                            brekLoop = false;
                            possibleObstructions++;
                            //Console.WriteLine("this has a Loop");
                            continue;
                        }
                        direction = TraverseUp(guardX, guardY);

                        break;
                    case (1, 0):
                        direction = TraverseRight(guardX, guardY);

                        break;
                    case (0, 1):
                        direction = TraverseDown(guardX, guardY);

                        break;
                    case (-1, 0):
                        direction = TraverseLeft(guardX, guardY);

                        break;
                }

                //if (direction == new Vector2(0, -1))
                //{
                //    if (loops > 0 && startingPoints.Contains(new Vector2(guardX, guardY)))
                //    {
                //        brekLoop = false;
                //        possibleObstructions++;
                //        //Console.WriteLine("this has a Loop");
                //        continue;
                //    }

                //    direction = TraverseUp(guardX, guardY);
                //}
                //else if (direction == new Vector2(1, 0))
                //{
                //    direction = TraverseRight(guardX, guardY);
                //}
                //else if (direction == new Vector2(0, 1))
                //{
                //    direction = TraverseDown(guardX, guardY);
                //}
                //else if (direction == new Vector2(-1, 0))
                //{
                //    direction = TraverseLeft(guardX, guardY);
                //}
            }
            //TraverseUp(guardX, guardY);
        }

        public static Vector2 TraverseUp(int posX, int posY)
        {
            int prevY = 0;
            for (int y = posY; y >= 0; y--)
            {
                if (mapRaw[posX, y] != '#')
                {
                    mapRaw[posX, y] = 'X';
                }
                else
                {
                    guardX = posX;
                    guardY = prevY;


                    return new Vector2(1, 0);
                }
                prevY = y;
            }
            brekLoop = false;
            return Vector2.Zero;
        }

        public static Vector2 TraverseRight(int posX, int posY)
        {
            int prevX = 0;
            for (int x = posX; x < sizeX; x++)
            {
                if (mapRaw[x, posY] != '#')
                {
                    mapRaw[x, posY] = 'X';
                }
                else
                {
                    guardX = prevX;
                    guardY = posY;

                    return new Vector2(0, 1);
                }
                prevX = x;
            }
            brekLoop = false;
            return Vector2.Zero;
        }

        public static Vector2 TraverseDown(int posX, int posY)
        {
            int prevY = 0;
            for (int y = posY; y < sizeY; y++)
            {
                if (mapRaw[posX, y] != '#')
                {
                    mapRaw[posX, y] = 'X';
                }
                else
                {
                    guardX = posX;
                    guardY = prevY;

                    return new Vector2(-1, 0);
                }
                prevY = y;
            }
            brekLoop = false;
            return Vector2.Zero;
        }

        public static Vector2 TraverseLeft(int posX, int posY)
        {
            int prevX = 0;
            for (int x = posX; x >= 0; x--)
            {
                if (mapRaw[x, posY] != '#')
                {
                    mapRaw[x, posY] = 'X';
                }
                else
                {
                    guardX = prevX;
                    guardY = posY;
                    if (startingPoints.Contains(new Vector2(guardX, guardY)))
                    {
                        loops++;
                    }
                    else
                    {
                        startingPoints.Add(new Vector2(guardX, guardY));
                    }

                    return new Vector2(0, -1);
                }
                prevX = x;
            }
            brekLoop = false;
            return Vector2.Zero;
        }

        public static void CountDistinctPositions()
        {
            int count = 0;
            for (int y = 0; y < mapRaw.GetLength(1); y++)
            {
                for (int x = 0; x < mapRaw.GetLength(0); x++)
                {
                    if (mapRaw[x, y] == 'X')
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
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day6\\Input.txt");

            if (sr != null)
            {
                mapRaw = new char[sr[0].Length, sr.Length];

                int x = 0;

                for (int y = 0; y < sr.Length; y++)
                {
                    foreach (char s in sr[y])
                    {
                        mapRaw[x, y] = s;
                        x++;
                    }
                    x = 0;
                }
            }
        }


        #region StolenSolution


        const int STRAIGHT = 0;
        const int UP = -1;

        readonly int rows;
        readonly int cols;
        char[,] map;

        readonly int startX;
        readonly int startY;

        public Day6()
        {
            string[] input = File.ReadLines("../../../Day6/Input.txt").ToArray();
            rows = input.Length;
            cols = input[0].Length;

            map = new char[rows, cols];

            // Find guard starting position, and copy input into a mutable 2D char-array
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    map[j, i] = input[i][j];
                    if (input[i][j] != '.' && input[i][j] != '#')
                    {
                        startX = j;
                        startY = i;
                    }
                }
        }

        public int RunPart1()
        {
            return Move(startX, startY, STRAIGHT, UP).visited.Count();
        }

        public int RunPart2()
        {
            int loops = 0;
            foreach ((int x, int y) in Move(startX, startY, STRAIGHT, UP).visited.Skip(1))
            {
                // Place obstacle at every spot in the path once (except start), and test for loops.
                char old = map[x, y];
                map[x, y] = '#';
                loops += Move(startX, startY, STRAIGHT, UP).loop;
                map[x, y] = old;
            }

            return loops;
        }

        // Walk the guard starting from a certain position into a certain direction.
        // Ends when guard goes out of bounds, or enters a loop.
        private (IEnumerable<(int, int)> visited, int loop) Move(int x, int y, int dx, int dy)
        {
            HashSet<(int, int, int, int)> visited = new HashSet<(int, int, int, int)>();

            int looped = 0;
            while (true)
            {
                // Looped back around to a tile AND direction that is already visited.
                if (!visited.Add((x, y, dx, dy)))
                {
                    looped = 1;
                    break;
                }

                int nextX = x + dx;
                int nextY = y + dy;

                // Moves out of bounds, done.
                if (nextX < 0 || nextX >= cols || nextY < 0 || nextY >= rows) break;

                // Rotate by 90 degrees if obstacle ahead
                if (map[nextX, nextY] == '#')
                {
                    int temp = dy;
                    dy = dx;
                    dx = -temp;
                    continue;
                }

                // Move into direction
                x += dx;
                y += dy;
            }

            // Select only tiles with distinct x,y coordinates.
            return (visited.Select(t => (t.Item1, t.Item2)).Distinct(), looped);
        }

        #endregion

    }
    interface IDay<T>
    {
        T RunPart1();
        T RunPart2();
    }
}
