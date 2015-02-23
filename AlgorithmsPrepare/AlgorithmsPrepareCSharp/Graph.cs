using System;
using System.Collections.Generic;

namespace AlgorithmsPrepareCSharp
{
    internal class EdgeNode
    {
        public char Id { get; private set; }
        public int Weight { get; private set; }

        public EdgeNode Next
        {
            get;
            set;
        }

        public EdgeNode (char id, int weight = 1, EdgeNode next = null)
        {
            this.Id = id;
            this.Weight = weight;
            this.Next = next;
        }
    }

    class Graph
    {
        public Dictionary<char, EdgeNode> NodeToAdjList { get; private set; }
        public int[] OutDegree { get; private set; }
        public int NumOfEdges { get; set; }
        public bool IsDirected{ get; private set; }

        public Graph(bool isDirected = false, int numOfEdges = 0)
        {
            this.NodeToAdjList = new Dictionary<char, EdgeNode>();
            this.OutDegree = new int[26];
            this.IsDirected = isDirected;
            this.NumOfEdges = numOfEdges;
        }

        /// <summary>
        /// Insert a new edge in graph
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="weight"></param>
        /// <param name="increaseEdges"></param>
        public void InsertEdges(char from, char to, int weight = 1, bool increaseEdges = false)
        {
            if (!this.NodeToAdjList.ContainsKey(from))
            {
                this.NodeToAdjList.Add(from, new EdgeNode(to));
            }
            EdgeNode newEdge = new EdgeNode(to, weight, this.NodeToAdjList[from]);
            this.NodeToAdjList[from] = newEdge;
            this.OutDegree[from - 'a']++;
            if (!increaseEdges)
            {
                InsertEdges(to, from, weight, true);
            }
            else
            {
                this.NumOfEdges++;
            }
        }

        /// <summary>
        /// Delete an edge from graph, if indicated edge doesn't exist, just return
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void DeleteEdge(char from, char to)
        {
            if (!this.NodeToAdjList.ContainsKey(from))
            {
                return;
            }

            EdgeNode prevEdge = null;
            EdgeNode firstEdge = this.NodeToAdjList[from];
            while (firstEdge.Id != to)
            {
                prevEdge = firstEdge;
                firstEdge = firstEdge.Next;
            }

            if (prevEdge != null)
            {
                prevEdge.Next = firstEdge.Next;
            }
            else
            {
                this.NodeToAdjList[from] = firstEdge.Next;
            }
        }

        /// <summary>
        /// Perform depth-first search
        /// </summary>
        public void DepthFirstSearch()
        {
            

        }

        /// <summary>
        /// Perform breadth-first search
        /// </summary>
        public void BreadthFirstSearch()
        {
            char[] parent = new char[26];
            parent[0] = '$';
            bool[] visited = new bool[26];
            visited[0] = true;

            Queue<char> q = new Queue<char>();
            q.Enqueue('a');

            while (q.Count != 0)
            {
                char topNode = q.Dequeue();
                EdgeNode iter = this.NodeToAdjList[topNode];
                while (iter != null)
                {
                    if (visited[iter.Id - 'a'])
                    {
                        iter = iter.Next;
                        continue;
                    }
                    q.Enqueue(iter.Id);
                    parent[iter.Id - 'a'] = topNode;
                    visited[iter.Id - 'a'] = true;
                    iter = iter.Next;
                }
            }

            foreach (var keyValPair in this.NodeToAdjList)
            {
                if (keyValPair.Key == 'a')
                {
                    continue;
                }
                FindPath('a', keyValPair.Key, parent);
                Console.Write('\n');
            }
        }

        private void FindPath(char start, char end, char[] parent)
        {
            if (start == end)
            {
                Console.Write(start);
            }
            else
            {
                FindPath(start, parent[end - 'a'], parent);
                Console.Write(' ');
                Console.Write(end);
            }
        }
    }
}
