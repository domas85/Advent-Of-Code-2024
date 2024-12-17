using DSA;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AOC
{

    public class Link
    {
        private INode m_source;
        private INode m_target;

        public INode Source => m_source;
        public INode Target => m_target;

        public Link(INode source, INode target)
        {
            m_source = source;
            m_target = target;
        }
    }
    public interface INode
    {
        public IEnumerable<Link> Links { get; }
    }

    public interface IGraph
    {
        public IEnumerable<INode> Nodes { get; }
    }

    class Day10 : IGraph
    {
        public class Node : INode
        {
            public int m_iValue;
            public Vector2 m_vPosition;
            public HashSet<Link> m_links = new HashSet<Link>();
            public bool isVisited = false;

            public IEnumerable<Link> Links => m_links;
        }

        public static int[,] map;

        private static List<Node> m_nodes;

        public IEnumerable<INode> Nodes => m_nodes;

        public static HashSet<Node> traversedNodes = new HashSet<Node>();
        public static List<Node> allTraversedNodes = new List<Node>();
        public static int answer = 0;

        public static void Run()
        {
            Console.Write("Starting... \n\n");
            ReadInput();
            CreateGraphMap();

            //SolvePart1();

            SolvePart2();

        }

        public static void SolvePart2()
        {
            for (int y = 0; y < m_nodes.Count; y++)
            {
                if (m_nodes[y].m_iValue == 0)
                {
                    printAllPaths(m_nodes[y], 9);
                }
            }
            Console.WriteLine("the answer is: " + answer);
        }


        public static void SolvePart1()
        {
            for (int y = 0; y < m_nodes.Count; y++)
            {
                if (m_nodes[y].m_iValue == 0)
                {
                    HashSet<Node> debug = FloodFill(m_nodes[y]);

                    foreach (Node node in debug)
                    {
                        Console.WriteLine("node value: " + node.m_iValue + " \nat Position: " + node.m_vPosition + "\n");
                    }
                    foreach (Node node in debug)
                    {
                        if (node.m_iValue == 9)
                        {
                            answer++;
                        }
                    }
                }
            }
            Console.WriteLine("the answer is: " + answer);
        }

        public static void printAllPaths(Node s, int d)
        {
            bool[] isVisited = new bool[m_nodes.Count];
            HashSet<Node> pathList = new HashSet<Node>();

            // add source to path[]
            pathList.Add(s);

            // Call recursive utility
            printAllPathsUtil(s, d, isVisited, pathList);
        }

        // A recursive function to print
        // all paths from 'u' to 'd'.
        // isVisited[] keeps track of
        // vertices in current path.
        // localPathList<> stores actual
        // vertices in the current path
        private static void printAllPathsUtil(Node u, int d,
                                       bool[] isVisited,
                                       HashSet<Node> localPathList)
        {

            if (u.m_iValue.Equals(d))
            {
                //Console.Write("path: " );
                //foreach (Node n in localPathList)
                //{
                //    Console.Write(" " + n.m_iValue);
                //}
                //Console.WriteLine("");

                answer++;
                // if match found then no need
                // to traverse more till depth
                return;
            }

            // Mark the current node
            u.isVisited = true;

            // Recur for all the vertices
            // adjacent to current vertex
      

            foreach (Link L in u.m_links)
            {
                Node neighbor = L.Target as Node;

                if (!neighbor.isVisited)
                {
                    // store current node
                    // in path[]
                    localPathList.Add(neighbor);
                    printAllPathsUtil(neighbor, d, isVisited,
                                      localPathList);

                    // remove current node
                    // in path[]
                    localPathList.Remove(neighbor);
                }
            }

            // Mark the current node
            u.isVisited = false;
        }


        public static HashSet<Node> FloodFill(Node start)
        {
            // setup
            Queue<Node> open = new Queue<Node>();
            HashSet<Node> closed = new HashSet<Node>();
            open.Enqueue(start);


            // interate through all connected nodes
            while (open.Count > 0)
            {
                Node current = open.Dequeue();
                closed.Add(current);

                foreach (Link link in current.Links)
                {
                    Node neighbor = link.Target as Node;

                    if (!open.Contains(link.Target) && !closed.Contains(neighbor))
                    {
                        open.Enqueue(neighbor);
                    }
                }
            }
            return closed;
        }

        public static void CreateGraphMap()
        {
            m_nodes = new List<Node>();
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    m_nodes.Add(new Node { m_iValue = map[x, y], m_vPosition = new Vector2(x, y) });
                }
            }

            //Create links

            for (int i = 0; i < m_nodes.Count; ++i)
            {
                Node node = m_nodes[i];

                // find all nodes within range
                kDTree nodeTree = kDTree.Create(m_nodes.ConvertAll(n => n.m_vPosition).ToArray());
                List<kDTree.kDNode> kDNodesInRange = new List<kDTree.kDNode>();
                nodeTree.FindNodesInRange(node.m_vPosition, 1.4f, kDNodesInRange);

                // create links
                foreach (kDTree.kDNode kDNode in kDNodesInRange)
                {
                    if (kDNode.m_iIndex == i)
                    {
                        continue;
                    }

                    Node other = m_nodes[kDNode.m_iIndex];
                    var stepSize = other.m_iValue - node.m_iValue;

                    if (node.m_iValue < other.m_iValue && stepSize == 1)
                    {
                        node.m_links.Add(new Link(node, other));
                    }
                }
            }
        }

        public static void ReadInput()
        {
            var sr = File.ReadAllLines("C:\\files\\Advent-Of-Code-2024\\AdventOfCode2024\\AdventOfCode2024\\Day10\\Input.txt");

            if (sr != null)
            {
                map = new int[sr[0].Length, sr.Length];

                int x = 0;

                for (int y = 0; y < sr.Length; y++)
                {
                    foreach (char s in sr[y])
                    {
                        map[x, y] = int.Parse(s.ToString());
                        x++;
                    }
                    x = 0;
                }
            }
        }

    }
}