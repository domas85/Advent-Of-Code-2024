using DSA;
using System.Linq;



namespace AOC
{


    class Day11
    {

        public static List<string> stones = new List<string>();

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
            Console.WriteLine("initial stones: \n" + string.Join(" ", stones) + "\n");

            for (int i = 0; i < 75; i++)
            {
                for (int s = 0; s < stones.Count; s++)
                {

                }
            }
            Console.WriteLine(string.Join(" ", stones));

            Console.WriteLine("\n" + "The stone count is: " + stones.Count);
        }

        public static List<string> CalculateStoneChange(string stone, int blinks, List<string> result)
        {
            // rule 1
            if (stone == "0" && blinks > 0)
            {
                stone = "1";
                blinks--;
                CalculateStoneChange(stone, blinks, result);
            }
            var stoneLenght = stone.Length;

            // rule 2
            if (stoneLenght % 2 == 0 && blinks > 0)
            {
                string firsthalf = stone.Substring(0, stoneLenght / 2);
                var skipFirstHalf = stone.Skip(stoneLenght / 2).ToList();


                for (int j = 0; j < skipFirstHalf.Count - 1; j++)
                {
                    if (skipFirstHalf[j] == '0')
                    {
                        skipFirstHalf.RemoveAt(j);
                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                var Secondhalf = string.Join("", skipFirstHalf);
                stone = firsthalf;
                //stones.Insert(s + 1, Secondhalf);
            }

            //rule 3
            //var newNum = long.Parse(stones[s]) * 2024;
            //stones[s] = newNum.ToString();
            return null;
        }

        public static Func<string, int> Memoize(Func<string, int> fn)
        {
            // We create the cache which we'll use to store the inputs and calculated results.
            var memoCache = new Dictionary<string, int>();

            return n =>
            {
                // We can check if we've already performed a calculation using the given input.
                // If we have, we can simply return that result.
                if (memoCache.ContainsKey(n))
                {
                    return memoCache[n];
                }

                // If we don't find the current input in our cache, we'll need to perform the calculation.
                // We also need to make sure we store that input and result for future use.
                var result = fn(n);
                memoCache[n] = result;

                return result;
            };
        }

        // Our recursiveFibonacci function can remain the same.
        public static int RecursiveFibonacci(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            return RecursiveFibonacci(n - 1) + RecursiveFibonacci(n - 2);
        }




        public static void SolvePart1()
        {
            Console.WriteLine("initial stones: \n" + string.Join(" ", stones) + "\n");

            int stonesCount = stones.Count;

            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine("blink: " + i);
                for (int s = 0; s < stonesCount; s++)
                {
                    var stoneLenght = stones[s].Length;

                    // rule 1
                    if (stones[s] == "0")
                    {
                        stones[s] = "1";
                    }
                    else if (stoneLenght % 2 == 0)
                    {
                        // rule 2
                        string firsthalf = stones[s].Substring(0, stoneLenght / 2);
                        var skipFirstHalf = stones[s].Skip(stoneLenght / 2).ToList();


                        for (int j = 0; j < skipFirstHalf.Count - 1; j++)
                        {
                            if (skipFirstHalf[j] == '0')
                            {
                                skipFirstHalf.RemoveAt(j);
                                j--;
                            }
                            else
                            {
                                break;
                            }
                        }
                        var Secondhalf = string.Join("", skipFirstHalf);

                        stones[s] = firsthalf;
                        stones.Insert(s + 1, Secondhalf);
                        s++;
                        stonesCount++;
                    }
                    else
                    {
                        // rule 3
                        var newNum = long.Parse(stones[s]) * 2024;
                        stones[s] = newNum.ToString();
                    }
                }
            }
            Console.WriteLine(string.Join(" ", stones));

            Console.WriteLine("\n" + "The stone count is: " + stones.Count);
        }


        public static void ReadInput()
        {
            var sr = File.ReadAllText("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day11\\Input.txt");

            if (sr != null)
            {
                var split = sr.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < split.Length; i++)
                {
                    stones.Add(split[i]);
                }
            }
        }

    }
}