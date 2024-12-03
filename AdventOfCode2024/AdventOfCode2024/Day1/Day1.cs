using System;

namespace AOC
{
    public class Day1
    {
        public static string line = "";
        public static List<int> listLeft = new List<int>();
        public static List<int> listRight = new List<int>();

        public static int distance;
        public static int similarity;

        public static void Run()
        {
            Console.Write("Starting...");
            ReadInput();
            SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            SortLists();
            int count = 0;
            for (int i = 0; i < listLeft.Count; i++)
            {
                for(int x  = 0; x < listRight.Count; x++)
                {
                    if(listLeft[i] == listRight[x])
                    {
                        count++;
                    }
                }
                similarity += listLeft[i] * count;
                count = 0;
            }

            Console.WriteLine("\n The total similarity is: " + similarity);
        }

        public static void SolvePart1()
        {
            SortLists();

            for (int i = 0; i < listLeft.Count; i++)
            {
                distance += (int)MathF.Abs(listLeft[i] - listRight[i]);
            }

            Console.WriteLine("\n The total distance is: " + distance);
        }
        public static void SortLists()
        {
            listLeft.Sort();
            listRight.Sort();
        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day1\\Input.txt");

            while (line != null)
            {
                line = sr.ReadLine();

                Console.WriteLine("\n " + line);

                if (line != null && line.Contains(" "))
                {
                    var split = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    listLeft.Add(int.Parse(split[0]));
                    listRight.Add(int.Parse(split[1]));
                    //Console.WriteLine("\n line 1: " + split[0]);
                    //Console.WriteLine("\n line 2: " + split[1]);
                }
            }
        }
    }
}
