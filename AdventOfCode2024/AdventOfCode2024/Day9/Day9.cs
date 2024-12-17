using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AOC
{
    public struct FileBlock
    {
        public int ID;
        public int Size;
        public int FreeSpace;
        public string VizualSize;
        public string VizualAdditionSize;
        public string VizualSpace;
        public string FullVizual { get { return VizualSize + VizualAdditionSize + VizualSpace; } }
    }

    public struct FileData
    {
        public int ID;
        public string Data;
    }

    class Day9
    {
        public static List<int> blocks = new List<int>();
        public static List<int> freeSpace = new List<int>();
        public static int freeSpaceCount = 0;
        public static Int128 answer = 0;
        public static List<string> disk = new List<string>();
        public static List<FileData> diskBlock = new List<FileData>();


        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();

            //SolvePart1();

            SolvePart2();


            //var input = File.ReadAllText("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day9\\Input.txt").Trim();
            //var part2 = Part2(input);
            //Console.WriteLine($"Part2: {part2}");
        }

        public static void SolvePart2()
        {
            GetWholeFileBlock();
            ProcessFullBlock();

            var fullDisk = "";
            //foreach (FileData data in diskBlock)
            //{
            //    fullDisk += data.Data;
            //}
            long count = 0;
            for (int i = 0; i < diskBlock.Count; i++)
            {
                //if (fullDisk[i] != '.')
                //{
                //    Int128 mul = Int128.Parse(fullDisk[i].ToString()) * i;
                //    answer += mul;
                //}


                if (diskBlock[i].Data.Contains('.'))
                {
                    var tempRegex = Regex.Matches(diskBlock[i].Data, @"(\.)");

                    count += tempRegex.Count;
                }

                if (!diskBlock[i].Data.Contains('.'))
                {
                    var tempRegex = Regex.Matches(diskBlock[i].Data, @"(" + diskBlock[i].ID + @")");
                    for (int x = 0; x < tempRegex.Count; x++)
                    {
                        Int128 mul = diskBlock[i].ID * count;
                        answer += mul;
                        count++;
                    }
                }

            }
            //Console.WriteLine(fullDisk);
            foreach (FileData data in diskBlock)
            {
                Console.Write(data.Data);
            }
            Console.WriteLine();
            Console.WriteLine("the total sum is: " + answer);
        }

        public static void ProcessFullBlock()
        {

            for (int i = diskBlock.Count - 1; i >= 0; i--)
            {
                for (int x = 0; x < i - 1; x++)
                {
                    #region Attempt 3
                    var tempRegex = Regex.Matches(diskBlock[i].Data, @"(" + diskBlock[i].ID + @")");

                    if (diskBlock[x].Data.Length >= tempRegex.Count && diskBlock[x].Data.Contains('.') && !diskBlock[i].Data.Contains('.'))
                    {
                        var tempSpace = diskBlock[x];
                        if (diskBlock[x].Data.Length == tempRegex.Count)
                        {
                            diskBlock[x] = diskBlock[i];
                            diskBlock[i] = tempSpace;
                            break;
                        }
                        else
                        {

                            int freeSpaceLeft = diskBlock[x].Data.Length - tempRegex.Count;
                            int spaceMoved = diskBlock[x].Data.Length - freeSpaceLeft;
                            var tempStringData = diskBlock[x].Data;
                            tempStringData = tempStringData.Truncate(spaceMoved);

                            FileData newSpaceEntry = new FileData();
                            newSpaceEntry.Data = tempStringData;
                            newSpaceEntry.ID = 0;


                            diskBlock[x] = diskBlock[i];
                            diskBlock[i] = newSpaceEntry;


                            FileData newEntry = new FileData();
                            newEntry.ID = 0;
                            string newString = "";

                            for (int y = 0; y < freeSpaceLeft; y++)
                            {
                                newString += '.';
                            }

                            if (diskBlock[x + 1].Data.Contains('.'))
                            {
                                var stringData = diskBlock[x + 1].Data;
                                newString += stringData;
                                newEntry.Data = newString;
                                diskBlock[x + 1] = newEntry;

                            }
                            else
                            {
                                newEntry.Data = newString;
                                diskBlock.Insert(x + 1, newEntry);
                                i++;
                            }
                        }
                    }
                    #endregion

                    #region attempt 2

                    //        if (diskBlock[x].FreeSpace >= diskBlock[i].Size)
                    //        {
                    //            var tempFileFront = diskBlock[x];
                    //            var tempFileEnd = diskBlock[i];


                    //            // Process file at the front
                    //            tempFileFront.FreeSpace -= diskBlock[i].Size;
                    //            string tempSpaceString = "";
                    //            for (int s = 0; s < tempFileFront.FreeSpace; s++)
                    //            {
                    //                tempSpaceString += '.';
                    //            }
                    //            tempFileFront.VizualSpace = tempSpaceString;
                    //            //tempFileFront.Size += diskBlock[i].Size;

                    //            string tempFileString = "";
                    //            for (int f = 0; f < diskBlock[i].Size; f++)
                    //            {
                    //                tempFileString += diskBlock[i].ID;
                    //            }
                    //            tempFileFront.VizualAdditionSize += tempFileString;

                    //            // Process file at the end
                    //            tempFileEnd.FreeSpace += diskBlock[i].Size;
                    //            string tempEndSpaceString = "";
                    //            for (int s = 0; s < tempFileEnd.FreeSpace; s++)
                    //            {
                    //                tempEndSpaceString += '.';
                    //            }
                    //            tempFileEnd.VizualSpace = tempEndSpaceString;
                    //            tempFileEnd.Size = 0;
                    //            tempFileEnd.VizualSize = "";

                    //            diskBlock[x] = tempFileFront;
                    //            diskBlock[i] = tempFileEnd;


                    //            break;
                    //        }
                    //    }
                    //    PrintDisk();
                    //    //Console.WriteLine(string.Join("", diskBlock));
                    //}
                    #endregion

                    #region Attempt 1
                    //for (int i = disk.Count - 1; i >= 0; i--)
                    //{
                    //    for (int x = 0; x < i - 1; x++)
                    //    {
                    //        // swap blocks

                    //        if (disk[x].Contains('.') && disk[x].Length >= disk[i].Length && !disk[i].Contains('.'))
                    //        {
                    //        int Lenghtcheck = int.Parse(disk[i].ToString());
                    //            var tempSplitLeft = disk[x].Substring(disk[i].Length, disk[x].Length - disk[i].Length);
                    //            var tempSplitEnd = disk[x].Substring(0, disk[i].Length);
                    //            disk[x] = disk[i];
                    //            disk.RemoveAt(i);
                    //            bool removedOnce = false;
                    //            int index = i;
                    //            if (disk[i - 1].Contains('.'))
                    //            {
                    //                tempSplitEnd += disk[i - 1];
                    //                disk.RemoveAt(i - 1);
                    //                index--;
                    //                removedOnce = true;
                    //            }

                    //            if (removedOnce)
                    //            {
                    //                if (i - 1 < disk.Count && disk[i - 1].Contains('.'))
                    //                {
                    //                    tempSplitEnd += disk[i - 1];
                    //                    disk.RemoveAt(i - 1);
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (i < disk.Count && disk[i].Contains('.'))
                    //                {
                    //                    tempSplitEnd += disk[i];
                    //                    disk.RemoveAt(i);
                    //                    index--;
                    //                }
                    //            }

                    //            if (i == disk.Count)
                    //            {
                    //                disk.Add(tempSplitEnd);
                    //            }
                    //            else
                    //            {
                    //                disk.Insert(index, tempSplitEnd);
                    //            }

                    //            if (tempSplitLeft != "")
                    //            {
                    //                disk.Insert(x + 1, tempSplitLeft);
                    //            }

                    //            break;
                    //        }
                    //    }
                    //Console.WriteLine(string.Join("", disk));
                    //}
                    #endregion

                }
                //foreach (FileData data in diskBlock)
                //{
                //    Console.Write(data.Data);
                //}
                //Console.WriteLine();
            }
        }

        public static void GetWholeFileBlock()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                string tempString = "";
                FileBlock newFile = new FileBlock();
                newFile.ID = i;
                newFile.Size = blocks[i];


                FileData newData = new FileData();
                newData.ID = i;
                FileData newEmpty = new FileData();
                newEmpty.ID = i;

                for (int x = 0; x < blocks[i]; x++)
                {
                    tempString += i.ToString();
                    //disk.Add(i.ToString());
                }
                if (tempString != "")
                {
                    disk.Add(tempString);
                    newFile.VizualSize = tempString;
                    newData.Data = tempString;
                }

                if (i < freeSpace.Count)
                {

                    string tempSpaceString = "";
                    newFile.FreeSpace = freeSpace[i];

                    for (int x = 0; x < freeSpace[i]; x++)
                    {
                        tempSpaceString += ".";
                        //disk.Add(".");
                        freeSpaceCount++;
                    }
                    newFile.VizualSpace = tempSpaceString;
                    newEmpty.Data = tempSpaceString;
                    if (tempSpaceString != "")
                    {
                        disk.Add(tempSpaceString);
                    }
                }
                else
                {
                    newFile.VizualSpace = "";
                    newEmpty.Data = "";
                }
                diskBlock.Add(newData);
                if (newEmpty.Data != "")
                {
                    diskBlock.Add(newEmpty);
                }
                //diskBlock.Add(newFile);
            }
            Console.WriteLine();
            Console.WriteLine(string.Join("", disk));
        }

        public static void PrintDisk()
        {
            for (int i = 0; i < diskBlock.Count; i++)
            {
                //Console.Write(diskBlock[i].FullVizual);
            }
            Console.WriteLine();
        }

        public static void SolvePart1()
        {
            GetIndividualFileBlock();
            ProcessIndividualFileBlock();

            for (int i = 0; i < disk.Count; i++)
            {
                if (disk[i] != ".")
                {
                    long mul = long.Parse(disk[i]) * i;
                    answer += mul;
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("the total sum is: " + answer);
        }

        public static void ProcessIndividualFileBlock()
        {
            for (int i = disk.Count - 1; i >= 0; i--)
            {
                if (freeSpaceCount > 0)
                {
                    for (int x = 0; x < disk.Count - 1; x++)
                    {
                        // swap blocks
                        if (disk[x] == ".")
                        {
                            var temp = disk[x];
                            disk[x] = disk[i];
                            disk[i] = temp;
                            freeSpaceCount--;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine(string.Join("", disk));
        }

        public static void GetIndividualFileBlock()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                for (int x = 0; x < blocks[i]; x++)
                {
                    disk.Add(i.ToString());
                }
                if (i < freeSpace.Count)
                {
                    for (int x = 0; x < freeSpace[i]; x++)
                    {
                        disk.Add(".");
                        freeSpaceCount++;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine(string.Join("", disk));
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllText("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day9\\Input.txt");

            Console.WriteLine("input: " + sr);

            if (sr != null)
            {
                for (int y = 0; y < sr.Length; y += 2)
                {
                    blocks.Add(int.Parse(sr[y].ToString()));
                    if (y + 1 < sr.Length)
                    {
                        freeSpace.Add(int.Parse(sr[y + 1].ToString()));
                    }
                }
            }
        }

        private static long Part2(string input)
        {
            var freeBlocks = new List<FreeSpace>();
            var files = new List<FileInfo>();
            var startBlock = 0;
            var fileID = 0;
            for (var i = 0; i < input.Length; i += 2)
            {
                var fileSize = input[i] - '0';
                var fileInfo = new FileInfo(startBlock, fileID++, fileSize);
                files.Add(fileInfo);
                startBlock += fileSize;

                if (i < input.Length - 1)
                {
                    var length = input[i + 1] - '0';
                    var freeSpace = new FreeSpace(startBlock, length);
                    freeBlocks.Add(freeSpace);
                    startBlock += length;
                }
            }

            for (var i = files.Count - 1; i >= 0; i--)
            {
                var fileInfo = files[i];
                var freeSpaceIndex = FindFreeSpace(freeBlocks, fileInfo);
                if (freeSpaceIndex == -1) continue;
                var freeSpace = freeBlocks[freeSpaceIndex];
                fileInfo.StartBlock = freeSpace.StartBlock;
                freeSpace.Allocate(fileInfo.FileSize);
                if (freeSpace.Length is 0) freeBlocks.RemoveAt(freeSpaceIndex);
            }

            return files.Sum(fileInfo => fileInfo.CheckSum);
        }
        private static int FindFreeSpace(List<FreeSpace> freeBlocks, FileInfo fileInfo)
        {
            for (var i = 0; i < freeBlocks.Count; i++)
            {
                var freeSpace = freeBlocks[i];
                if (freeSpace.StartBlock >= fileInfo.StartBlock) break;
                if (freeSpace.Length >= fileInfo.FileSize) return i;
            }
            return -1;
        }
    }
}
public static class StringExt
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }
}
internal class FileInfo(int StartBlock, int FileID, int FileSize)
{
    public int StartBlock { get; set; } = StartBlock;
    public int FileID { get; } = FileID;
    public int FileSize { get; } = FileSize;

    public long CheckSum
    {
        get
        {
            var checksum = 0L;
            for (var i = 0; i < FileSize; i++)
            {
                checksum += FileID * (StartBlock + i);
            }
            return checksum;
        }
    }
}
internal class FreeSpace(int StartBlock, int Length)
{
    public int StartBlock { get; private set; } = StartBlock;
    public int Length { get; private set; } = Length;

    public void Allocate(int fileSize)
    {
        StartBlock += fileSize;
        Length -= fileSize;
    }
}