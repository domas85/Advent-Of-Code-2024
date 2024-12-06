using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC
{
    public struct Rules
    {
        public int thus_number;
        public List<int> before;
        public List<int> after;
    }


    public class Day5
    {
        public static Dictionary<int, Rules> rules = new Dictionary<int, Rules>();

        public static List<int> left = new List<int>();
        public static List<int> right = new List<int>();
        public static List<List<int>> updates = new List<List<int>>();
        public static List<List<int>> unsortedUpdates = new List<List<int>>();
        public static bool rulesBroken;
        public static int answer;
        public static void Run()
        {
            Console.Write("Starting... \n ");
            ReadInput();

            SolvePart1(updates);

            SolvePart2(unsortedUpdates);

        }

        public static void SolvePart2(List<List<int>> theUpdates)
        {
            foreach (List<int> upd in theUpdates)
            {
                for (int i = 0; i < upd.Count; i++)
                {
                    SortUpdate(upd[i], upd, i);
                    if (rules.ContainsKey(upd[i]))
                    {
                        CheckRules(upd[i], upd, i);
                        if (rulesBroken) break;
                    }
                }
                if (rulesBroken)
                {
                    SolvePart2(theUpdates);
                }
            }

            answer = 0;
            SolvePart1(unsortedUpdates);
        }

        public static void SortUpdate(int numberToCheck, List<int> inUpdate, int currentIndex)
        {
            var rulesForNumber = rules.TryGetValue(numberToCheck, out var result);

            //Check if this number does not break after rules 
            for (int x = 0; x < result.after.Count; x++)
            {
                if (inUpdate.Contains(result.after[x]))
                {
                    int ruleIndex = inUpdate.IndexOf(result.after[x]);
                    if (ruleIndex < currentIndex)
                    {
                        rulesBroken = false;
                    }
                    else
                    {
                        rulesBroken = true;
                        int temp = inUpdate[currentIndex];
                        inUpdate[currentIndex] = inUpdate[ruleIndex];
                        inUpdate[ruleIndex] = temp;
                    }
                }
            }

            //Check if this number does not break before rules 
            for (int x = 0; x < result.before.Count; x++)
            {
                if (inUpdate.Contains(result.before[x]))
                {
                    int ruleIndex = inUpdate.IndexOf(result.before[x]);
                    if (ruleIndex > currentIndex)
                    {
                        rulesBroken = false;
                    }
                    else
                    {
                        rulesBroken = true;

                        int temp = inUpdate[currentIndex];
                        inUpdate[currentIndex] = inUpdate[ruleIndex];
                        inUpdate[ruleIndex] = temp;
                    }
                }
            }
            while (rulesBroken)
            {
                for (int i = 0; i < inUpdate.Count; i++)
                {
                    SortUpdate(inUpdate[i], inUpdate, i);
                    if (rules.ContainsKey(inUpdate[i]))
                    {
                        CheckRules(inUpdate[i], inUpdate, i);
                        if (rulesBroken) break;
                    }
                }
            }
        }

        public static void SolvePart1(List<List<int>> theUpdates)
        {
            foreach (List<int> upd in theUpdates)
            {
                for (int i = 0; i < upd.Count; i++)
                {
                    if (rules.ContainsKey(upd[i]))
                    {
                        CheckRules(upd[i], upd, i);
                        if (rulesBroken) break;
                    }
                    else
                    {
                        AddRulesForNumber(upd[i]);
                        CheckRules(upd[i], upd, i);
                    }
                }
                if (rulesBroken)
                {
                    Console.WriteLine("update: " + string.Join(" ", upd) + " : have a broken rule");
                    unsortedUpdates.Add(upd);
                }
                else
                {
                    Console.WriteLine("update: " + string.Join(" ", upd) + " : have no broken rules");
                    int value = upd[upd.Count / 2];

                    answer += value;
                }
            }
            Console.WriteLine("\n After adding all valid updates middle values the answer is: " + answer + "\n");
        }

        public static void CheckRules(int numberToCheck, List<int> inUpdate, int currentIndex)
        {
            var rulesForNumber = rules.TryGetValue(numberToCheck, out var result);

            //Check if this number does not break after rules 
            for (int x = 0; x < result.after.Count; x++)
            {
                if (inUpdate.Contains(result.after[x]))
                {
                    int ruleIndex = inUpdate.IndexOf(result.after[x]);
                    if (ruleIndex < currentIndex)
                    {
                        rulesBroken = false;
                    }
                    else
                    {
                        rulesBroken = true;
                        break;
                    }
                }
            }

            //Check if this number does not break before rules 
            for (int x = 0; x < result.before.Count; x++)
            {
                if (inUpdate.Contains(result.before[x]))
                {
                    int ruleIndex = inUpdate.IndexOf(result.before[x]);
                    if (ruleIndex > currentIndex)
                    {
                        rulesBroken = false;
                    }
                    else
                    {
                        rulesBroken = true;
                        break;
                    }
                }
            }
        }

        public static void AddRulesForNumber(int numberToSearch)
        {
            if (!rules.ContainsKey(numberToSearch))
            {
                var newRule = new Rules();
                newRule.thus_number = numberToSearch;
                newRule.before = new List<int>();
                newRule.after = new List<int>();

                rules.Add(newRule.thus_number, newRule);
                for (int i = 0; i < left.Count; i++)
                {
                    if (left[i] == numberToSearch)
                    {
                        newRule.before.Add(right[i]);
                        AddRulesForNumber(right[i]);
                    }
                    if (right[i] == numberToSearch)
                    {
                        newRule.after.Add(left[i]);
                        AddRulesForNumber(left[i]);
                    }
                }
            }
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day5\\Input.txt");

            int indexWhereItBroke = 0;

            //Get the rules into their own lists
            if (sr != null)
            {
                for (int i = 0; i < sr.Length; i++)
                {
                    if (sr[i] != null)
                    {
                        var strArray = sr[i].Split("|", StringSplitOptions.None);
                        indexWhereItBroke = i;
                        if (sr[i].Length <= 1) break;
                        int[] intArray = System.Array.ConvertAll(strArray, num => int.Parse(num));

                        left.Add(intArray[0]);
                        right.Add(intArray[1]);
                    }
                }
            }

            //Get the updates to a list
            if (sr != null)
            {
                for (int i = indexWhereItBroke + 1; i < sr.Length; i++)
                {
                    if (sr[i] != null)
                    {
                        var strArray = sr[i].Split(",", StringSplitOptions.None);
                        if (sr[i].Length <= 1) break;
                        int[] intArray = System.Array.ConvertAll(strArray, num => int.Parse(num));

                        updates.Add(intArray.ToList());
                    }
                }
            }

            for (int i = 0; i < left.Count; i++)
            {
                Console.Write(left[i] + " ");
            }
            Console.WriteLine("\n");

            for (int i = 0; i < left.Count; i++)
            {
                Console.Write(right[i] + " ");
            }
            Console.WriteLine("\n");

            for (int i = 0; i < updates.Count; i++)
            {
                Console.WriteLine(string.Join(" ", updates[i]));
            }
            Console.WriteLine("\n");
        }
    }
}
