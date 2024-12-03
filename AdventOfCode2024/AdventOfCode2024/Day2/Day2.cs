using System;
using System.Diagnostics;
using System.Linq;


namespace AOC
{
    public class Day2
    {
        public static string line = "";
        public static List<List<int>> allReports = new List<List<int>>();



        public static void Run()
        {
            Console.Write("Starting...");
            ReadInput();


            //StolenSolution();
            //SolvePart1();
            SolvePart2New();
        }

        public static void SolvePart2New()
        {
            int count = 0;
            int repotCount = 0;
            int reportErrorCount = 0;
            foreach (List<int> report in allReports)
            {
                repotCount++;
                int result = CheckIfSafe(report);
                if (result == -1)
                {
                    count++;
                }
                else
                {
                    report.RemoveAt(result);

                    if (CheckIfSafe(report) == -1)
                    {
                        count++;
                    }
                    else
                    {
                        reportErrorCount++;
                    }
                }
            }
            Console.WriteLine("\n there are: " + repotCount + " reports");
            Console.WriteLine("\n there are: " + count + " safe reports");
            Console.WriteLine("\n and there are: " + reportErrorCount + " reports with errors");
        }
        public static void SolvePart2()
        {
            int count = 0;
            int removeAmount = 0;
            foreach (List<int> report in allReports)
            {
                int x = 0;
                for (int i = 0; i < report.Count - 1; i++)
                {
                    int changeAmount = (int)MathF.Abs(report[i] - report[i + 1]);
                    int nextChangeAmount = 0;
                    if (i + 2 < report.Count)
                    {
                        nextChangeAmount = (int)MathF.Abs(report[i] - report[i + 2]);
                    }

                    if (changeAmount > 3 || changeAmount < 1)
                    {
                        removeAmount++;
                        if (nextChangeAmount > 3 || nextChangeAmount < 1)
                        {
                            x = 0;
                            removeAmount++;
                            break;
                        }
                        if (i + 2 < report.Count)
                        {
                            if (x < 0 && report[i] < report[i + 2] || x > 0 && report[i] > report[i + 2])
                            {
                                x = 0;
                                removeAmount++;
                                break;
                            }
                        }
                        i++;
                    }

                    if (x < 0 && report[i] < report[i + 1] || x > 0 && report[i] > report[i + 1])
                    {
                        removeAmount++;
                        if (i + 2 < report.Count)
                        {
                            if (x < 0 && report[i] < report[i + 2] || x > 0 && report[i] > report[i + 2])
                            {
                                x = 0;
                                removeAmount++;
                                break;
                            }
                        }
                        if (nextChangeAmount > 3 || nextChangeAmount < 1)
                        {
                            x = 0;
                            removeAmount++;
                            break;
                        }
                        i++;
                    }


                    if (x == 0)
                    {
                        x = report[i] < report[i + 1] ? 1 : -1;
                    }

                }

                if (x != 0 || removeAmount <= 1)
                {
                    count++;
                    Console.WriteLine("\n report is Safe :)");
                }
                else
                {
                    Console.WriteLine("\n report is Unsafe :(");
                }
                removeAmount = 0;
            }
            Console.WriteLine("\n there are: " + count + " safe reports");
        }
        public static void SolvePart1()
        {
            int count = 0;
            foreach (List<int> report in allReports)
            {
                if (CheckIfSafe(report) == -1)
                {
                    count++;
                }
            }
            Console.WriteLine("\n there are: " + count + " safe reports");
        }

        public static int CheckIfSafe(List<int> report)
        {
            int x = 0;
            int indexWhereFail = -1;
            for (int i = 0; i < report.Count - 1; i++)
            {
                int changeAmount = (int)MathF.Abs(report[i] - report[i + 1]);
                
                indexWhereFail = i; // was just i before

                if (changeAmount > 3 || changeAmount < 1)
                {
                    x = 0;
                    break;
                }

                if (x < 0 && report[i] < report[i + 1] || x > 0 && report[i] > report[i + 1])
                {
                    x = 0;
                    break;
                }

                if (x == 0)
                {
                    x = report[i] < report[i + 1] ? 1 : -1;
                }

            }

            if (x != 0)
            {

                Console.WriteLine("\n "+  string.Join(" ", report) + "\n report is Safe :)");
                return -1;
            }
            else
            {
                Console.WriteLine("\n "+  string.Join(" ", report) + "\n report is Unsafe :(");
                return indexWhereFail;
            }

        }

        public static void ReadInput()
        {
            StreamReader sr = new StreamReader("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day2\\Input.txt");

            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null && line.Contains(" "))
                {
                    var strArray = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                    int[] intArray = System.Array.ConvertAll(strArray, num => int.Parse(num));
                    List<int> temp = new List<int>();
                    temp.AddRange(intArray);
                    allReports.Add(temp);
                }
            }
        }

        public static void StolenSolution()
        {
            var input = File.ReadLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day2\\Input.txt").Select(s => s.Split(' ').Select(int.Parse));

            Console.WriteLine(input.Count(IsSafe1));
            Console.WriteLine(input.Count(IsSafe2));

            bool IsSafe1(IEnumerable<int> a) =>
                IsSafe(a.Zip(a.Skip(1), (x, y) => x - y));

            bool IsSafe2(IEnumerable<int> a) =>
                Enumerable.Range(0, a.Count())
                    .Any(i => IsSafe1(a.Take(i).Concat(a.Skip(i + 1))));

            bool IsSafe(IEnumerable<int> b) =>
                b.All(x => x >= -3 && x <= -1) ||
                b.All(x => x >= 1 && x <= 3);
        }
    }
}
