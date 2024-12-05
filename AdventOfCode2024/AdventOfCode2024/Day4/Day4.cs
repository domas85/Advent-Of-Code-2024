using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public class Day4
    {
        public static char[,] input;

        public static void Run()
        {
            Console.Write("Starting... \n ");
            ReadInput();

            //SolvePart1();
            SolvePart2();
        }

        public static void SolvePart2()
        {
            int count = 0;
            for (int y = 3; y < input.GetLength(1); y++)
            {
                for (int x = 3; x < input.GetLength(0) - 3; x++)
                {
                    if (input[x, y] != 'A')
                    {
                        continue;
                    }
                    List<string> wordMatrix = new List<string>(new string[4]);

                    wordMatrix[0] += input[x - 1, y - 1]; // wordTopLeft
                    wordMatrix[0] += "A";
                    wordMatrix[0] += input[x + 1, y + 1]; // wordDownRight

                    wordMatrix[1] += input[x + 1, y - 1]; // wordTopRight
                    wordMatrix[1] += "A";
                    wordMatrix[1] += input[x - 1, y + 1]; // wordDownLeft

                    wordMatrix[2] += input[x + 1, y + 1]; // wordDownRight
                    wordMatrix[2] += "A";
                    wordMatrix[2] += input[x - 1, y - 1]; // wordTopLeft

                    wordMatrix[3] += input[x - 1, y + 1]; // wordDownLeft
                    wordMatrix[3] += "A";
                    wordMatrix[3] += input[x + 1, y - 1]; // wordTopRight

                    int occurrences = 0;
                    foreach (string word in wordMatrix)
                    {
                        if (word == "MAS")
                        {
                            occurrences++;
                        }
                        Console.WriteLine(word);
                    }
                    if (occurrences == 2)
                    {
                        count++;
                    }
                    Console.Write("\n");
                }
            }
            Console.WriteLine("the answer is: " + count);
        }

        public static void SolvePart1()
        {
            int count = 0;
            for (int y = 3; y < input.GetLength(1); y++)
            {
                for (int x = 3; x < input.GetLength(0) - 3; x++)
                {
                    if (input[x, y] != 'X')
                    {
                        continue;
                    }
                    string wordRight = "";
                    string wordDownRight = "";
                    string wordDown = "";
                    string wordDownLeft = "";
                    string wordLeft = "";
                    string wordTopLeft = "";
                    string wordTop = "";
                    string wordTopRight = "";

                    List<string> wordMatrix = new List<string>(new string[10]);

                    for (int offset = 0; offset <= 3; offset++)
                    {
                        //if (offset + x > input.GetUpperBound(0) || offset + y > input.GetUpperBound(1) )
                        //{
                        //    continue;
                        //}
                        wordMatrix[0] += input[x + offset, y]; // wordRight

                        wordMatrix[1] += input[x + offset, y + offset]; // wordDownRight

                        wordMatrix[2] += input[x, y + offset]; // wordDown

                        wordMatrix[3] += input[x + offset * -1, y + offset]; // wordDownLeft

                        wordMatrix[4] += input[x + offset * -1, y]; // wordLeft

                        wordMatrix[5] += input[x + offset * -1, y + offset * -1]; // wordTopLeft

                        wordMatrix[6] += input[x, y + offset * -1]; // wordTop

                        wordMatrix[7] += input[x + offset, y + offset * -1]; // wordTopRight
                    }

                    foreach (string word in wordMatrix)
                    {
                        if (word == "XMAS")
                        {
                            count++;
                        }
                        Console.WriteLine(word);
                    }
                }
            }
            Console.WriteLine("the answer is: " + count);
        }
        public static void pain()
        {
            int count = 0;
            for (int y = 0; y < input.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    if (input[x, y] != 'X')
                    {
                        continue;
                    }
                    string wordRight = "";
                    string wordDownRight = "";
                    string wordDown = "";
                    string wordDownLeft = "";
                    string wordLeft = "";
                    string wordTopLeft = "";
                    string wordTop = "";
                    string wordTopRight = "";

                    List<string> wordMatrix = new List<string>(new string[10]);

                    for (int offset = 0; offset <= 3; offset++)
                    {
                        //if (offset + x > input.GetUpperBound(0) || offset + y > input.GetUpperBound(1) )
                        //{
                        //    continue;
                        //}
                        if (x + offset <= input.GetUpperBound(0))
                        {
                            wordMatrix[0] += input[x + offset, y]; // wordRight
                        }

                        if (y + offset <= input.GetUpperBound(1) && x + offset < input.GetUpperBound(0))
                        {
                            wordMatrix[1] += input[x + offset, y + offset]; // wordDownRight
                        }

                        if (y + offset <= input.GetUpperBound(1))
                        {
                            wordMatrix[2] += input[x, y + offset]; // wordDown
                        }

                        if (y + offset <= input.GetUpperBound(1) && x + offset * -1 >= 0)
                        {
                            wordMatrix[3] += input[x + offset * -1, y + offset]; // wordDownLeft
                        }

                        if (x + offset * -1 >= 0)
                        {
                            wordMatrix[4] += input[x + offset * -1, y]; // wordLeft
                        }

                        if (x + offset * -1 >= 0 && y + offset * -1 >= 0)
                        {
                            wordMatrix[5] += input[x + offset * -1, y + offset * -1]; // wordTopLeft

                        }

                        if (y + offset * -1 >= 0)
                        {
                            wordMatrix[6] += input[x, y + offset * -1]; // wordTop
                        }
                        if (x + offset <= input.GetUpperBound(0) && y + offset * -1 >= 0)
                        {
                            wordMatrix[7] += input[x + offset, y + offset * -1]; // wordTopRight

                        }





                    }

                    foreach (string word in wordMatrix)
                    {
                        if (word == "XMAS")
                        {
                            count++;
                        }
                        Console.WriteLine(word);
                    }
                }
            }
            Console.WriteLine("the answer is: " + count);
        }
        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day4\\Input.txt");

            if (sr != null)
            {
                input = new char[sr[0].Length, sr.Length];

                int x = 0;

                for (int i = 0; i < sr.Length; i++)
                {
                    foreach (char s in sr[i])
                    {
                        input[x, i] = s;
                        x++;
                    }
                    x = 0;
                }
            }
        }
    }
}
