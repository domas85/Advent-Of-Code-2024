using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace DSA
{
    public class kDTree : BinaryTree<Vector2>
    {
        public class kDNode : Node 
        {
            public int  m_iIndex;
        }

        #region Nearest Neighbor Search

        public kDNode FindNearestNeighbor(Vector2 v, List<kDNode> visitedNodes)
        {
            return FindNearestNeighbor(m_root as kDNode, v, 0, visitedNodes);
        }

        protected kDNode FindNearestNeighbor(kDNode node, Vector2 v, int iDimension, List<kDNode> visitedNodes) 
        {
            if (node == null)
            {
                return null;
            }
               
            // TODO: this list is only for visualization
            visitedNodes.Add(node);

            // decide what branch to search next
            bool bSearchLeft = v[iDimension] <= node.m_value[iDimension];
            kDNode nextBranch = (bSearchLeft ? node.m_left : node.m_right) as kDNode;
            kDNode otherBranch = (bSearchLeft ? node.m_right : node.m_left) as kDNode;
            int iNextDimension= (iDimension + 1) % 2;

            kDNode temp = FindNearestNeighbor(nextBranch, v, iNextDimension, visitedNodes);
            kDNode best = GetClosest(v, temp, node);

            float fDistanceToBest = Vector2.Distance(best.m_value, v);
            float fDistanceToPlane = Math.Abs(v[iDimension] - node.m_value[iDimension]);

            if (fDistanceToBest >= fDistanceToPlane)
            {
                temp = FindNearestNeighbor(otherBranch, v, iNextDimension, visitedNodes);
                best = GetClosest(v, temp, best);
            }

            return best;
        }

        public class CustomVector2
        { 
            public Vector2 Vector2 { get; set; }

            public CustomVector2(float x, float y)
            {
                this.Vector2 = new(x, y);
            }

            public static implicit operator Vector2(CustomVector2 self)
            {
                return self.Vector2;
            }

            public static float SqrMagnitude(Vector2 a)
            {
                
                return a.X * a.X + a.Y * a.Y;
            }

        }

        kDNode GetClosest(Vector2 v, kDNode A, kDNode B)
        {
            float fDistA = A != null ? CustomVector2.SqrMagnitude(v - A.m_value) : float.MaxValue;
            float fDistB = B != null ? CustomVector2.SqrMagnitude(v - B.m_value) : float.MaxValue;
            return fDistA <= fDistB ? A : B;
        }

        #endregion

        #region Range Search

        public void FindNodesInRange(Vector2 v, float fRange, List<kDNode> nodesInRange)
        {
            FindNodesInRange(m_root as kDNode, v, fRange, 0, nodesInRange);
        }

        protected void FindNodesInRange(kDNode node, Vector2 v, float fRange, int iDimension, List<kDNode> nodesInRange)
        {
            if (node == null)
            {
                return;
            }

            // node in range?
            float fDistanceToNode = Vector2.Distance(v, node.m_value);
            if (fDistanceToNode < fRange && fDistanceToNode == 1f)
            {
                nodesInRange.Add(node);
            }

            // decide what branch to search next
            bool bSearchLeft = v[iDimension] <= node.m_value[iDimension];
            kDNode nextBranch = (bSearchLeft ? node.m_left : node.m_right) as kDNode;
            kDNode otherBranch = (bSearchLeft ? node.m_right : node.m_left) as kDNode;
            int iNextDimension = (iDimension + 1) % 2;

            FindNodesInRange(nextBranch, v, fRange, iNextDimension, nodesInRange);

            float fDistanceToPlane = Math.Abs(v[iDimension] - node.m_value[iDimension]);
            if (fRange >= fDistanceToPlane)
            {
                FindNodesInRange(otherBranch, v, fRange, iNextDimension, nodesInRange);
            }
        }

        #endregion

        #region Tree Creation

        public static kDTree Create(Vector2[] points)
        {
            // create indices array
            int[] indices = new int[points.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                indices[i] = i;
            }

            kDTree newTree = new kDTree();
            newTree.m_root = CreateNode(points, indices, 0, 0, points.Length - 1);
            return newTree;
        }

        static Node CreateNode(Vector2[] points, int[] indices, int iDimension, int iStart, int iEnd) 
        {
            if (iStart > iEnd)
            {
                return null;
            }

            // sort points along iDimension 
            Sort(points, indices, iDimension, iStart, iEnd);

            // select median point
            int iMiddle = (iStart + iEnd) / 2;
            Vector2 vMedian = points[iMiddle];

            // create node
            int iNextDimension = (iDimension + 1) % 2;
            kDNode node = new kDNode
            { 
                m_value = vMedian,
                m_iIndex = indices[iMiddle],
                m_left = CreateNode(points, indices, iNextDimension, iStart, iMiddle - 1),
                m_right = CreateNode(points, indices, iNextDimension, iMiddle + 1, iEnd)
            };

            return node;
        }

        static int Partition(Vector2[] points, int[] indices, int iDimension, int iStart, int iEnd)
        {
            // select the pivot value
            Vector2 vPivotValue = points[(iStart + iEnd) / 2];
            int iLeft = iStart;
            int iRight = iEnd;

            while (iLeft <= iRight)
            {
                // move left index until a value greater or equal to the pivot is found
                while (points[iLeft][iDimension] < vPivotValue[iDimension])
                {
                    iLeft++;
                }

                // move right index until a value less or equal to the pivot is found
                while (points[iRight][iDimension] > vPivotValue[iDimension])
                {
                    iRight--;
                }

                // should we swap?
                if (iLeft <= iRight)
                {
                    // ... otherwise swap
                    Vector2 vTemp = points[iLeft];
                    points[iLeft] = points[iRight];
                    points[iRight] = vTemp;

                    // also swap indices
                    int iTemp = indices[iLeft];
                    indices[iLeft] = indices[iRight];
                    indices[iRight] = iTemp;

                    iLeft++;
                    iRight--;
                }
            }

            return iLeft;
        }

        static void Sort(Vector2[] points, int[] indices, int iDimension, int iStart, int iEnd)
        {
            if (iStart < iEnd)
            {
                // Partition the array
                int iPivotIndex = Partition(points, indices, iDimension, iStart, iEnd);

                // Send left side off to QuickSort
                Sort(points, indices, iDimension, iStart, iPivotIndex - 1);

                // Send right side off to QuickSort
                Sort(points, indices, iDimension, iPivotIndex, iEnd);
            }
        }

        #endregion
    }
}