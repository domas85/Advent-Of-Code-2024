using DSA;
using System.Linq;



namespace AOC
{


    class Day12
    {
        public static string[,] garden;

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();

            SolvePart1();

            //SolvePart2();

            Console.ReadLine();

        }

        public static void SolvePart2()
        {

        }

        public static void SolvePart1()
        {

        }


        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day12\\Input.txt");

            if (sr != null)
            {
                garden = new string[sr[0].Length, sr.Length]; 

                for (int y = 0; y < sr.Length; y++)
                {
                    for (int x = 0; x < sr[y].Length; x++)
                    {
                        garden[x,y] = sr[y][x].ToString();
                    }
                }
            }
        }

    }
}