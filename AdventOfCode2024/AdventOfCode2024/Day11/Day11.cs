using DSA;
using System.Linq;



namespace AOC
{


    class Day11
    {

        public static List<string> stones = new List<string>();
        public static Dictionary<string, Int128> resultingStones = new Dictionary<string, Int128>();
        public static Dictionary<string, Int128> tempDic = new Dictionary<string, Int128>();

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();

            //SolvePart1();

            SolvePart2();

            Console.ReadLine();

        }

        public static void SolvePart2()
        {
            Console.WriteLine("initial stones: \n" + string.Join(" ", stones) + "\n");
            foreach (string ston in stones)
            {
                resultingStones.Add(ston, 1);
            }

            foreach (KeyValuePair<string, Int128> entry in resultingStones)
            {
                tempDic[entry.Key] = entry.Value;
            }

            Console.WriteLine(string.Join(" ", resultingStones.ToArray()));

            for (int i = 0; i < 100; i++)
            {
                var bro = CalculateStoneChange(resultingStones, tempDic);
                foreach (KeyValuePair<string, Int128> entry in bro)
                {
                    resultingStones[entry.Key] = entry.Value;
                }
                foreach (KeyValuePair<string, Int128> entry in bro)
                {
                    tempDic[entry.Key] = entry.Value;
                }
            }
            Console.WriteLine(string.Join(" ", stones));

            Int128 answer = 0;

            foreach (KeyValuePair<string, Int128> entry in resultingStones)
            {
                Console.WriteLine("of stone: " + entry.Key + " there are: " + entry.Value);
                answer += entry.Value;
            }

            Console.WriteLine("\n" + "The stone count is: " + answer);
        }

        public static Dictionary<string, Int128> CalculateStoneChange(Dictionary<string, Int128> checkAllStonesDictionary, Dictionary<string, Int128> modifyAllStonesDictionary)
        {


            foreach (KeyValuePair<string, Int128> entry in checkAllStonesDictionary)
            {
                if (entry.Value <= 0) continue;

                if (entry.Key == "0")
                {
                    // rule 1
                    if (checkAllStonesDictionary["0"] > 0)
                    {
                        modifyAllStonesDictionary["1"] += checkAllStonesDictionary["0"];
                        modifyAllStonesDictionary["0"] = 0;

                        continue;
                    }
                }

                // rule 2
                var stoneLenght = entry.Key.Length;

                if (stoneLenght % 2 == 0)
                {
                    string firsthalf = entry.Key.Substring(0, stoneLenght / 2);
                    var skipFirstHalf = entry.Key.Skip(stoneLenght / 2).ToList();

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
                    if (modifyAllStonesDictionary.ContainsKey(firsthalf))
                    {
                        modifyAllStonesDictionary[firsthalf] += entry.Value;
                    }
                    else
                    {
                        modifyAllStonesDictionary[firsthalf] = entry.Value;
                    }

                    if (modifyAllStonesDictionary.ContainsKey(Secondhalf))
                    {
                        modifyAllStonesDictionary[Secondhalf] += entry.Value;
                    }
                    else
                    {
                        modifyAllStonesDictionary[Secondhalf] = entry.Value;
                    }

                    modifyAllStonesDictionary[entry.Key] -= entry.Value;

                    continue;
                }


                //rule 3
                var newNum = long.Parse(entry.Key) * 2024;
                if (modifyAllStonesDictionary.ContainsKey(newNum.ToString()))
                {
                    modifyAllStonesDictionary[newNum.ToString()] += entry.Value;
                }
                else
                {
                    modifyAllStonesDictionary[newNum.ToString()] = entry.Value;
                }
                modifyAllStonesDictionary[entry.Key] -= entry.Value;
            }

            return modifyAllStonesDictionary;
        }

        //public static Func<string, int> Memoize(Func<string, int > fn)
        //{
        //    // We create the cache which we'll use to store the inputs and calculated results.
        //    var memoCache = new Dictionary<string, int>();

        //    return n =>
        //    {
        //        // We can check if we've already performed a calculation using the given input.
        //        // If we have, we can simply return that result.
        //        if (memoCache.ContainsKey(n))
        //        {
        //            return memoCache[n];
        //        }

        //        // If we don't find the current input in our cache, we'll need to perform the calculation.
        //        // We also need to make sure we store that input and result for future use.
        //        var result = fn(n);
        //        memoCache[n] = result;

        //        return result;
        //    };
        //}

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