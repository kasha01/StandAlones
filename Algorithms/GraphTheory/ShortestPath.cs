using Algorithms.HelpersDataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.GraphTheory
{
    class ShortestPath
    {
        public static int[] predecessor;

        #region Dijkstra
        // check MinPriorityQueue Implementation in DataStructure Project
        public static MinPriorityQueue<int> minpriorityqueue;

        public static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        public static void Print(int[] distance, int verticesCount)
        {
            Console.WriteLine("Vertex Distance from source");

            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i, distance[i]);
        }

        public static void Dijkstra(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];
            predecessor = new int[verticesCount];

            // Initialize
            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
                predecessor[i] = -1;
            }

            // Designating a source vertex
            distance[source] = 0;

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                // Set node with shortest path as the current Node 
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;  // mark node with shortest path as visited

                for (int v = 0; v < verticesCount; ++v)
                    // loop thru all unvisited neighbor nodes where path to the neighbor node is shorter than what it (neighbor) currently has
                    // u: current node, v: neighbor node(s)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                        predecessor[v] = u; //the predecessor of neighbor v is the current node u
                    }
            }
            Print(distance, verticesCount);
        }

        public static void DijkstraWithPriorityQueue(int[,] graph, int source, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];
            predecessor = new int[verticesCount];
            minpriorityqueue = new MinPriorityQueue<int>(verticesCount);

            // Initialize
            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
                predecessor[i] = -1;
                minpriorityqueue.add_with_priority(i, int.MaxValue);  //initialize Priority Queue with all nodes have Infinity Priority/Infinity distance
            }

            // Designating a source vertex
            distance[source] = 0;
            minpriorityqueue.decrease_priority(0, 0);     //Set the source as the node with lowest Priority (Top of the Heap)
            /* the whole idea of MinPriorityQueue is it replaces the expensive method of MinimumDistance which returns the node with the minimum/shortest distance
             from source, by loop through ALL nodes O(n), instead Priority Queue can get me the node with the minimum priority (shortest distance) much faster in O(log n)
             Remember I am having MinPriorityQueue (node at the top of the heap is the one having lowest priority where (Priority 0 is better than Priority 10)) */

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                // Set node with shortest path as the current Node 
                int u = minpriorityqueue.extract_min();   //get me the node with the shortest distance (naturally this will get top of heap and remove the top and sift down)
                shortestPathTreeSet[u] = true;  // mark node with shortest path as visited

                for (int v = 0; v < verticesCount; ++v)
                    // loop thru all unvisited neighbor nodes where path to the neighbor node is shorter than what it (neighbor) currently has
                    // u: current node, v: neighbor node(s)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                    {
                        distance[v] = distance[u] + graph[u, v];
                        predecessor[v] = u; //the predecessor of neighbor v is the current node u
                        minpriorityqueue.decrease_priority(v, distance[v]);   //now node v has a better priority (it is more up the heap)
                    }
            }
            Print(distance, verticesCount);
        }
        #endregion

        #region BellmanFord
        public static void BellmanFord(int[,] graph, int source, int verticesCount)
        {
            int[] dist = new int[verticesCount];

            //Init
            for (int i = 0; i < verticesCount; i++)
            {
                dist[i] = int.MaxValue;
            }

            //Designated source
            dist[source] = 0;

            for (int i = 0; i < verticesCount - 1; i++)
            {
                for (int u = 0; u < verticesCount; u++)     //loop to all nodes
                {
                    for (int v = 0; v < verticesCount; v++)  //loop thru all edges from u --> v
                    {
                        if (graph[u, v] != int.MaxValue)    //there is a connect if it is max
                        {
                            int w = graph[u, v];
                            if (dist[u] + w < dist[v])
                            {
                                dist[v] = dist[u] + w;
                            }
                        }
                    }
                }
            }

            Print(dist, verticesCount);
        }
        #endregion

        #region FloydRoyWarshal
        public static void FloyRoyWarshal(int[,] graph, int gsize)
        {
            int[,] dist = new int[gsize, gsize];
            int[,] path = new int[gsize, gsize];

            // int
            for (int i = 0; i < gsize - 1; i++)
            {
                for (int j = 0; j < gsize - 1; j++)
                {
                    if (graph[i, j] != 0)
                    {
                        dist[i, j] = graph[i, j];
                        path[i, j] = i;
                    }
                    else
                    {
                        // no path - in graph matrix, if the value is zero - it means there is no path
                        dist[i, j] = int.MaxValue;
                        path[i, j] = -1;
                    }
                }
            }

            // to be continued :)
        }

        public static void PrintPathFromSToV(int node)
        {
            if (node == 0)
            {
                Console.Write("0");
            }
            else if (predecessor[node] == -1)
            {
                Console.Write("...No Path");
            }
            else
            {
                PrintPathFromSToV(predecessor[node]);
                Console.Write("..." + node);
            }
        }
        #endregion
    }
}
